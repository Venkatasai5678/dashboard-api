using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEntity.MODEL
{
    public class EmployeeName
    {
        [Key]
        public int EmployeeeId { get; set; }
        public string? Employeename { get; set; }
    }
}
