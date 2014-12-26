using DLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvclibrary.ViewModels
{
    public class ProfileViewModel
    {
        public UserProfileViewModel UserPersonalInformation { get; set; }

        [Display(Name = "Highest level of qualification")]
        public List<highestEducation> EducationList { get; set; }

        public Int64 myeducation { get; set; }
    }

}