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
    
    public partial class company
    {
        public company()
        {
            this.employeeTests = new HashSet<employeeTest>();
        }
    
        public long companyId { get; set; }
        public string companyName { get; set; }
        public string companyAbout { get; set; }
        public string companyLogo { get; set; }
        public string companyWebsite { get; set; }
        public long createdBy { get; set; }
        public System.DateTime createdOn { get; set; }
        public System.DateTime modifiedOn { get; set; }
        public long modifiedBy { get; set; }
    
        public virtual ICollection<employeeTest> employeeTests { get; set; }
    }
}
