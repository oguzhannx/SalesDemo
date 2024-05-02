using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDemo.Entities.Auth
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<Guid>
    {
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}
