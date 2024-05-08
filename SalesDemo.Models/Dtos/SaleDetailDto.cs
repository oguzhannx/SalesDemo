using MongoDB.Bson.Serialization.Attributes;
using SalesDemo.Models.Dtos;

namespace SalesDemo.Entities
{
    [BsonIgnoreExtraElements]

    public class SaleDetailDto 
    {
        public BaseId Id { get; set; }
        public ProductDto product { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
    }
}
