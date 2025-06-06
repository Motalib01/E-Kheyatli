using System.ComponentModel;
using kheyatli.Api.Models;
using System.ComponentModel.DataAnnotations;

//refactor user and his role
public class User : Base
{

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [Required, EmailAddress]
    public string EmailAddress { get; set; }

    [Required, PasswordPropertyText]
    public string Password { get; set; }
    public string? ProfilePictureURL { get; set; }

    [Required]
    public UserRole Role { get; set; }

    public Client? Client { get; set; }
    public Tailor? Tailor { get; set; }
}