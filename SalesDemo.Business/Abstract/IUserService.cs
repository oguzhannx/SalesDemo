using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Business.Abstract
{
    public interface IUserService
    {
        Result<User> getUserById(string userId);
        Result<string> getUserRole(string userId);
    }
}
