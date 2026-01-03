using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEntity.MODEL
{
    public class ChartDataDto
    {
        public int id { get; set; }
        public string Label { get; set; }      
        public decimal Value { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
    }
}