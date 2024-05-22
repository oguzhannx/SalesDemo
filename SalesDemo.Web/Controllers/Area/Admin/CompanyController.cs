using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using SalesDemo.Helper.Connection;
using System;
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
                var company = await companyConn.GetAsync("https://localhost:44363/api/Company/GetByCompanyName?companyName=" + companyName);
                return View(company.Data);
            }
            CompanyVM companyVM = new CompanyVM
            {
                CompanyName = "",
                PhoneNumber = "",

            };
            return View(companyVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertAsync(CompanyVM companyVm)
        {

            Company company = new Company
            {
                CompanyName = companyVm.CompanyName,
                PhoneNumber = companyVm.PhoneNumber,
                Products = null,
            };


            //companyid Null ise ekleme yapsın
            if (companyVm.CompanyId == null)
            {
                HttpConnection<Company> connection = new HttpConnection<Company>();
                var a = await connection.PostAsync("https://localhost:44363/api/Company", company);
            }
            //Company id null değilse guncelleme yapsın
            else
            {
                //https://localhost:44363/api/Company?id=asdasd
                HttpConnection<Company> connection = new HttpConnection<Company>();
                var a = await connection.PutAsync("https://localhost:44363/api/Company?id=" + companyVm.CompanyId, company);
            }
            return RedirectToAction("index");
        }


      
        public IActionResult Delete(string companyId)
        {
            //gelen id e gore company sil
            HttpConnection<Company> connection = new HttpConnection<Company>();
            var a = connection.DeleteAsync("https://localhost:44363/api/Company?id=" + companyId);


            return RedirectToAction("Index");
        }


    }
}
