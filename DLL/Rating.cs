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
    
    public partial class Rating
    {
        public long RatingId { get; set; }
        public long RatingValue { get; set; }
        public long JobId { get; set; }
        public long createdBy { get; set; }
        public System.DateTime createdOn { get; set; }
        public System.DateTime modifiedOn { get; set; }
        public long modifiedBy { get; set; }
    
        public virtual account account { get; set; }
        public virtual job job { get; set; }
    }
}
