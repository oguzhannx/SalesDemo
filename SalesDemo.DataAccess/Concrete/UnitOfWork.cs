using Microsoft.Extensions.Options;
using SalesDemo.Core.DbSettingModels;
using SalesDemo.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IOptions<MongoSettings> _settings;
        public UnitOfWork(IOptions<MongoSettings> settings)
        {
            _settings = settings;
            Sale = new SaleRepository(_settings);
            Company = new CompanyRepository(_settings);
            Product = new ProductRepository(_settings);
            User = new UserRepository(_settings);
        }

        public ICompanyRepository Company { get; set; }
        public IProductRepository Product { get; set; }
        public ISaleRepository Sale { get; set; }
        public IUserRepository User { get; set; }
    }
}
