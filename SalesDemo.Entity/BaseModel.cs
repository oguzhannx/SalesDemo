using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SalesDemo.Entities
{
    public class BaseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
