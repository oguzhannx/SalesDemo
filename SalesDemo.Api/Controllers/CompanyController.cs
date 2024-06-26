﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public Result<ICollection<Company>> Get()
        {
            try
            {
                return _companyService.GetCompanies();
            }
            catch (System.Exception e)
            {
                return new Result<ICollection<Company>>(Int32.Parse(HttpStatusCode.BadRequest.ToString()), e.Message, null, DateTime.Now);
            }
        }

        [HttpGet("GetByCompanyName")]
        public Result<Company> GetCompanyByName(string companyName)
        {
            try
            {
                return _companyService.GetCompanyByCompanyName(companyName);
            }
            catch (System.Exception e)
            {
                return new Result<Company>(Int32.Parse(HttpStatusCode.BadRequest.ToString()), e.Message, null, DateTime.Now);
            }
        }


        [HttpPost]
        public Result<Company> PostCompany([FromBody]Company company)
        {
            return _companyService.AddCompany(company);
        }

        [HttpDelete]
        public Result<Company> DeleteCompany(string id)
        {
            return _companyService.DeleteCompany(id);
        }
       
        
        [HttpPut]
        public Result<Company> UpdateCompany([FromBody]Company company,string id)
        {
            company.Id = ObjectId.Parse(id);
            return _companyService.UpdateCompany(company, id);
        }

    }
}
