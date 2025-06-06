using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public class ChatService : IChatService
{
    public Task SendMessageAsync(Guid senderId, Guid receiverId, string message) => 
        Task.CompletedTask;
    public Task<IEnumerable<ChatMessageDTO>> GetChatHistoryAsync(Guid userId, Guid partnerId) => 
        Task.FromResult<IEnumerable<ChatMessageDTO>>(new List<ChatMessageDTO>());
}