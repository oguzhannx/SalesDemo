using SalesDemo.Models.Dtos;
using System;
using System.Collections.Generic;

namespace SalesDemo.Entities
{
    public class SaleDto 
    {
        public BaseId Id { get; set; }
        public DateTime SaleDate { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<SaleDetailDto> SaleDetails { get; set; }
    }
}
