using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;
using SalesDemo.Web.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICompanyRepository companyRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _companyRepository = companyRepository;
        }

        public async Task<IActionResult> Index()
        {
            //List<Product> products = new List<Product>
            //{
            //     new Product { Price = 10, PruductName = "kola" },
            //            new Product { Price = 15, PruductName = "kola zero" },
            //            new Product { Price = 20, PruductName = "kola light" },
            //            new Product { Price = 5, PruductName = "uzun makarna" },
            //            new Product { Price = 7, PruductName = "kelebek makarna" },
            //            new Product { Price = 9, PruductName = "fiyonk makarna" },
            //};
            //List<Company> companies = new List<Company>
            //{
            //     new Company
            //     {
            //         CompanyName = "Colo Colo",
            //         PhoneNumber = "123456",
            //         Products = new List<Product>
            //         {
            //            new Product { Price = 10, PruductName = "kola" },
            //            new Product { Price = 15, PruductName = "kola zero" },
            //            new Product { Price = 20, PruductName = "kola light" },
            //         }
            //     },
            //     new Company
            //     {
            //         CompanyName = "makarna makarna",
            //         PhoneNumber = "654321",
            //         Products = new List<Product>
            //         {
            //            new Product { Price = 5, PruductName = "uzun makarna" },
            //            new Product { Price = 7, PruductName = "kelebek makarna" },
            //            new Product { Price = 9, PruductName = "fiyonk makarna" },
            //         }
            //     }

            //};

            //foreach (var item in companies)
            //{
            //    _companyRepository.InsertOneAsync(item);
            //}
            //foreach (var item in products)
            //{
            //    _productRepository.InsertOneAsync(item);
            //}


            return View();
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
