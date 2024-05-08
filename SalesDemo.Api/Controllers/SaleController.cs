using Microsoft.AspNetCore.Mvc;
using SalesDemo.Business.Abstract;

namespace SalesDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {

        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var a = _saleService.getAllSales().Result;
                return Ok(a.Result);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("FromCompanyId")]
        public IActionResult GetSalesFromCompanyId(string id)
        {
            try
            {
                var a = _saleService.getSalesByComppanyId(id).Result;

                return Ok(a);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
