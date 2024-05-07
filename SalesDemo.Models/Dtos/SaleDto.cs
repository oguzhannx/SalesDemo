using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace SalesDemo.Entities
{
    public class SaleDto 
    {
        public string Id { get; set; }
        public DateTime SaleDate { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<SaleDetailDto> SaleDetails { get; set; }
    }
}
