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
    
    public partial class employeeTest
    {
        public long employeeTestId { get; set; }
        public string testTitle { get; set; }
        public string testDescription { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int testDuration { get; set; }
        public long companyId { get; set; }
        public long createdBy { get; set; }
        public System.DateTime createdOn { get; set; }
        public System.DateTime modifiedOn { get; set; }
        public long modifiedBy { get; set; }
        public bool isDeleted { get; set; }
        public bool isActive { get; set; }
    
        public virtual company company { get; set; }
    }
}
