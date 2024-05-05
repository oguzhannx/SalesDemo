using MongoDB.Bson;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Business.Abstract
{
    public interface ISaleService
    {
        Task<GetManyResult<Sale>> getAllSales();
        GetOneResult<Sale> getSalesByComppanyId(ObjectId id);    }
}
