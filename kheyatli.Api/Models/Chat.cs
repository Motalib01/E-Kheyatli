using kheyatli.Api.Models;

public class Chat : Base
{

    public Guid ClientId { get; set; }
    public Guid TailorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Client Client { get; set; }
    public Tailor Tailor { get; set; }
    public ICollection<ChatMessage> Messages { get; set; }
}