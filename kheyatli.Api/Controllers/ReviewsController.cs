using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReviewsController : BaseController<Review> { public ReviewsController(IUnitOfWork uow) : base(uow, uow.Reviews) { } }