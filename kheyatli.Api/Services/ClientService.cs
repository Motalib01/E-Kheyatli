using kheyatli.Api.Dtos;
using kheyatli.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using kheyatli.Api.Data;

namespace kheyatli.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get a tailor's portfolio by tailorId
        public async Task<PortfolioDTO> ViewTailorPortfolioAsync(Guid tailorId)
        {
            var portfolio = await _context.Portfolios
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.TailorId == tailorId);

            if (portfolio == null) return null;

            return _mapper.Map<PortfolioDTO>(portfolio);
        }

        // Add measurements to a product
        public async Task AddMeasurementsAsync(Guid productId, IEnumerable<ProductMeasurementDTO> measurements)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return;

            var measurementEntities = _mapper.Map<List<ProductMeasurement>>(measurements);
            foreach (var m in measurementEntities)
            {
                m.ProductId = productId;
            }

            await _context.ProductMeasurements.AddRangeAsync(measurementEntities);
            await _context.SaveChangesAsync();
        }

        // Track all orders for a specific client
        public async Task<IEnumerable<OrderDTO>> TrackOrdersAsync(Guid clientId)
        {
            var orders = await _context.Orders
                .Where(o => o.ClientId == clientId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        // View all measurement guides from a specific tailor
        public async Task<IEnumerable<MeasurementsGuideDTO>> ViewMeasurementGuidesAsync(Guid tailorId)
        {
            var guides = await _context.MeasurementsGuides
                .Where(g => g.TailorId == tailorId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MeasurementsGuideDTO>>(guides);
        }
    }
}
