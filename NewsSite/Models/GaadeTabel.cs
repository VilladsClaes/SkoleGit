//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewsSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GaadeTabel
    {
        public int GaadeID { get; set; }
        public string GaadeSpoergsmaal { get; set; }
        public string GaadeSvar { get; set; }
        public int FK_Kategori { get; set; }
    
        public virtual KategoriTabel KategoriTabel { get; set; }
    }
}
