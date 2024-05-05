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
using System.Collections.Concurrent;
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

            using (HttpClient _client = new HttpClient())
            {

            
            
           var user = await _userManager.GetUserAsync(User);
            var role = await _userManager.GetRolesAsync(user);

            if (role.Contains("seyhanlar"))
            {
                var companies =  _companyRepository.GetAllAsync().Result.Result.ToList();


                List<IndexVM> indexVMs = new List<IndexVM>();

                foreach (var item in companies)
                {
                    HttpResponseMessage response = await _client.GetAsync($"https://localhost:44363/api/sale/{item.Id}");

                    // Yanıtın başarılı olduğunu kontrol etme
                    response.EnsureSuccessStatusCode();

                    // JSON yanıtını okuma ve model tipine dönüştürme
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<List<Sale>>(jsonResponse);

                    //var sale = _saleRepository.FilterByAsync(q => q.CompanyId == item.Id).Result.Result.FirstOrDefault(); //sales talosunda company id'ye karşilık gelen yeri alma

                    IndexVM indexVM = new IndexVM
                    {
                        Company = item,
                        Sale = new Sale { }
                    };
                    indexVMs.Add(indexVM);
                }
                return View(indexVMs);

            }
            else
            {
                var companies = _companyRepository.FilterByAsync(q => q.CompanyName.ToLower() == role.First().ToLower()).Result.Result;



                List<IndexVM> indexVMs = new List<IndexVM>();

                foreach (var item in companies)
                {
                    var sale = _saleRepository.FilterByAsync(q => q.CompanyId == item.Id).Result.Result.FirstOrDefault(); //sales talosunda company id'ye karşilık gelen yeri alma
                    IndexVM indexVM = new IndexVM
                    {
                        Company = item,
                        Sale = sale
                    };
                    indexVMs.Add(indexVM);
                }
                return View(indexVMs);

            }
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
