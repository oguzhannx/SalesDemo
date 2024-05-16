using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesDemo.Models.ViewModels;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities.Auth;
using Microsoft.Extensions.Configuration;
using SalesDemo.DataAccess.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.Extensions.Options;
using SalesDemo.Core.Models.Auth;
using AspNetCore.Identity.MongoDbCore.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SalesDemo.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<MongoIdentityRole> _roleManager;
        private readonly JwtModel jwtModel;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IUserRepository userRepository,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IOptions<JwtModel> options,
            ILogger<AccountController> logger,
            RoleManager<MongoIdentityRole> roleManager
            )
        {
            _userRepository = userRepository;
            _signInManager = signInManager;            
            _userManager = userManager;
            jwtModel= options.Value;
            _logger = logger;
            _roleManager = roleManager;

        }
        [HttpPost("register")]
        public  IActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {

                User user = new User
                {
                    Name = registerVM.Name,
                    Surname = registerVM.Surname,
                    UserName = registerVM.UserName,
                    PhoneNumber = registerVM.Phone,

                };

                var result =  _userManager.CreateAsync(user, registerVM.Password).Result;
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

                         _userManager.AddToRoleAsync(user, registerVM.CompanyName.ToLower());
                        if (result.Succeeded)
                        {
                            //await _signInManager.SignInAsync(user, isPersistent: false);
                            //_logger.LogInformation("User logged in.");

                            return Ok();
                        }
                    }
                    //company ismide rol zaten varsa kullanıcıya direkt o rolu ata
                    else
                    {
                         _userManager.AddToRoleAsync(user, registerVM.CompanyName.ToLower());
                        if (result.Succeeded)
                        {
                            //await _signInManager.SignInAsync(user, isPersistent: false);
                            //_logger.LogInformation("User logged in.");

                            return Ok();
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
                    return BadRequest(result.Errors);


                }

                return BadRequest();



            }
            return BadRequest();

        }



        [HttpPost("token")]
        public IActionResult Token([FromBody]LoginVM loginVm)
        {
            var a = jwtModel;


            //kullanıcının bulunması
            var user = _userRepository.FilterBy(q => q.UserName == loginVm.UserName).Data.First();
            
            //kullıcının rolunun bulunması
            var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            //giriş yapabiliyor mu diye kontrol etme
            var canLogin = _signInManager.CheckPasswordSignInAsync(user, loginVm.Password, false).Result;


            if (canLogin.Succeeded)
            {
                // JWT'nin oluşturulması
                var issuer = jwtModel.Issuer;
                var audience = jwtModel.Audience;
                var key = Encoding.ASCII.GetBytes
                (jwtModel.Key);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("UserName", user.UserName.ToString()),
                    new Claim("PhoneNumber", user.PhoneNumber.ToString()),
                    new Claim("Name", user.Name.ToString()),
                    new Claim("Surname", user.Surname.ToString()),
                    new Claim("Role", role.ToString()),

                 }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var stringToken = tokenHandler.WriteToken(token);




                return Ok(stringToken);

            }
            else
            {
                return BadRequest();
            }







        }


    }
}
