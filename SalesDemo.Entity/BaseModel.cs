using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Entities
{
    public class BaseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
