using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AdminsController : BaseController<Admin> {
    public AdminsController(IUnitOfWork uow) : base(uow, uow.Admins)
    {

    }
}