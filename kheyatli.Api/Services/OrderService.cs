using AutoMapper;
using kheyatli.Api.Data;
using kheyatli.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace kheyatli.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Set price and delivery date
        public async Task SetPriceAndTimeAsync(Guid orderId, decimal price, DateTime deliveryDate)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return;

            order.Price = price;
            order.UpdatedAt = DateTime.UtcNow;

            if (order.Status == OrderStatus.Pending)
                order.Status = OrderStatus.Confirmed;

            order.DeliveredAt = deliveryDate;

            await _context.SaveChangesAsync();
        }

        // Get all orders by client
        public async Task<IEnumerable<OrderDTO>> GetOrdersByClientAsync(Guid clientId)
        {
            var orders = await _context.Orders
                .Where(o => o.ClientId == clientId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        // Get all orders by tailor
        public async Task<IEnumerable<OrderDTO>> GetOrdersByTailorAsync(Guid tailorId)
        {
            var orders = await _context.Orders
                .Where(o => o.TailorId == tailorId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        // Update order status
        public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return;

            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;

            if (status == OrderStatus.Delivered)
            {
                order.DeliveredAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        // Cancel an order
        public async Task CancelOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return;

            order.Status = OrderStatus.Cancelled;
            order.CancelledAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
