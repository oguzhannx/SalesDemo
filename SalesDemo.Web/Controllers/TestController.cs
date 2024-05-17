using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers
{
    public class MyModel
    {
        public string cookie { get; set; }
        public string message { get; set; }
        public string errmsg { get; set; }
    }
    public class TestController : Controller
    {


        public async Task<IActionResult> Index()
        {

            HttpClientHandler handler = new HttpClientHandler(); 
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (HttpClient client = new HttpClient(handler))
            {

                var a = Request.Cookies["jwt"];

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Request.Cookies["jwt"]);
                var responseMessage = await client.GetAsync("https://10.60.60.141:44363/api/test");
                var jsonString = await responseMessage.Content.ReadAsStringAsync();

                MyModel vm = new MyModel
                {
                    cookie = a,
                    message = jsonString,
                    errmsg = responseMessage.ReasonPhrase
                };
                return View(vm);

            }
        }

    }
}