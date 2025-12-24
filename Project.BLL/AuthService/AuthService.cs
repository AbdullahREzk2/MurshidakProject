
namespace Project.BLL.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<AuthService> _logger;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly int _refreshTokenExpireDays = 14;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtProvider jwtProvider,
            ILogger<AuthService> logger,
            IEmailService emailService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _logger = logger;
            _emailService = emailService;
            _mapper = mapper;
        }

        // ============================
        // LOGIN
        // ============================
        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellation = default)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            // Explicit email confirmation check
            if (!user.EmailConfirmed)
                return Result.Failure<AuthResponse>(UserErrors.EmailNotConfirmed);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!signInResult.Succeeded)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            var (token, expiresIn) = _jwtProvider.GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpireDays);

            user.RefreshTokens.Add(new refreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(
                user.Id,
                user.Email!,
                user.firstName,
                user.lastName,
                token,
                expiresIn,
                refreshToken,
                refreshTokenExpiration);

            return Result.Success(response);
        }

        // ============================
        // REFRESH TOKEN
        // ============================
        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token,string refreshToken,CancellationToken cancellation = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var activeToken = user.RefreshTokens
                .SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (activeToken is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

            activeToken.RevokedOn = DateTime.UtcNow;

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user);

            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration =
                DateTime.UtcNow.AddDays(_refreshTokenExpireDays);

            user.RefreshTokens.Add(new refreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(
                user.Id,
                user.Email!,
                user.firstName,
                user.lastName,
                newToken,
                expiresIn,
                newRefreshToken,
                refreshTokenExpiration);

            return Result.Success(response);
        }

        // ============================
        // REGISTER
        // ============================
        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellation = default)
        {
            var exists = await _userManager.Users
                .AnyAsync(x => x.Email == request.Email, cancellation);

            if (exists)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return Result.Failure(UserErrors.InvalidCredentials);

            // ✅ Add user to Student role
            var roleResult = await _userManager.AddToRoleAsync(user, "Student");
            if (!roleResult.Succeeded)
                return Result.Failure(UserErrors.RoleAssignmentFailed);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            await _emailService.SendConfirmationEmailAsync(user, code);

            return Result.Success();
        }

        // ============================
        // CONFIRM EMAIL
        // ============================
        public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                return Result.Failure(UserErrors.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            string code;

            try
            {
                code = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(request.Code));
            }
            catch
            {
                return Result.Failure(UserErrors.InvalidCode);
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(
                error.Code,
                error.Description,
                StatusCodes.Status400BadRequest));
        }

        // ============================
        // RESEND CONFIRMATION
        // ============================
        public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null || user.EmailConfirmed)
                return Result.Success();

            var code =
                await _userManager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            await _emailService.SendConfirmationEmailAsync(user, code);

            return Result.Success();
        }

        // ============================
        // RESET PASSWORD
        // ============================
        public async Task<Result> SendResetPasswordCodeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || !user.EmailConfirmed)
                return Result.Success();

            var code =
                await _userManager.GeneratePasswordResetTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            await _emailService.SendResetPasswordEmailAsync(user, code);

            return Result.Success();
        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return Result.Failure(UserErrors.InvalidCode);

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(request.Code));

                result = await _userManager
                    .ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch
            {
                result = IdentityResult.Failed(
                    _userManager.ErrorDescriber.InvalidToken());
            }

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(
                error.Code,
                error.Description,
                StatusCodes.Status400BadRequest));
        }

        // ============================
        // REVOKE REFRESH TOKEN
        // ============================
        public async Task<Result> RevokeRefreshTokenAsync(string token,string refreshToken,CancellationToken cancellation = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var activeToken = user.RefreshTokens
                .SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (activeToken is null)
                return Result.Failure(UserErrors.InvalidRefreshToken);

            activeToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return Result.Success();
        }

        // ============================
        // HELPERS
        // ============================
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));
        }



    }
}
