using System.ComponentModel.DataAnnotations;

namespace kheyatli.Api.Dtos;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required, Phone]
    public string PhoneNumber { get; set; }

    [Required, EmailAddress]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }

    public string ProfilePictureURL { get; set; }

    [Required]
    public UserRole Role { get; set; }

    // Extra fields
    public string Address { get; set; } // For Client or Tailor
    public string Brand { get; set; }   // For Tailor
    public string Bio { get; set; }     // For Tailor
}
