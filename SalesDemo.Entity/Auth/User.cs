using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace SalesDemo.Entities.Auth
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
