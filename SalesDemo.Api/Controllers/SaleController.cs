using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Net;

namespace SalesDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class SaleController : ControllerBase
    {

        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }



        [HttpGet]
        public Result<ICollection<Sale>> Get()
        {
            try
            {

                return _saleService.getAllSales();
            }
            catch (System.Exception e)
            {

                return new Result<ICollection<Sale>>(Int32.Parse(HttpStatusCode.BadRequest.ToString()), e.Message, null, DateTime.Now);
            }
        }


        [HttpGet("FromCompanyId")]
        public Result<ICollection<Sale>> GetSalesFromCompanyId(string id)
        {
            try
            {


                return _saleService.getSalesByComppanyId(id);
            }
            catch (System.Exception e)
            {

                return new Result<ICollection<Sale>>(Int32.Parse(HttpStatusCode.BadRequest.ToString()), e.Message, null, DateTime.Now);
            }
        }



        [HttpGet("test")]
        [Authorize(Roles = "seyhanlar")]
        public IActionResult test()
        {
            return Ok("it works....");
        }
    }
}
