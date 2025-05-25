using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ChatMessagesController : BaseController<ChatMessage> {
    public ChatMessagesController(IUnitOfWork uow) : base(uow, uow.ChatMessages)
    {

    }
}