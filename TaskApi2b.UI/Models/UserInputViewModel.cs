using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi2b.UI.Models
{
    public class UserInputViewModel
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        [Display(Name = "Base Currency (EUR)")]
        public string BaseCurrency { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [Display(Name = "Target Currency")]
        public string TargetCurrency { get; set; }

        [Required]
        //[DataType(DataType.Currency)]
        //[Range(double.Epsilon, double.MaxValue)]
        [Display(Name = "Amount")]
        public string Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime? Date { get; set; }

    }
}
