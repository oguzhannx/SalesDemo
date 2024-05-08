using System.Collections.Generic;

namespace SalesDemo.Entities
{

    public class Company : BaseModel
    {

        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
