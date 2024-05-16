using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace SalesDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public Result<ICollection<Product>> Get()
        {
            try
            {

                return _productService.GetProducts();

            }
            catch (System.Exception e)
            {

                return new Result<ICollection<Product>>(Int32.Parse(HttpStatusCode.BadRequest.ToString()),e.Message, null, DateTime.Now );
            }
        }
    }
}
