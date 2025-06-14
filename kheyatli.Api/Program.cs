using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using kheyatli.Api.Startup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

namespace kheyatli.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 
            var configuration = builder.Configuration;

            builder.AddDependencies();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    // Convert enum values to and from their string names (e.g., "Admin" instead of 1)
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });


            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            builder.Services.AddSingleton(jwtSettings);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            // Ensure directories exist
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var profilePicDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-pictures");

            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            if (!Directory.Exists(profilePicDir))
                Directory.CreateDirectory(profilePicDir);

            // Serve static files from wwwroot (default)
            app.UseStaticFiles();

            // Serve files from /wwwroot/uploads mapped to /uploads
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadDir),
                RequestPath = "/uploads"
            });

            // Serve files from /wwwroot/profile-pictures mapped to /profile-pictures
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(profilePicDir),
                RequestPath = "/profile-pictures"
            });



            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }

        public class JwtSettings
        {
            public string Key { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public int DurationInMinutes { get; set; }
        }

        public class TokenService
        {
            private readonly JwtSettings _jwtSettings;

            public TokenService(JwtSettings jwtSettings)
            {
                _jwtSettings = jwtSettings;
            }

            public string GenerateToken(User user)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.EmailAddress),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}
