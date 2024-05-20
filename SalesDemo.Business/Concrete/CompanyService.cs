using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
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

        public Result<Company> AddCompany(Company company)
        {
            return _companyRepository.InsertOne(company);
        }

        public Result<ICollection<Company>> GetCompanies()
        {

            return _companyRepository.GetAll();
        }

        public Result<Company> GetCompanyByCompanyName(string companyName)
        {


            var a = _companyRepository.FilterBy(q => q.CompanyName.ToLower() == companyName.ToLower());

            Result<Company> result = new();
            result.Message = a.Message;
            result.Data = a.Data.FirstOrDefault();

            return result;


        }
    }
}
