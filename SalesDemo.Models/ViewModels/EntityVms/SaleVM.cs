using SalesDemo.Models.Dtos;
using System;
using System.Collections.Generic;

namespace SalesDemo.Entities
{
    public class SaleVM
    {
        public BaseIdVM Id { get; set; }
        public DateTime SaleDate { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<SaleDetailVM> SaleDetails { get; set; }
    }
}
