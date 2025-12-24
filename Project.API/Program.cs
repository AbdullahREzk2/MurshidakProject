
using Project.BLL.Services;

namespace Project.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // -----------------------------
            // Add services to the container
            // -----------------------------

            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // -----------------------------
            // JWT Options Binding  ? (IMPORTANT)
            // -----------------------------
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("Jwt"));

            // -----------------------------
            // Authentication (JWT Bearer)
            // -----------------------------
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwt = builder.Configuration
                    .GetSection("Jwt")
                    .Get<JwtOptions>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwt!.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt.Key)),

                    ClockSkew = TimeSpan.Zero
                };
            });

            // -----------------------------
            // AutoMapper
            // -----------------------------
            builder.Services.AddAutoMapper(typeof(AuthMap).Assembly);

            // -----------------------------
            // JWT Provider
            // -----------------------------
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            // -----------------------------
            // Email
            // -----------------------------
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            // -----------------------------
            // Application Services
            // -----------------------------
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<ILevelRepository,LevelRepository>();
            builder.Services.AddScoped<ILevelService,LevelService>();
            builder.Services.AddScoped<ISpeclizationRepository, SpeclizationRepository>();
            builder.Services.AddScoped<ISpeclizationService, SpeclizationService>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IprofileService, profileService>();
            // -----------------------------
            // Controllers
            // -----------------------------
            builder.Services.AddControllers();

            // -----------------------------
            // Swagger
            // -----------------------------
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // -----------------------------
            // Configure HTTP pipeline
            // -----------------------------
            
                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }


    }
}
