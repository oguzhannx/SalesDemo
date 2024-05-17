using MongoDB.Bson;
using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;
using System.Collections.Generic;

namespace SalesDemo.Business.Concrete
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        //butun satışları getiren 
        public Result<ICollection<Sale>> getAllSales() => _saleRepository.GetAll();

        //şirket id sine göre satışları getiren
        public Result<ICollection<Sale>> getSalesByComppanyId(string id) => _saleRepository.FilterBy(q => q.CompanyId == ObjectId.Parse(id));

    }
}
