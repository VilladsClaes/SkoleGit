using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductWeb.Models
{
    public class AdminProductViewModel
    {
        public ProductTable Product { get; set; }
        public List<kategoriTable> KategoriListe { get; set; }
    }
}