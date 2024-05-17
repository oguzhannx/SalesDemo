using MongoDB.Bson.Serialization.Attributes;
using SalesDemo.Models.Dtos;

namespace SalesDemo.Entities
{
    [BsonIgnoreExtraElements]

    public class SaleDetailDto
    {
        public BaseIdVM Id { get; set; }
        public ProductVM product { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
    }
}
