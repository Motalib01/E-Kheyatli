using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public interface IChatService
{
    Task SendMessageAsync(Guid senderId, Guid receiverId, string message);
    Task<IEnumerable<ChatMessageDTO>> GetChatHistoryAsync(Guid userId, Guid partnerId);
}