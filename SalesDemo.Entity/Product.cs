using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SalesDemo.Entities
{

    public class Product : BaseModel
    {
      
        public string PruductName { get; set; }
        public double Price { get; set; }





    }
}
