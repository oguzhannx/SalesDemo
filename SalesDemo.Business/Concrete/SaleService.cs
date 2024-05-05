using MongoDB.Bson;
using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;
using System.Threading.Tasks;

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
        public async Task<GetManyResult<Sale>> getAllSales() => await _saleRepository.GetAllAsync();

        //şirket id sine göre satışları getiren
        public GetManyResult<Sale> getSalesByComppanyId(string id)
        {
            var a = _saleRepository.FilterBy(q => q.CompanyId == ObjectId.Parse(id));
            return a;
        }
    }
}
