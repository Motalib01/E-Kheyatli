using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }

    // Tailor fields
    public string? Brand { get; set; }
    public string? Bio { get; set; }
    public string? Address { get; set; }
}