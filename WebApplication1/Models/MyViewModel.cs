using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MyViewModel
    {
        
        //Oplist filer som uploades via Admin-index
        public List<FileInfo> MyFileInfo { get; set; }
        public FileInfo MyFile { get; set; }
        public string Msg { get; set; }
        

    }
}