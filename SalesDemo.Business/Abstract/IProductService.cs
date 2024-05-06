using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using SalesDemo.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Business.Abstract
{
    public interface IProductService
    {
        GetManyResult<Product> GetProducts();
        List<ProductFromCompanyDto> GetProductsFromCompany();
    }
}
