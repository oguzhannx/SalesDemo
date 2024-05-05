﻿using MongoDB.Bson;
using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public GetOneResult<Sale> getSalesByComppanyId(ObjectId id)
        {

            return  _saleRepository.GetById(id.ToString());
        }
    }
}