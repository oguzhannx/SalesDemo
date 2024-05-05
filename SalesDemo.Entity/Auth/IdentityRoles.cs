using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace SalesDemo.Entities.Auth
{
    [CollectionName("Roles")]

    public class IdentityRoles : MongoIdentityRole
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public ObjectId? CompanyId { get; set; }
    }
}
