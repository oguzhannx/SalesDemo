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

namespace SalesDemo.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtModel jwtModel;
        public AccountController(
            IUserRepository userRepository,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IOptions<JwtModel> options)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;            
            _userManager = userManager;
            jwtModel= options.Value;

        }
        [HttpPost("register")]
        public IActionResult Register(RegisterVM registerVM)
        {





            return Ok();
        }



        [HttpPost("token")]
        public IActionResult Token([FromBody]LoginVM loginVm)
        {
            var a = jwtModel;


            //kullanıcının bulunması
            var user = _userRepository.FilterBy(q => q.NormalizedUserName == loginVm.UserName.ToUpper()).Data.First();
            
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
