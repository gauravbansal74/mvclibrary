using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvclibrary.ViewModels
{
    public class BankViewModel
    {
        [Display(Name = "Account Number")]
        public string accountnumber { get; set; }
        [Display(Name = "Account Holder Name")]
        public string accountholdername { get; set; }
        [Display(Name = "Branch IFSC Code")]
        public string accountifsccode { get; set; }
    }
}