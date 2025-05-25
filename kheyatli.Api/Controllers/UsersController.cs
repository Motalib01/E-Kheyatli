using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController<User> { public UsersController(IUnitOfWork uow) : base(uow, uow.Users) { } }