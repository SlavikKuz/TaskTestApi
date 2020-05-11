using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskApi2d.Ui.Models
{
    public class ExchangeRateViewModel
    {
        public int Timestamp { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Rate { get; set; }
    }
}
