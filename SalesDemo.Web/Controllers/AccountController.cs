using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesDemo.Helper.Connection;
using SalesDemo.Models.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                //kayıt işleminin yapılması
                HttpConnection<RegisterVM> connection = new HttpConnection<RegisterVM>();
                var postResult = await connection.register("https://localhost:44363/api/Account/register", registerVM);

                //kayıt işleminin sonucu true ise giriş yapsın
                if (postResult)
                {
                    LoginVM loginVM = new LoginVM
                    {
                        UserName = registerVM.UserName,
                        Password = registerVM.Password,
                    };


                    HttpConnection<LoginVM> login = new HttpConnection<LoginVM>();

                    var response = await login.tokenAsync("https://localhost:44363/api/Account/token", loginVM, HttpContext);



                    if (response)
                    {
                        return LocalRedirect(returnUrl);

                    }

                }



            }
            return View();
        }

        public IActionResult Login()
        {


            if (Request.Cookies["jwt"] != null)
            {
                // JWT'yi çözme eğer jwt suresi dolmamış ise direk yonlendir
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = (handler.ReadToken(Request.Cookies["jwt"]) as JwtSecurityToken);
                var exp = jsonToken.Claims.First(q => q.Type.Equals("exp")).Value;
                var ticks = long.Parse(exp);
                var tokenDate = DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;
                var now = DateTime.Now.ToUniversalTime();
                if ((tokenDate >= now))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

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
                HttpConnection<LoginVM> login = new HttpConnection<LoginVM>();
                var response = await login.tokenAsync("https://localhost:44363/api/Account/token", loginVM, HttpContext);

                if (response)
                {
                    return LocalRedirect(returnUrl);

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
