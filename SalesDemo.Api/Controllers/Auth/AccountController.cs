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

namespace SalesDemo.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AccountController(IConfiguration config,
            IUserRepository userRepository,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _config = config;
            _userRepository = userRepository;
            _signInManager = signInManager;            
            _userManager = userManager;

        }
        [HttpPost]
        public IActionResult Register(RegisterVM registerVM)
        {





            return Ok();
        }



        [HttpPost("token")]
        public IActionResult Token([FromBody]LoginVM loginVm)
        {
          

            //kullanıcının bulunması
            var user = _userRepository.FilterBy(q => q.NormalizedUserName == loginVm.UserName.ToUpper()).Data.First();
            
            //kullıcının rolunun bulunması
            var role = _userManager.GetRolesAsync(user).Result;

            //giriş yapabiliyor mu diye kontrol etme
            var canLogin = _signInManager.CheckPasswordSignInAsync(user, loginVm.Password, false).Result;


            if (canLogin.Succeeded)
            {
                // JWT'nin oluşturulması
                var issuer = _config.GetSection("Jwt")["Issuer"];
                var audience = _config.GetSection("Jwt")["Audience"];
                var key = Encoding.ASCII.GetBytes
                (_config.GetSection("Jwt")["Key"]);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id", Guid.NewGuid().ToString()),

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
