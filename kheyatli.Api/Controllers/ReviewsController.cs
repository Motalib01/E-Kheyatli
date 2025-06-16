using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//<summary>
// ReviewsController handles operations related to reviews in the Kheyatli application.
//</summary>
namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReviewsController : BaseController<Review> {
    private readonly ApplicationDbContext _context;
    public ReviewsController(IUnitOfWork uow, ApplicationDbContext context) : base(uow, uow.Reviews)
    {
        _context = context;
    }
    [HttpGet("tailor/{tailorId:guid}")]
    public async Task<IActionResult> GetReviewsByTailorId(Guid tailorId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.TailorId == tailorId)
            .Select(r => new
            {
                r.Id,
                r.Rate,
                r.Comment,
                r.CreatedAt,
                Client = r.Client != null ? new
                {
                    r.Client.Id,
                    r.Client.User.FirstName,
                    r.Client.User.LastName,
                    r.Client.User.EmailAddress
                } : null
            })
            .ToListAsync();

        return Ok(reviews);
    }
}