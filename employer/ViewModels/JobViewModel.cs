using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace employer.ViewModels
{
    [Serializable]
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

        [Required(ErrorMessage = "Company infomation is Required.")]
        [Display(Name = "About the Company")]
        public string jobCompanyInfo { get; set; }

        [Display(Name = "How to apply using -")]
        public bool jobApplyModeIsEmail { get; set; }

        [Display(Name = "Source Email or Website link")]
        public string jobApplyMode { get; set; }

        [Display(Name = "Expires on")]
        public DateTime jobExpireDate { get; set; }

        [Display(Name = "Location")]
        public string jobLocation { get; set; }

        [Display(Name = "Name of the Company")]
        public string jobCompnayName { get; set; }


  }


}