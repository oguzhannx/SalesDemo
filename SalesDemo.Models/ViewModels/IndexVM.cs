using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Models.ViewModels
{
    public class IndexVM
    {
        public Company Company { get; set; }
        public Sale Sale { get; set; }
    }
}
