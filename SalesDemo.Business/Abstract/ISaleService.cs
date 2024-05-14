using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDemo.Business.Abstract
{
    public interface ISaleService
    {
        Result<ICollection<Sale>> getAllSales();
        Result<ICollection<Sale>> getSalesByComppanyId(string id);
    }
}
