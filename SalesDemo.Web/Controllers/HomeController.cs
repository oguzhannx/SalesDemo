﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;
using SalesDemo.Entities.Auth;
using SalesDemo.Models.ViewModels;
using SalesDemo.Web.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
                var companies = _companyRepository.GetAllAsync().Result.Result.ToList();


                List<IndexVM> indexVMs = new List<IndexVM>();
                foreach (var item in companies)
                {

                    //sales talosunda companyId'ye karşilık gelen veriyi alma alma
                    var sale = _saleRepository.FilterByAsync(q => q.CompanyId == item.Id).Result.Result.FirstOrDefault();



                    IndexVM indexVM = new IndexVM
                    {
                        Company = item,
                        Sale = sale
                    };
                    indexVMs.Add(indexVM);
                }
                return View(indexVMs);

            }
            else
            {
                //companyName ye gore company getirme
                var companies = _companyRepository.FilterByAsync(q => q.CompanyName.ToLower() == role.First().ToLower()).Result.Result;



                List<IndexVM> indexVMs = new List<IndexVM>();

                foreach (var item in companies)
                {
                    //sales talosunda companyId'ye karşilık gelen yeri alma
                    var sale = _saleRepository.FilterByAsync(q => q.CompanyId == item.Id).Result.Result.FirstOrDefault();

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
