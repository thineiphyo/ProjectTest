using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.Core.Entities
{
    public class InterestRule
    {
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get;  set; }
        [Required(ErrorMessage = "Rule Id is required.")]
        public string RuleId { get;  set; }

        [Range(0, 100, ErrorMessage = "Rate must be between 0 and 100.")]
        public decimal Rate { get;  set; }
       
    }
}
