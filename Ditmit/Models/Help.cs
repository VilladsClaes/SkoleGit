//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ditmit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Help
    {
        public int HelpID { get; set; }
        public string HelpText { get; set; }
        public Nullable<int> HelpType_FK { get; set; }
        public Nullable<System.DateTime> HelpDate { get; set; }
    
        public virtual HelpType HelpType { get; set; }
    }
}
