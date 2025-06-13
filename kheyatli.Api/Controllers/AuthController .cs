using System.Security.Cryptography;
using kheyatli.Api.Data;
using kheyatli.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static kheyatli.Api.Program;

namespace kheyatli.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(ApplicationDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.EmailAddress == loginDto.EmailAddress);

        if (user == null)
            return Unauthorized("Invalid credentials");

        var hashedInputPassword = HashPassword(loginDto.Password);

        if (user.Password != hashedInputPassword)
            return Unauthorized("Invalid credentials");

        var token = _tokenService.GenerateToken(user);
        return Ok(new
        {
            Token = token,
            User = new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.EmailAddress,
                user.Role
            }
        });
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!Enum.IsDefined(typeof(UserRole), dto.Role))
            return BadRequest("Invalid role.");

        if (_context.Users.Any(u => u.EmailAddress == dto.EmailAddress))
            return BadRequest("Email already in use.");

        // Create User
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
            Password = HashPassword(dto.Password),
            Role = dto.Role
        };

        _context.Users.Add(user);

        Guid? tailorId = null;
        Guid? portfolioId = null;

        switch (dto.Role)
        {
            case UserRole.Tailor:
                // Generate tailorId first to use for both Tailor and Portfolio
                tailorId = Guid.NewGuid();

                // Create Portfolio first and assign the tailorId (Foreign Key)
                var portfolio = new Portfolio
                {
                    Id = Guid.NewGuid(),
                    TailorId = tailorId, // Connect portfolio to tailor-to-be-created
                                         // Add default properties here if necessary (e.g., CreatedAt)
                };
                portfolioId = portfolio.Id;
                _context.Portfolios.Add(portfolio);

                // Create Tailor
                var tailor = new Tailor
                {
                    Id = tailorId.Value,
                    UserId = user.Id,
                    PortfolioId = portfolio.Id,
                    Brand = dto.Brand,
                    Bio = dto.Bio,
                    Address = dto.Address,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                _context.Tailors.Add(tailor);
                break;

            case UserRole.Client:
                var client = new Client
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Address = dto.Address,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                _context.Clients.Add(client);
                break;

            case UserRole.Admin:
                // No additional table yet.
                break;

            default:
                return BadRequest("Unsupported role.");
        }

        await _context.SaveChangesAsync();

        // Generate token
        var token = _tokenService.GenerateToken(user);

        return Ok(new
        {
            Token = token,
            UserId = user.Id,
            Role = user.Role,
            TailorId = tailorId,
            PortfolioId = portfolioId
        });
    }



    [HttpGet("me")]
    public async Task<IActionResult> GetProfile(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.Client)
            .Include(u => u.Tailor)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return NotFound();

        return Ok(new
        {
            UserId = user.Id,
            TailorId = user.Tailor?.Id,  
            ClientId = user.Client?.Id,  
            user.FirstName,
            user.LastName,
            user.EmailAddress,
            user.Role,
            user.PhoneNumber,
            user.ProfilePictureURL,
            Address = user.Role switch
            {
                UserRole.Client => user.Client?.Address,
                UserRole.Tailor => user.Tailor?.Address,
                _ => null
            },
            Bio = user.Role == UserRole.Tailor ? user.Tailor?.Bio : null,
            Brand = user.Role == UserRole.Tailor ? user.Tailor?.Brand : null,
            IsActive = user.Role switch
            {
                UserRole.Client => user.Client?.IsActive,
                UserRole.Tailor => user.Tailor?.IsActive,
                _ => null
            },
            CreatedAt = user.Role switch
            {
                UserRole.Client => user.Client?.CreatedAt,
                UserRole.Tailor => user.Tailor?.CreatedAt,
                _ => null
            }
        });
    }


    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return NotFound();

        var currentPasswordHash = HashPassword(dto.CurrentPassword);
        if (user.Password != currentPasswordHash)
            return BadRequest("Current password is incorrect.");

        user.Password = HashPassword(dto.NewPassword);
        await _context.SaveChangesAsync();

        return Ok("Password changed successfully.");
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
