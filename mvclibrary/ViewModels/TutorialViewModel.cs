using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvclibrary.ViewModels
{
    public class TutorialViewModel
    {
        public List<tutorialCategory> tutorialCategoryList { get; set; }
        public List<video> videoList { get; set; }
    }
}