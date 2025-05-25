using kheyatli.Api.Models;

public class Admin: Base
{
    public Guid UserId { get; set; }
    public User User { get; set; }
}