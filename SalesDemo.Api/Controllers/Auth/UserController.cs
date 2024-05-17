using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities.Auth;
using System;
using System.Linq;

namespace SalesDemo.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;


        public UserController(IUserRepository userRepository, UserManager<User> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        [HttpGet("role")]
        public string getUserRole(string userId)
        {
            var user = userRepository.GetById(userId, "guid").Data;
            var role = userManager.GetRolesAsync(user).Result.FirstOrDefault();
            var res = new Result<string>();
            res.Time = DateTime.Now;
            res.Message = "data getirildi";
            res.StatusCode = 200;
            res.Data = role;

            return role;
        }

        [HttpGet]
        public Result<User> getUserById(string userId)
        {
            return userRepository.GetById(userId);
        }
    }
}
