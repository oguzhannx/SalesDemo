using Microsoft.AspNetCore.Identity;
using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public Result<User> getUserById(string userId)
        {
            return _userRepository.GetById(userId);
        }

        public  Result<string> getUserRole(string userId)
        {
            var user = _userRepository.GetById(userId).Data;
            var role =  _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            
            return new Result<string>(int.Parse(HttpStatusCode.OK.ToString()), "veri getirildi", role, DateTime.Now);
        }
    }
}
