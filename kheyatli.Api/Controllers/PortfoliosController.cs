using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PortfoliosController : BaseController<Portfolio> { public PortfoliosController(IUnitOfWork uow) : base(uow, uow.Portfolios) { } }