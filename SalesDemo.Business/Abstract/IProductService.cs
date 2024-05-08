using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;

namespace SalesDemo.Business.Abstract
{
    public interface IProductService
    {
        GetManyResult<Product> GetProducts();
    }
}
