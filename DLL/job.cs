//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class job
    {
        public long jobId { get; set; }
        public string jobTitle { get; set; }
        public int jobMinExp { get; set; }
        public int jobMaxExp { get; set; }
        public bool jobDisclosed { get; set; }
        public string jobKeyword { get; set; }
        public long jobMinSalary { get; set; }
        public long jobMaxSalary { get; set; }
        public string jobDescription { get; set; }
        public bool jobOtherDetailPresent { get; set; }
        public string jobOtherDetail { get; set; }
        public string jobCompanyInfo { get; set; }
        public long createdBy { get; set; }
        public System.DateTime createdOn { get; set; }
        public System.DateTime modifiedOn { get; set; }
        public long modifiedBy { get; set; }
        public int jobStatus { get; set; }
        public bool jobDeteled { get; set; }
    }
}
