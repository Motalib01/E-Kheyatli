using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kheyatli.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController<Product>
{
    private readonly ApplicationDbContext _context;

    public ProductsController(IUnitOfWork uow, ApplicationDbContext context) : base(uow, uow.Products)
    {
        _context = context;
    }

    [HttpGet("{productId:guid}/productWithImages")]
    public async Task<IActionResult> GetProductWithImages(Guid productId)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Tailor)
            .Include(p => p.Portfolio)
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
            return NotFound(new { Message = "Product not found" });

        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var result = new
        {
            product.Id,
            product.Name,
            product.FabricPreferences,
            product.StyleReferences,
            product.Quote,
            product.Notes,
            Tailor = product.Tailor != null ? new
            {
                product.Tailor.Id,
                product.Tailor.Brand,
                product.Tailor.Bio
            } : null,
            Images = product.Images.Select(img => new
            {
                img.Id,
                ImageUrl = $"{baseUrl}{img.ImageUrl}"
            }),
            Portfolio = product.Portfolio != null ? new
            {
                product.Portfolio.Id
            } : null
        };

        return Ok(result);
    }

    [HttpPost("{productId}/images")]
    public async Task<IActionResult> UploadImages(Guid productId, List<IFormFile> files)
    {
        var productExists = await _context.Products.AnyAsync(p => p.Id == productId);
        if (!productExists) return NotFound(new { Message = "Product not found" });

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsFolder);

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return BadRequest(new { Message = $"Invalid file type: {extension}" });

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var productImage = new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ImageUrl = $"/uploads/{fileName}"
            };
            _context.ProductImages.Add(productImage);
        }

        await _context.SaveChangesAsync();

        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var images = await _context.ProductImages
            .Where(img => img.ProductId == productId)
            .Select(img => new
            {
                img.Id,
                ImageUrl = $"{baseUrl}{img.ImageUrl}"
            })
            .ToListAsync();

        return Ok(new
        {
            Message = "Images uploaded successfully",
            Images = images
        });
    }



    [HttpGet("{productId}/images")]
    public async Task<IActionResult> GetImagesByProductId(Guid productId)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
            return NotFound(new { Message = "Product not found" });

        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var images = product.Images.Select(img => new
        {
            img.Id,
            ImageUrl = $"{baseUrl}{img.ImageUrl}"
        });

        return Ok(images);
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
            return NotFound(new { Message = "No products found for this portfolio." });

        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var result = products.Select(p => new
        {
            p.Id,
            p.Name,
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
            Images = p.Images.Select(img => new
            {
                img.Id,
                ImageUrl = $"{baseUrl}{img.ImageUrl}"
            }),
            Portfolio = new
            {
                p.PortfolioId
            }
        });

        return Ok(result);
    }
}
