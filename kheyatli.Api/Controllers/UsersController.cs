using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kheyatli.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController<User>
{
    private ApplicationDbContext _context;
    public UsersController(IUnitOfWork uow, ApplicationDbContext context) : base(uow, uow.Users)
    {
        _context = context;
    }

    [HttpPost("{userId}/upload-profile-picture")]
    public async Task<IActionResult> UploadProfilePicture(Guid userId, IFormFile file)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound("User not found");

        if (file == null || file.Length == 0) return BadRequest("Invalid file.");

        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-pictures");
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        user.ProfilePictureURL = $"/profile-pictures/{fileName}";
        await _context.SaveChangesAsync();

        return Ok(new { profilePictureUrl = user.ProfilePictureURL });
    }

}