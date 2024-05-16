﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.DataAccess.Abstract
{
    public class IUnitOfWork 
    {
        ICompanyRepository Company { get; }
        IProductRepository Product { get; }
        ISaleRepository Sale{ get; }
        IUserRepository User { get; }
    }
}