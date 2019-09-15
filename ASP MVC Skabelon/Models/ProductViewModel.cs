using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ASP_MVC_Skabelon.Models
{
    public class ProductViewModel
    {
        public ProductTable Product { get; set; }
        public ProductPhotoTable Photo { get; set; }
        public List<ProductTable> Products { get; set; }
        public List<ProductCategoryTable> Categories { get; set; }
        public List<ProductPhotoTable> Photos { get; set; }


        //Brug sammen med IOTOOLS
        public List<FileInfo> MyFileInfo { get; set; }
        public string Msg { get; set; }
    }
}