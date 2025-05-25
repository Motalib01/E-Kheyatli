using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrdersController : BaseController<Order> { public OrdersController(IUnitOfWork uow) : base(uow, uow.Orders) { } }