using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;

namespace SalesDemo.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        public ProductService(IProductRepository productRepository,
            ICompanyRepository companyRepository)
        {
            _productRepository = productRepository;
            _companyRepository = companyRepository;
        }
        public GetManyResult<Product> GetProducts()
        {

            return _productRepository.GetAll();
        }


    }
}
