
namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
            return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
        }

        [HttpPost("revoke-refreshToken")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.RegisterAsync(request, cancellationToken);
            return authResult.IsSuccess ? Ok() : authResult.ToProblem();
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
                return Redirect("/email/verified.html?status=invalid");

            var request = new ConfirmEmailRequest(userId, code);
            var result = await _authService.ConfirmEmailAsync(request);

            if (result.IsSuccess)
                return Redirect("/email/verified.html?status=success");
            else
                return Redirect("/email/verified.html?status=failed");
        }

        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.ResendConfirmationEmailAsync(request);
            return authResult.IsSuccess ? Ok() : authResult.ToProblem();
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.SendResetPasswordCodeAsync(request.Email);
            return authResult.IsSuccess ? Ok() : authResult.ToProblem();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.ResetPasswordAsync(request);
            return authResult.IsSuccess ? Ok() : authResult.ToProblem();
        }

        private void SetRefreshTokenCookies(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }



    }
}
