using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                var responseMessage =await client.GetAsync("https://localhost:44363/api/Company/WithoutProducts");
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                //var values = JsonConvert.DeserializeObject<List<CompanyWithoutProductsDto>>(jsonString);

                return View();


            }
        }
    }
}
