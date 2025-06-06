using AutoMapper;
using kheyatli.Api.Data;
using kheyatli.Api.Dtos;
using kheyatli.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace kheyatli.Api.Services;

public class TailorService : ITailorService
{
    public TailorService(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public async Task<IEnumerable<OrderDTO>> ViewOrdersAsync(Guid tailorId)
    {
        var orders = await _context.Orders
            .Where(o => o.TailorId == tailorId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<OrderDTO>>(orders);
    }

    public async Task<bool> MarkOrderAsDeliveredAsync(Guid orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return false;

        order.Status = OrderStatus.Delivered; 
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task AddMeasurementGuideAsync(Guid tailorId, MeasurementsGuideDTO guide)
    {
        var entity = _mapper.Map<MeasurementsGuide>(guide);
        entity.TailorId = tailorId;

        await _context.MeasurementsGuides.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MeasurementsGuideDTO>> GetMyMeasurementGuidesAsync(Guid tailorId)
    {
        var guides = await _context.MeasurementsGuides
            .Where(g => g.TailorId == tailorId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MeasurementsGuideDTO>>(guides);
    }
}