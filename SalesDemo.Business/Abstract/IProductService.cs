using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System.Collections;
using System.Collections.Generic;

namespace SalesDemo.Business.Abstract
{
    public interface IProductService
    {
        Result<ICollection<Product>> GetProducts();
    }
}
