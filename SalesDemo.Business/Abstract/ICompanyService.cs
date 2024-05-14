using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System.Collections.Generic;

namespace SalesDemo.Business.Abstract
{
    public interface ICompanyService
    {
        Result<ICollection<Company>> GetCompanies();
        Result<Company> GetCompanyByCompanyName(string companyName);
    }
}
