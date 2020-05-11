using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi2d.UI.Models
{
    public class DateFromToViewModel
    {
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }
    }
}
