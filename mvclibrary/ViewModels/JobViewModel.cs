using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mvclibrary.ViewModels
{
    public class JobViewModel
    {
        [Required(ErrorMessage = "Title is Required.")]
        [Display(Name="Title")]
        public string jobTitle { get; set; }

        [Required(ErrorMessage = "Minimum Experience is Required.")]
        [Display(Name = "Min. Exp.")]
        public int jobMinExp {get; set;}

        [Required(ErrorMessage = "Maximum Experience is Required.")]
        [Display(Name = "Max. Exp.")]
        public int jobMaxExp {get; set;}

        [Display(Name = "Do you want to disclose Salary?")]
        public bool jobDisclosed { get; set; }

        [Required(ErrorMessage = "Keywords are Required.")]
        [Display(Name = "Keywords")]
        public string jobKeyword { get; set; }

        [Required(ErrorMessage = "Minimum Salary is Required.")]
        [Display(Name = "Min. Sal.")]
        public int jobMinSalary { get; set; }

        [Required(ErrorMessage = "Maximum Salary is Required.")]
        [Display(Name = "Max. Sal.")]
        public int jobMaxSalary { get; set; }

        [Required(ErrorMessage = "Description is Required.")]
        [Display(Name = "Description")]
        public string jobDescription { get; set; }

        [Display(Name = "Do you want to enter other details about this post?")]
        public bool jobOtherDetailPresent { get; set; }

        [Display(Name = "Other  Detail")]
        public string jobOtherDetail { get; set; }

        [Required(ErrorMessage = "Company info is Required.")]
        [Display(Name = "Company Info")]
        public string jobCompanyInfo { get; set; }
  }


}