using kheyatli.Api.Models;

public class Notification : Base
{
    public string Title { get; set; }
    public string Content { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
}