using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Entities
{
    public class Sale : BaseModel
    {
        public ObjectId CompanyId{ get; set; }
        public DateTime SaleDate { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
