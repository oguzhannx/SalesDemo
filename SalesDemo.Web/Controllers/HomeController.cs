using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using SalesDemo.Helper.Connection;
using SalesDemo.Models.ViewModels;
using SalesDemo.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        [Obsolete]
        public async Task<IActionResult> Index()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(Request.Cookies["jwt"]) as JwtSecurityToken;


            // Claims (iddialar) JSON olarak okuma
            var claimsJson = new JObject();
            foreach (var claim in jsonToken.Claims)
            {
                claimsJson.Add(claim.Type, claim.Value);
            }

            var role = claimsJson["role"].ToString();

            ViewBag.UserName = claimsJson["unique_name"].ToString();
            ViewBag.Role = claimsJson["role"].ToString();

            if (role.Contains("seyhanlar"))
            {
                //Butun Şirketlerin Getirilmesi
                Result<ICollection<CompanyVM>> companyResult = new();

                HttpConnection<Result<ICollection<CompanyVM>>> companyConn = new HttpConnection<Result<ICollection<CompanyVM>>>();
                companyResult = await companyConn.GetAsync("https://localhost:44363/api/Company", "Authorization", "Bearer " + Request.Cookies["jwt"]);




                List<IndexVM> indexVMs = new List<IndexVM>();
                foreach (var item in companyResult.Data)
                {
                    //sales talosunda companyId'ye karşilık gelen veriyi alma alma
                    Result<ICollection<SaleVM>> saleDtos = new();
                    ObjectId objectId = new(item.Id.TimeStamp, item.Id.Machine, (short)item.Id.Pid, item.Id.Increment);

                    HttpConnection<Result<ICollection<SaleVM>>> SaleConn = new HttpConnection<Result<ICollection<SaleVM>>>();
                    saleDtos = await SaleConn.GetAsync("https://localhost:44363/api/Sale/FromCompanyId?id=" + objectId.ToString(), "Authorization", "Bearer " + Request.Cookies["jwt"]);

                    IndexVM indexVM = new IndexVM
                    {
                        CompanyVM = item,
                        SaleVM = saleDtos.Data.First(),
                    };
                    indexVMs.Add(indexVM);
                }
                return View(indexVMs);



            }
            else
            {
                //companyName ye gore sirketi getirme
                Result<CompanyVM> item = new();

                HttpConnection<Result<CompanyVM>> companyConn = new HttpConnection<Result<CompanyVM>>();
                item = await companyConn.GetAsync("https://localhost:44363/api/Company/GetByCompanyName?companyName=" + role, "Authorization", "Bearer " + Request.Cookies["jwt"]);





                //sales talosunda companyId'ye karşilık gelen yeri alma
                Result<ICollection<SaleVM>> saleDtos = new();
                ObjectId objectId = new(item.Data.Id.TimeStamp, item.Data.Id.Machine, (short)item.Data.Id.Pid, item.Data.Id.Increment); //companyId yi objectId ye çevirme
                HttpConnection<Result<ICollection<SaleVM>>> saleConn = new HttpConnection<Result<ICollection<SaleVM>>>();
                saleDtos = await saleConn.GetAsync("https://localhost:44363/api/Sale/FromCompanyId?id=" + objectId, "Authorization", "Bearer " + Request.Cookies["jwt"]);



                IndexVM indexVM = new IndexVM
                {
                    CompanyVM = item.Data,
                    SaleVM = saleDtos.Data.FirstOrDefault()
                };

                List<IndexVM> indexVMs = new List<IndexVM>();
                indexVMs.Add(indexVM);
                return View(indexVMs);
            }


        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
