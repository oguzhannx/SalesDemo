using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    
    public class CompanyController : ControllerBase
    {

        public readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpGet]
        public IList<Company> GetCompanies()
        {
            var a = _companyService.GetCompanies();

            return a.ToList();
        } 
        
        [HttpGet("WithoutProducts")]
        public ICollection<CompanyWithoutProductsDto> GetCompanyWithoutProducts()
        {
            List<CompanyWithoutProductsDto> dto = new List<CompanyWithoutProductsDto>();


            var a = _companyService.GetCompanies();

            foreach (var item in a)
            {
                CompanyWithoutProductsDto companyWithoutProducts = new CompanyWithoutProductsDto
                {
                    Name = item.CompanyName,
                    PhoneNumber = item.PhoneNumber,
                    
                };
                dto.Add(companyWithoutProducts);
            }

            return dto;
        }
    }
}
