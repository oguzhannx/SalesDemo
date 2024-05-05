using Microsoft.Extensions.Options;
using SalesDemo.Core.DbSettingModels;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.DataAccess.Repository;
using SalesDemo.Entities;

namespace SalesDemo.DataAccess.Concrete
{
    public class CompanyRepository : MongoRepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(IOptions<MongoSettings> settings) : base(settings)
        {
        }
    }
}
