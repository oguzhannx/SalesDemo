using Microsoft.AspNetCore.Mvc;

namespace SalesDemo.Api.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
