using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ClientsController : BaseController<Client> { public ClientsController(IUnitOfWork uow) : base(uow, uow.Clients) { } }