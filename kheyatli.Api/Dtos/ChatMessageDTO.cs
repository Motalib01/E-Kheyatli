namespace kheyatli.Api.Dtos;

public class ChatMessageDTO
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public string Message { get; set; }
    public DateTime SentAt { get; set; }
}