using SalesDemo.Core.Repository.Abstract;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
