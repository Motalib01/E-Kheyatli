using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ChatsController : BaseController<Chat> { public ChatsController(IUnitOfWork uow) : base(uow, uow.Chats) { } }