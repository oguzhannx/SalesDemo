using SalesDemo.Models.Dtos;
using System.Collections.Generic;

namespace SalesDemo.Entities
{

    public class CompanyDto 
    {
        public BaseId Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
