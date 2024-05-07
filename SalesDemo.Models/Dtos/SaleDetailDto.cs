using MongoDB.Bson.Serialization.Attributes;

namespace SalesDemo.Entities
{
    [BsonIgnoreExtraElements]

    public class SaleDetailDto
    {
        public string Id { get; set; }
        public ProductDto product { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
    }
}
