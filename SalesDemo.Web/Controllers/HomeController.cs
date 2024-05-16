using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;
using SalesDemo.Entities.Auth;
using SalesDemo.Models.ViewModels;
using SalesDemo.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
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

        public async Task<IActionResult> Index()
        {
            // JWT'yi çözme
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(Request.Cookies["jwt"]) as JwtSecurityToken;

            // Claims (iddialar) JSON olarak okuma
            var claimsJson = new JObject();
            foreach (var claim in jsonToken.Claims)
            {
                claimsJson.Add(claim.Type, claim.Value);
            }
                      
            var role = claimsJson["Role"].ToString();
            
            ViewBag.UserName = claimsJson["UserName"].ToString();
            ViewBag.Role = claimsJson["Role"].ToString();

            if (role.Contains("seyhanlar"))
            {
                //Butun Şirketlerin Getirilmesi
                Result<ICollection<CompanyVM>> companyResult = new();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
                    var responseMessage = await client.GetAsync("https://localhost:44363/api/Company");
                    var jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Result<ICollection<CompanyVM>>>(jsonString);
                    companyResult = result;
                }
                if (companyResult != null)
                {
                    List<IndexVM> indexVMs = new List<IndexVM>();
                    foreach (var item in companyResult.Data)
                    {
                        //sales talosunda companyId'ye karşilık gelen veriyi alma alma
                        Result<ICollection<SaleVM>> saleDtos = new();
                        using (var client = new HttpClient())
                        {
                            ObjectId objectId = new(item.Id.TimeStamp, item.Id.Machine, (short)item.Id.Pid, item.Id.Increment);
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
                            var responseMessage = await client.GetAsync("https://localhost:44363/api/Sale/FromCompanyId?id=" + objectId.ToString());
                            var jsonString = await responseMessage.Content.ReadAsStringAsync();
                            var saleDtoDatas = JsonConvert.DeserializeObject<Result<ICollection<SaleVM>>>(jsonString);
                            saleDtos = saleDtoDatas;
                        }
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
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

            }
            else
            {
                //companyName ye gore company getirme
                Result<CompanyVM> item = new();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
                    var responseMessage = await client.GetAsync("https://localhost:44363/api/Company/GetByCompanyName?companyName=" + role);
                    var jsonString = await responseMessage.Content.ReadAsStringAsync();
                    var companyVMDatas = JsonConvert.DeserializeObject<Result<CompanyVM>>(jsonString);
                    item = companyVMDatas;
                }
                List<IndexVM> indexVMs = new List<IndexVM>();             
                    //sales talosunda companyId'ye karşilık gelen yeri alma
                    Result<ICollection<SaleVM>> saleDtos = new();
                    using (var client = new HttpClient())
                    {
                    ObjectId objectId = new(item.Data.Id.TimeStamp, item.Data.Id.Machine, (short)item.Data.Id.Pid, item.Data.Id.Increment);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
                    var responseMessage = await client.GetAsync("https://localhost:44363/api/Sale/FromCompanyId?id=" + objectId);
                        var jsonString = await responseMessage.Content.ReadAsStringAsync();
                        var saleDtoDatas = JsonConvert.DeserializeObject<Result<ICollection<SaleVM>>>(jsonString);
                        saleDtos = saleDtoDatas;
                    }
                    IndexVM indexVM = new IndexVM
                    {
                        CompanyVM = item.Data,
                        SaleVM = saleDtos.Data.FirstOrDefault()
                    };
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
