using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MeasurementsGuidesController : BaseController<MeasurementsGuide> { public MeasurementsGuidesController(IUnitOfWork uow) : base(uow, uow.MeasurementGuides) { } }