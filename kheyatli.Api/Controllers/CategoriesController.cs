using kheyatli.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace kheyatli.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : BaseController<Category> {
    public CategoriesController(IUnitOfWork uow) : base(uow, uow.Categories)
    {

    }

}