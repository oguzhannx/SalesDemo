using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using SalesDemo.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers
{
    public class TestController : Controller
    {


        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var responseMessage =await client.GetAsync("https://localhost:44363/api/Sale");
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var values = BsonSerializer.Deserialize<List<Sale>>(jsonString);

                return View();


            }
        }
    }
}
