using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middlewares_API.Models.Entity;
using Middlewares_API.Services;

namespace Middlewares_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("GetCategories")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return Ok(CategoryService.GetCategories());
        }
    }
}
