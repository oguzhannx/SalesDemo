using Microsoft.AspNetCore.Mvc;
using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<SaleController>
        [HttpGet]
        public  IEnumerable<Sale> GetAll()
        {
              var a =  _saleService.getAllSales().Result.Result;

            return a;
        }

    }
}
