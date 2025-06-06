using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public interface IOrderService
{
    Task SetPriceAndTimeAsync(Guid orderId, decimal price, DateTime deliveryDate);
    Task<IEnumerable<OrderDTO>> GetOrdersByClientAsync(Guid clientId);
    Task<IEnumerable<OrderDTO>> GetOrdersByTailorAsync(Guid tailorId);
    Task UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
    Task CancelOrderAsync(Guid orderId);
}