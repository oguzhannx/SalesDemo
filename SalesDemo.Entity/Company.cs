using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Entities
{

    public class Company:BaseModel
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Product> Products{ get; set; }
    }
}
