﻿using SalesDemo.Core.Models.Concrete;
using SalesDemo.Entities;
using System.Threading.Tasks;

namespace SalesDemo.Business.Abstract
{
    public interface ISaleService
    {
        Task<GetManyResult<Sale>> getAllSales();
        GetManyResult<Sale> getSalesByComppanyId(string id);
    }
}
