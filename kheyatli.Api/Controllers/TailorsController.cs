using kheyatli.Api.Data;
using kheyatli.Api.Dtos;
using kheyatli.Api.Models;
using kheyatli.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TailorsController : BaseController<Tailor>
{
    private readonly ITailorService _tailorService;

    public TailorsController(IUnitOfWork uow, ITailorService tailorService) : base(uow, uow.Tailors)
    {
        _tailorService = tailorService;
    }

    // GET: api/tailors/{tailorId}/orders
    [HttpGet("{tailorId:guid}/orders")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> ViewOrders(Guid tailorId)
    {
        var orders = await _tailorService.ViewOrdersAsync(tailorId);
        return Ok(orders);
    }

    // PUT: api/tailors/orders/{orderId}/deliver
    [HttpPut("orders/{orderId:guid}/deliver")]
    public async Task<IActionResult> MarkOrderAsDelivered(Guid orderId)
    {
        var result = await _tailorService.MarkOrderAsDeliveredAsync(orderId);
        if (!result)
            return NotFound();

        return NoContent();
    }

    // POST: api/tailors/{tailorId}/measurement-guides
    [HttpPost("{tailorId:guid}/measurement-guides")]
    public async Task<IActionResult> AddMeasurementGuide(Guid tailorId, [FromBody] MeasurementsGuideDTO guide)
    {
        await _tailorService.AddMeasurementGuideAsync(tailorId, guide);
        return Ok();
    }

    // GET: api/tailors/{tailorId}/measurement-guides
    [HttpGet("{tailorId:guid}/measurement-guides")]
    public async Task<ActionResult<IEnumerable<MeasurementsGuideDTO>>> GetMyMeasurementGuides(Guid tailorId)
    {
        var guides = await _tailorService.GetMyMeasurementGuidesAsync(tailorId);
        return Ok(guides);
    }
}