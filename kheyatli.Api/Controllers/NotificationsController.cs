using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NotificationsController : BaseController<Notification> { public NotificationsController(IUnitOfWork uow) : base(uow, uow.Notifications) { } }