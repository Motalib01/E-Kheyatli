using kheyatli.Api.Models;

public class ChatMessage : Base
{
    public Guid ChatId { get; set; }

    public string Message { get; set; }
    public DateTime SentAt { get; set; }

    public Chat? Chat { get; set; }
}