using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SalesDemo.Entities
{

    public class Product : BaseModel
    {
      
        [BsonElement("pruductName")]
        public string PruductName { get; set; }
        [BsonElement("price")]
        public double Price { get; set; }





    }
}
