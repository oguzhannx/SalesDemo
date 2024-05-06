using Microsoft.AspNetCore.Mvc;
using SalesDemo.Business.Abstract;
using SalesDemo.Entities;
using SalesDemo.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace SalesDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ICollection<Product> GetProducts()
        {
            return _productService.GetProducts().Result.ToList();
        }
        [HttpGet("FromCompany")]
        public List<ProductFromCompanyDto> GetProductsFromCompany()
        {
            var a = _productService.GetProductsFromCompany();

            return a;
        }






    }
}
