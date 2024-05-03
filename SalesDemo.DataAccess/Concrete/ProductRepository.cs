﻿using Microsoft.Extensions.Options;
using SalesDemo.Core.DbSettingModels;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.DataAccess.Repository;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.DataAccess.Concrete
{
    public class ProductRepository : MongoRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IOptions<MongoSettings> settings) : base(settings)
        {
        }
    }
}