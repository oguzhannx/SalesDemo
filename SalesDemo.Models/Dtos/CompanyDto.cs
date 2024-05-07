using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SalesDemo.Entities
{

    public class CompanyDto
    {
        public string Id { get; set; }

        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
