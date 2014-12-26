using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvclibrary.ViewModels
{
    public class VideoViewModel
    {
        public video currentVideo { get; set; }
        public List<video> videoList { get; set; }
    }
}