using Microsoft.AspNetCore.Mvc;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using SalesDemo.Helper.Connection;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers.Area.Admin
{
    public class CompanyController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            //şirketleri getir
            Result<ICollection<CompanyVM>> companyResult = new();
            HttpConnection<Result<ICollection<CompanyVM>>> companyConn = new HttpConnection<Result<ICollection<CompanyVM>>>();
            companyResult = await companyConn.GetAsync("https://localhost:44363/api/Company");



            //şirketlieri List<CompanyVM> oalrak view içerisnde Dondur

            return View(companyResult.Data);
        }

        public async Task<IActionResult> Upsert(string? companyName)
        {
            //eger companyvm içerisindeki id alanı boş ise ekleme işlemi yap
            if (companyName != null)
            {
                HttpConnection<Result<CompanyVM>> companyConn = new HttpConnection<Result<CompanyVM>>();
                var company =await companyConn.GetAsync("https://localhost:44363/api/Company/GetByCompanyName?companyName=" + companyName);
                return View(company.Data);
            }
            
            return View();
        }

        
        public IActionResult Upsert(CompanyVM companyVM)
        {

            return View();
        }
       

        public IActionResult Delete(string id)
        {
            //gelen id e gore company sil



            return View();
        }


    }
}
