using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesDemo.Entities.Auth;
using SalesDemo.Models.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers
{
    
    public class AccountController : Controller
    {
    
        private readonly ILogger<AccountController> _logger;



        public AccountController(
        
            ILogger<AccountController> accountController)
        {
            _logger = accountController;
        
        }

        public IActionResult Register()
        {


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("/home/index");

            if (ModelState.IsValid)
            {

                using (HttpClient client = new HttpClient())
                {
                    // Veriyi JSON formatına çevirme
                    string jsonData = JsonSerializer.Serialize(registerVM);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44363/api/Account/register", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();

                        // JWT'yi bir cookie'ye yerleştirme
                        Response.Cookies.Append("jwt", token, new CookieOptions
                        {
                            HttpOnly = true, // Cookie'ye JavaScript erişimini engeller
                            Secure = true,   // Sadece HTTPS üzerinden iletilir
                            SameSite = SameSiteMode.Strict, // CSRF saldırılarını önlemek için güçlendirilmiş güvenlik
                            Expires = DateTime.UtcNow.AddDays(1) // Cookie'nin son kullanma tarihi 
                        });

                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }

                }

                }
                return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM, string returnUrl = null)
        {
            returnUrl ??= Url.Content("/home/index");

            if (ModelState.IsValid)
            {

                using (HttpClient client = new HttpClient())
                {
                    // Veriyi JSON formatına çevirme
                    string jsonData = JsonSerializer.Serialize(loginVM);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44363/api/Account/token", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();

                        // JWT'yi bir cookie'ye yerleştirme
                        Response.Cookies.Append("jwt", token, new CookieOptions
                        {
                            HttpOnly = true, // Cookie'ye JavaScript erişimini engeller
                            Secure = true,   // Sadece HTTPS üzerinden iletilir
                            SameSite = SameSiteMode.Strict, // CSRF saldırılarını önlemek için güçlendirilmiş güvenlik
                            Expires = DateTime.UtcNow.AddDays(1) // Cookie'nin son kullanma tarihi 
                        });

                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);



                    }

                }


            }
            return View();
        }

        public IActionResult LogoutAsync()
        {

            Response.Cookies.Delete("jwt");


            _logger.LogInformation("user signout");

            return RedirectToAction("Login");
        }


    }
}
