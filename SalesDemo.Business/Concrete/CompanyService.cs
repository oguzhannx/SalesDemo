using SalesDemo.Business.Abstract;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SalesDemo.Business.Concrete
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public ICollection<Company> GetCompanies()
        {
            var a = _companyRepository.GetAll().Result.ToList();
            return a;
        }

        public Company GetCompanyByCompanyName(string companyName)
        {


            return _companyRepository.FilterBy(q => q.CompanyName.ToLower() == companyName.ToLower()).Result.FirstOrDefault();


        }
    }
}
