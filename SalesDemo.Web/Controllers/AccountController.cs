using Microsoft.AspNetCore.Mvc;

namespace SalesDemo.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
