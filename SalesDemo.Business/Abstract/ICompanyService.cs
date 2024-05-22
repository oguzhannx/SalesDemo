using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System.Collections.Generic;

namespace SalesDemo.Business.Abstract
{
    public interface ICompanyService
    {
        Result<ICollection<Company>> GetCompanies();
        Result<Company> AddCompany(Company company);
        Result<Company> UpdateCompany(Company company, string id);
        Result<Company> DeleteCompany(string id);
        Result<Company> GetCompanyByCompanyName(string companyName);
    }
}
