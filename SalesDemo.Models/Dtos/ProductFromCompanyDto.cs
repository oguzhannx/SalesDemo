using MongoDB.Bson;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Models.Dtos
{
    public class ProductFromCompanyDto
    {
        public string CompanyName{ get; set; }
        public string CompanyId{ get; set; }
        public ICollection<Product> Products{ get; set; }
    }
}
