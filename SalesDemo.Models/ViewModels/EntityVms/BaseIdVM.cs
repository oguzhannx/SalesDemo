using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDemo.Models.Dtos
{
    public class BaseIdVM
    {
        public int TimeStamp { get; set; }
        public int Machine { get; set; }
        public int Increment { get; set; }
        public int Pid { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
