using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;
using SalesDemo.Entities.Auth;
using SalesDemo.Models.ViewModels;
using SalesDemo.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICompanyRepository companyRepository, UserManager<User> userManager,
            ISaleRepository saleRepository, HttpClient client)
        {
            _logger = logger;
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _userManager = userManager;
            _saleRepository = saleRepository;
        }

        public async Task<IActionResult> Index()
        {





            var user = await _userManager.GetUserAsync(User);
            var role = await _userManager.GetRolesAsync(user);

            if (role.Contains("seyhanlar"))
            {
                //Butun Şirketlerin Getirilmesi
                //var companies = _companyRepository.GetAllAsync().Result.Result.ToList();

                List<CompanyDto> companyDtos = new();
                using (var client = new HttpClient())
                {
                    var responseMessage = await client.GetAsync("https://localhost:44363/api/Company");
                    var jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var companyDtoDatas = JsonConvert.DeserializeObject<List<CompanyDto>>(jsonString);
                    companyDtos = companyDtoDatas;
                }




                List<IndexVM> indexVMs = new List<IndexVM>();
                foreach (var item in companyDtos)
                {

                    //sales talosunda companyId'ye karşilık gelen veriyi alma alma
                    //var sale = _saleRepository.FilterByAsync(q => q.CompanyId == item.Id).Result.Result.FirstOrDefault();
                    List<SaleDto> saleDtos = new();
                    using (var client = new HttpClient())
                    {
                        ObjectId objectId = new(item.Id.TimeStamp, item.Id.Machine, (short)item.Id.Pid, item.Id.Increment);
                        var responseMessage = await client.GetAsync("https://localhost:44363/api/Sale/FromCompanyId?id=" + objectId);
                        var jsonString = await responseMessage.Content.ReadAsStringAsync();
                        var saleDtoDatas = JsonConvert.DeserializeObject<List<SaleDto>>(jsonString);
                        saleDtos = saleDtoDatas;
                    }


                    IndexVM indexVM = new IndexVM
                    {
                        CompanyDto = item,
                        SaleDto = saleDtos.FirstOrDefault()
                    };
                    indexVMs.Add(indexVM);
                }
                return View(indexVMs);

            }
            else
            {
                //companyName ye gore company getirme
                //var companies = _companyRepository.FilterByAsync(q => q.CompanyName.ToLower() == role.First().ToLower()).Result.Result;
                CompanyDto item = new();
                using (var client = new HttpClient())
                {
                  
                    var responseMessage = await client.GetAsync("https://localhost:44363/api/Company/GetByCompanyName?companyName=" + role.FirstOrDefault());
                    var jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var companyDtoDatas = JsonConvert.DeserializeObject<CompanyDto>(jsonString);
                    item = companyDtoDatas;
                }


                List<IndexVM> indexVMs = new List<IndexVM>();

               
                    //sales talosunda companyId'ye karşilık gelen yeri alma
                    //var sale = _saleRepository.FilterByAsync(q => q.CompanyId == item.Id).Result.Result.FirstOrDefault();
                    List<SaleDto> saleDtos = new();
                    using (var client = new HttpClient())
                    {
                        ObjectId objectId = new(item.Id.TimeStamp, item.Id.Machine, (short)item.Id.Pid, item.Id.Increment);
                        var responseMessage = await client.GetAsync("https://localhost:44363/api/Sale/FromCompanyId?id=" + objectId);
                        var jsonString = await responseMessage.Content.ReadAsStringAsync();
                        var saleDtoDatas = JsonConvert.DeserializeObject<List<SaleDto>>(jsonString);
                        saleDtos = saleDtoDatas;
                    }


                    IndexVM indexVM = new IndexVM
                    {
                        CompanyDto = item,
                        SaleDto = saleDtos.FirstOrDefault()
                    };
                    indexVMs.Add(indexVM);
                
                return View(indexVMs);

            }





        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
