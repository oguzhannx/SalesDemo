using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesDemo.Business.Abstract;

namespace SalesDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var a = _productService.GetProducts();
                return Ok(a.Result);

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
