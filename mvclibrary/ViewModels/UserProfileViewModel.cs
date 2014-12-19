using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvclibrary.ViewModels
{
    public class UserProfileViewModel
    {
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Phone")]
        public string phoneNumber { get; set; }

        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }
    }
}