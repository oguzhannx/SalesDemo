using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SalesDemo.Entities
{

    public class Company : BaseModel
    {
        
        [BsonElement("companyName")]
        public string CompanyName { get; set; }
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("products")]
        public ICollection<Product> Products { get; set; }
    }
}
