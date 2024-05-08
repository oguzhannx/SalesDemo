using MongoDB.Bson;
using SalesDemo.Models.Dtos;

namespace SalesDemo.Entities
{

    public class ProductDto 
    {
        public BaseId Id { get; set; }

        public string PruductName { get; set; }
        public double Price { get; set; }




    }

}
