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
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return NotFound("Product not found");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        foreach (var file in files)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/uploads/{fileName}";
            _context.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                ImageUrl = imageUrl
            });
        }

        await _context.SaveChangesAsync();
        return Ok("Images uploaded successfully");
    }
}