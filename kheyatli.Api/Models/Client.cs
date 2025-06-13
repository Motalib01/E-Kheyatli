using kheyatli.Api.Models;

public class Client : Base
{
    public Guid UserId { get; set; }
    public string? Address { get; set; }

    public bool? IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }

    public User? User { get; set; }

    public ICollection<Order>? Orders { get; set; }
    public ICollection<Chat> Chats { get; set; }
}