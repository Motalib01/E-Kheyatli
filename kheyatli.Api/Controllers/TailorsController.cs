using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TailorsController : BaseController<Tailor> { public TailorsController(IUnitOfWork uow) : base(uow, uow.Tailors) { } }