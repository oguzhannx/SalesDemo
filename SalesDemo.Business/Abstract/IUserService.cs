using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities.Auth;

namespace SalesDemo.Business.Abstract
{
    public interface IUserService
    {
        Result<User> getUserById(string userId);
        Result<string> getUserRole(string userId);
    }
}
