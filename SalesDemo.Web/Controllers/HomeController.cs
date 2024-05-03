using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
            ISaleRepository saleRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _userManager = userManager;
            _saleRepository = saleRepository;
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

            //List<Sale> sale = new List<Sale>
            //{
            //    new Sale
            //    {
            //        CompanyId = ObjectId.Parse("6633a1bb111f9b176fe12b89"),
            //        SaleDate = DateTime.Now,
            //        TotalPrice = 0,
            //        SaleDetails = new List<SaleDetail>
            //        {
            //            new SaleDetail
            //            {
            //                Count = 2,
            //                product = new Product
            //                {
            //                    Id = ObjectId.Parse("66339e9cc3017476e6ba8b01"),
            //                    Price = 5,
            //                    PruductName = "uzun makarna"
            //                },
            //                Total = 10,    
            //            },
            //            new SaleDetail
            //            {
            //                Count = 3,
            //                product = new Product 
            //                { 
            //                    Price = 7,
            //                    PruductName = "kelebek makarna",
            //                    Id = ObjectId.Parse("66339e9cc3017476e6ba8b02")},
            //                Total = 21,    
            //            }
            //        },  
            //    },
            //    new Sale
            //    {
            //        CompanyId = ObjectId.Parse("6633a1bb111f9b176fe12b88"),
            //        SaleDate = DateTime.Now,
            //        TotalPrice = 0,
            //        SaleDetails = new List<SaleDetail>
            //        {
            //            new SaleDetail
            //            {
            //                Count = 2,
            //                product = new Product
            //                {
            //                    Id = ObjectId.Parse("66339e99c3017476e6ba8afe"),
            //                    Price = 10,
            //                    PruductName = "kola"
            //                },
            //                Total = 20,    
            //            },
            //            new SaleDetail
            //            {
            //                Count = 3,
            //                product = new Product 
            //                { 
            //                    Price = 15,
            //                    PruductName = "kola zero",
            //                    Id = ObjectId.Parse("66339e9cc3017476e6ba8aff")},
            //                Total = 45,    
            //            }
            //        },  
            //    }

            //};
            //await _saleRepository.InsertManyAsync(sale);
            

            var companies =   _companyRepository.GetAllAsync().Result.Result.ToList();
           //var sales = _saleRepository.GetAllAsync().Result.Result.ToList();

           // foreach (var item in sales)
           // {
           //     foreach (var a in item.SaleDetails)
           //     {
           //         item.TotalPrice += a.Total; 
           //     }
           // }


            List<IndexVM> indexVMs = new List<IndexVM>();

            foreach (var item in companies)
            {
               var sale = _saleRepository.FilterByAsync(q => q.CompanyId == item.Id).Result.Result.FirstOrDefault(); //sales talosunda company id'ye karşilık gelen yeri alma
                IndexVM indexVM = new IndexVM { 
                Company = item, Sale = sale};
                indexVMs.Add(indexVM);
            }




            return View(indexVMs);
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
