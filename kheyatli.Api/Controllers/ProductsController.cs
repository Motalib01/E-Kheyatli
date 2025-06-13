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

    [HttpPost("{productId}/images")]
    public async Task<IActionResult> UploadImages(Guid productId, List<IFormFile> files)
    {
        var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
            return NotFound("Product not found");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uploadedImages = new List<object>();

        foreach (var file in files)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/uploads/{fileName}";

            var productImage = new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ImageUrl = imageUrl
            };

            _context.ProductImages.Add(productImage);
            uploadedImages.Add(new { productImage.Id, productImage.ImageUrl });
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Images uploaded successfully",
            Images = uploadedImages
        });
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
            Images = p.Images?.Select(img => new
            {
                img.Id,
                img.ImageUrl
            }),
            Portfolio = new
            {
                p.PortfolioId
            }
        });

        return Ok(result);
    }


}