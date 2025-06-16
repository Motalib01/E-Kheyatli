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

    public async Task<IEnumerable<object>> GetAllTailorsWithReviewsAsync()
    {
        var tailors = await _context.Tailors
            .Include(t => t.User)
            .Include(t => t.Portfolio)
            .Include(t => t.Category)
            .ToListAsync();

        var tailorIds = tailors.Select(t => t.Id).ToList();

        var reviews = await _context.Reviews
            .Where(r => tailorIds.Contains(r.TailorId))
            .Select(r => new
            {
                r.Id,
                r.TailorId,
                r.Rate,
                r.Comment,
                r.CreatedAt,
                Client = new
                {
                    r.ClientId
                }
            })
            .ToListAsync();

        var result = tailors.Select(t => new
        {
            t.Id,
            t.Brand,
            t.Bio,
            t.Address,
            t.IsActive,
            Category = t.Category != null ? new
            {
                t.Category.Id,
                t.Category.Name
            } : null,
            User = t.User != null ? new
            {
                t.User.Id,
                t.User.FirstName,
                t.User.LastName,
                t.User.EmailAddress,
                t.User.PhoneNumber,
                t.User.ProfilePictureURL
            } : null,
            Reviews = reviews.Where(r => r.TailorId == t.Id)
        });

        return result;
    }
    public async Task<object?> GetTailorWithReviewsAsync(Guid tailorId)
    {
        var tailor = await _context.Tailors
            .Include(t => t.User)
            .Include(t => t.Portfolio)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == tailorId);

        if (tailor == null) return null;

        var reviews = await _context.Reviews
            .Where(r => r.TailorId == tailorId)
            .Select(r => new
            {
                r.Id,
                r.Rate,
                r.Comment,
                r.CreatedAt,
                Client = new
                {
                    r.ClientId
                }
            })
            .ToListAsync();

        return new
        {
            tailor.Id,
            tailor.Brand,
            tailor.Bio,
            tailor.Address,
            tailor.IsActive,
            Category = tailor.Category != null ? new
            {
                tailor.Category.Id,
                tailor.Category.Name
            } : null,
            User = tailor.User != null ? new
            {
                tailor.User.Id,
                tailor.User.FirstName,
                tailor.User.LastName,
                tailor.User.EmailAddress,
                tailor.User.PhoneNumber,
                tailor.User.ProfilePictureURL
            } : null,
            Reviews = reviews
        };
    }


}