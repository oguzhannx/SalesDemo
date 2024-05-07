﻿using SalesDemo.Business.Abstract;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.DataAccess.Concrete;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
