using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Entities
{
    public class SaleDetail:BaseModel
    {
        public Product product{ get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
    }
}
