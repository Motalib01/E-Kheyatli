using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController<Product> { public ProductsController(IUnitOfWork uow) : base(uow, uow.Products) { } }