using kheyatli.Api.Data;
using kheyatli.Api.Dtos;
using kheyatli.Api.Models;
using kheyatli.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : BaseController<Order>
{
    private readonly IOrderService _orderService;

    public OrdersController(IUnitOfWork uow, IOrderService orderService) : base(uow, uow.Orders)
    {
        _orderService = orderService;
    }

    [HttpPost("{orderId}/set-price-and-time")]
    public async Task<IActionResult> SetPriceAndTime(Guid orderId, [FromBody] SetPriceAndTimeRequest request)
    {
        await _orderService.SetPriceAndTimeAsync(orderId, request.Price, request.DeliveryDate);
        return NoContent();
    }

    [HttpGet("by-client/{clientId}")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByClient(Guid clientId)
    {
        var orders = await _orderService.GetOrdersByClientAsync(clientId);
        return Ok(orders);
    }

    [HttpGet("by-tailor/{tailorId}")]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByTailor(Guid tailorId)
    {
        var orders = await _orderService.GetOrdersByTailorAsync(tailorId);
        return Ok(orders);
    }

    [HttpPut("{orderId}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusRequest request)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, request.Status);
        return NoContent();
    }

    [HttpPut("{orderId}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid orderId)
    {
        await _orderService.CancelOrderAsync(orderId);
        return NoContent();
    }
}