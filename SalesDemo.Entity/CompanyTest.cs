using System.Collections.Generic;

namespace SalesDemo.Entities
{

    public class CompanyTest
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
