using Microsoft.Extensions.Options;
using SalesDemo.Core.DbSettingModels;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.DataAccess.Repository;
using SalesDemo.Entities;
using SalesDemo.Entities.Auth;

namespace SalesDemo.DataAccess.Concrete
{
    public class UserRepository : MongoRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IOptions<MongoSettings> settings) : base(settings)
        {

        }

    }
}
