//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductTable()
        {
            this.ColorProductRelationTables = new HashSet<ColorProductRelationTable>();
            this.SizeProductRelationTables = new HashSet<SizeProductRelationTable>();
            this.PictureTables = new HashSet<PictureTable>();
        }
    
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<double> ProductPrice { get; set; }
        public Nullable<int> ProductBrandID { get; set; }
        public string ProductBeskrivelse { get; set; }
    
        public virtual BrandTable BrandTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ColorProductRelationTable> ColorProductRelationTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SizeProductRelationTable> SizeProductRelationTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PictureTable> PictureTables { get; set; }
    }
}