using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Entities.Auth
{
    [CollectionName("Roles")]

    public class IdentityRoles : MongoIdentityRole 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public ObjectId? CompanyId{ get; set; }
    }
}
