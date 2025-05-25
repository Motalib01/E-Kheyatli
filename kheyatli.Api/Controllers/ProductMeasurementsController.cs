using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductMeasurementsController : BaseController<ProductMeasurement> { public ProductMeasurementsController(IUnitOfWork uow) : base(uow, uow.ProductMeasurements) { } }