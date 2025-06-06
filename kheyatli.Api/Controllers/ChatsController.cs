using kheyatli.Api.Data;
using kheyatli.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatsController : BaseController<Chat>
{
    public ChatsController(IUnitOfWork uow, IChatService chatService) : base(uow, uow.Chats)
    {
        _chatService = chatService;
    }
    private readonly IChatService _chatService;


    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(Guid senderId, Guid receiverId, string message)
    {
        await _chatService.SendMessageAsync(senderId, receiverId, message);
        return Ok();
    }
}