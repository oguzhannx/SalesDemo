using SalesDemo.Models.Dtos;
using System.Collections.Generic;

namespace SalesDemo.Entities
{

    public class CompanyVM 
    {
        public BaseIdVM Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<ProductVM> Products { get; set; }
    }
}
