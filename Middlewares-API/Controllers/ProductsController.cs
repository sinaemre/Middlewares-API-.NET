using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middlewares_API.Models.Entity;
using Middlewares_API.Services;

namespace Middlewares_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("GetProducts")]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var unitPrice =  ProductService.Calculate();
            return Ok($"Products : {ProductService.GetProducts()}\nTotal Unit Price : {unitPrice}");
        }
    }
}
