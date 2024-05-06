﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SalesDemo.Entities
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")] // MongoDB'de "_id" olarak belirtmek için
        public ObjectId Id { get; set; }
    }
}
