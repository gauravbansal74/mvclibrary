using DLL;
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

        [Display(Name = "I am based in")]
        public string basedIn { get; set; }

        [Display(Name = "My role is classified as")]
        public string classifiedRole { get; set; }

        [Display(Name = "Jobseeking status")]
        public string jobseekingStatus { get; set; }

        [Display(Name = "Availability")]
        public string availability { get; set; }

        [Display(Name="Highest level of qualification")]
        public string highestEducation { get; set; }

        [Display(Name = "Work types")]
        public string workType { get; set; }

        [Display(Name = "Preferred work locations")]
        public string Preferredlocations { get; set; }

        public string ResumeFileName { get; set; }

        public bool PersonalInformationUpdated { get; set;}

        public bool QualificationSkillsUpdated { get; set;}

        public bool CurrentStatusUpdated { get; set;}

        public bool ResumeUpdated { get; set;}

    }
}