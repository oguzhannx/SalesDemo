using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Entities
{

    public class Product:BaseModel
    {
        public string PruductName { get; set; }
        public double Price { get; set; }

    }
}
