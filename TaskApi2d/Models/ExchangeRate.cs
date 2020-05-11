using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskApi2d
{
    public class ExchangeRate
    {
        [Key]
        public int ExchangeRateId { get; set; }

        public int Timestamp { get; set; }

        [StringLength(3)]
        public string BaseCurrency { get; set; }

        [StringLength(3)]
        public string TargetCurrency { get; set; }

        //the chepest currency is Venezuelan Bolivar 182754.588980 (12,6)
        //besides that EF sets decimal to (19,2) by default
        [Column(TypeName = "decimal(14,6)")]
        public decimal Rate { get; set; }
    }
}
