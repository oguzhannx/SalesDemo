using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesDemo.Entities.Auth;
using SalesDemo.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<MongoIdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;



        public AccountController(UserManager<User> userManager,
            RoleManager<MongoIdentityRole> roleManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> accountController)
        {
            _logger = accountController;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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

                User user = new User
                {
                    Name = registerVM.Name,
                    Surname = registerVM.Surname,
                    UserName = registerVM.UserName,
                    PhoneNumber = registerVM.Phone,

                };

                var result = await _userManager.CreateAsync(user, registerVM.Password);
                _logger.LogInformation("User created.");

                if (result.Succeeded)
                {
                    //companyName isminde rol oluşturur
                    if (!_roleManager.RoleExistsAsync(registerVM.CompanyName.ToLower()).GetAwaiter().GetResult())
                    {
                        _roleManager.CreateAsync(new MongoIdentityRole
                        {
                            Name = registerVM.CompanyName.ToLower(),
                        }).GetAwaiter().GetResult();

                        await _userManager.AddToRoleAsync(user, registerVM.CompanyName.ToLower());
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, registerVM.CompanyName.ToLower()));
                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            _logger.LogInformation("User logged in.");

                            return LocalRedirect(returnUrl);
                        }
                    }
                    //company ismide rol zaten varsa kullanıcıya direkt o rolu ata
                    else
                    {
                        await _userManager.AddToRoleAsync(user, registerVM.CompanyName.ToLower());
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, registerVM.CompanyName.ToLower()));
                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            _logger.LogInformation("User logged in.");

                            return LocalRedirect(returnUrl);
                        }

                    }
                }
                else if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        _logger.LogInformation(error.Description);

                    }

                    // Hata mesajlarını ViewBag üzerinden kullanıcıya gönderin
                    ViewBag.RegisterErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
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
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await _userManager.FindByNameAsync(loginVM.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, true, false);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }
                }

            }
            return View();
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("user signout");

            return RedirectToAction("Login");
        }


    }
}
