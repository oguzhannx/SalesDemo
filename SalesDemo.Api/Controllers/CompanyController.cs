﻿using Microsoft.AspNetCore.Mvc;
using SalesDemo.Business.Abstract;

namespace SalesDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                var a = _companyService.GetCompanies();

                return Ok(a);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }


        }
        [HttpGet("GetByCompanyName")]
        public IActionResult GetByCompanyName(string companyName)
        {

            try
            {
                var a = _companyService.GetCompanyByCompanyName(companyName);

                return Ok(a);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }


        }

    }
}