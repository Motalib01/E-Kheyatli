using kheyatli.Api.Data;
using kheyatli.Api.Dtos;
using kheyatli.Api.Models;
using kheyatli.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : BaseController<Client>
{
    private readonly IClientService _clientService;

    public ClientsController(IUnitOfWork uow, IClientService clientService) : base(uow, uow.Clients)
    {
        _clientService = clientService;
    }

    // GET: api/clients/tailors/{tailorId}/portfolio
    [HttpGet("tailors/{tailorId}/portfolio")]
    public async Task<ActionResult<PortfolioDTO>> ViewTailorPortfolio(Guid tailorId)
    {
        var portfolio = await _clientService.ViewTailorPortfolioAsync(tailorId);
        if (portfolio == null) return NotFound("Tailor portfolio not found");
        return Ok(portfolio);
    }

    // POST: api/clients/products/{productId}/measurements
    [HttpPost("products/{productId}/measurements")]
    public async Task<IActionResult> AddMeasurements(Guid productId, [FromBody] IEnumerable<ProductMeasurementDTO> measurements)
    {
        if (measurements == null || !measurements.Any()) return BadRequest("No measurements provided");

        await _clientService.AddMeasurementsAsync(productId, measurements);
        return NoContent();
    }

    // GET: api/clients/{clientId}/orders
    [HttpGet("{clientId}/orders")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> TrackOrders(Guid clientId)
    {
        var orders = await _clientService.TrackOrdersAsync(clientId);
        return Ok(orders);
    }

    // GET: api/clients/tailors/{tailorId}/measurement-guides
    [HttpGet("tailors/{tailorId}/measurement-guides")]
    public async Task<ActionResult<IEnumerable<MeasurementsGuideDTO>>> ViewMeasurementGuides(Guid tailorId)
    {
        var guides = await _clientService.ViewMeasurementGuidesAsync(tailorId);
        return Ok(guides);
    }
}
