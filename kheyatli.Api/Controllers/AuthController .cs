using System.Security.Cryptography;
using kheyatli.Api.Data;
using kheyatli.Api.Dtos;
using Microsoft.AspNetCore.Identity.Data;
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
    public async Task<IActionResult> Login([FromBody] Dtos.LoginRequest loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.EmailAddress == loginDto.EmailAddress);

        if (user == null)
            return Unauthorized("Invalid credentials");

        var hashedInputPassword = HashPassword(loginDto.Password);

        if (user.Password != hashedInputPassword)
            return Unauthorized("Invalid credentials");

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (_context.Users.Any(u => u.EmailAddress == dto.EmailAddress))
            return BadRequest("Email already in use.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
            Password = HashPassword(dto.Password), // You should use a hashing library
            Role = dto.Role
        };

        _context.Users.Add(user);

        //switch (dto.Role)
        //{
        //    case UserRole.Client:
        //        var client = new Client
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = user.Id,
        //            Address = dto.Address,
        //            CreatedAt = DateTime.UtcNow
        //        };
        //        _context.Clients.Add(client);
        //        break;

        //    case UserRole.Tailor:
        //        var tailor = new Tailor
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = user.Id,
        //            Address = dto.Address,
        //            Brand = dto.Brand,
        //            Bio = dto.Bio,
        //            CreatedAt = DateTime.UtcNow
        //        };
        //        _context.Tailors.Add(tailor);
        //        break;

        //    case UserRole.Admin:
        //        var admin = new Admin
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = user.Id
        //        };
        //        _context.Admins.Add(admin);
        //        break;
        //}

        await _context.SaveChangesAsync();

        var token = _tokenService.GenerateToken(user);
        return Ok(new { Token = token });
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
