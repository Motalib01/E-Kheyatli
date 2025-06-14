using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PortfoliosController : BaseController<Portfolio> {
    private readonly ApplicationDbContext _context;
    public PortfoliosController(IUnitOfWork uow, ApplicationDbContext context) : base(uow, uow.Portfolios)
    {
        _context = context;
    }

    [HttpGet("{portfolioId}/with-thumbnail")]
    public async Task<IActionResult> GetProductsWithThumbnailByPortfolioId(Guid portfolioId)
    {
        var products = await _context.Products
            .Where(p => p.PortfolioId == portfolioId)
            .Include(p => p.Images)
            .Include(p => p.Tailor)
            .Include(p => p.Portfolio)
            .ToListAsync();

        if (!products.Any())
            return NotFound("No products found for this portfolio.");

        var result = products.Select(p => new
        {
            p.Id,
            p.FabricPreferences,
            p.StyleReferences,
            p.Quote,
            p.Notes,
            Tailor = p.Tailor != null ? new
            {
                p.Tailor.Id,
                p.Tailor.Brand,
                p.Tailor.Bio
            } : null,
            Thumbnail = p.Images.OrderBy(i => i.Id).Select(img => new
            {
                img.Id,
                img.ImageUrl
            }).FirstOrDefault(),
            Portfolio = p.Portfolio != null
                ? new { p.Portfolio.Id}
                : null
        });

        return Ok(result);
    }


    [HttpGet("portfolio/{portfolioId}")]
    public async Task<IActionResult> GetProductsByPortfolioId(Guid portfolioId)
    {
        var products = await _context.Products
            .Where(p => p.PortfolioId == portfolioId)
            .Include(p => p.Images)
            .Include(p => p.Tailor)
            .Include(p => p.Portfolio)
            .ToListAsync();

        if (products == null || !products.Any())
            return NotFound("No products found for this portfolio.");

        var result = products.Select(p => new
        {
            p.Id,
            p.FabricPreferences,
            p.StyleReferences,
            p.Quote,
            p.Notes,
            Tailor = p.Tailor != null ? new
            {
                p.Tailor.Id,
                p.Tailor.Brand,
                p.Tailor.Bio
            } : null,
            Images = p.Images != null
                ? p.Images.Select(img => (object)new
                {
                    img.Id,
                    img.ImageUrl
                }).ToList()
                : new List<object>(),
            Portfolio = new
            {
                p.PortfolioId
            }
        });

        return Ok(result);
    }


}