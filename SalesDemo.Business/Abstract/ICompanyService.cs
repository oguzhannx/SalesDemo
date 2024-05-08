using SalesDemo.Entities;
using System.Collections.Generic;

namespace SalesDemo.Business.Abstract
{
    public interface ICompanyService
    {
        ICollection<Company> GetCompanies();
        Company GetCompanyByCompanyName(string companyName);
    }
}
