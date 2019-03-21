using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace NewsSite.Models
{
    public class ViewModelFile
    {
        public List<FileInfo> MyFileInfo { get; set; }
        public string Msg { get; set; }
    }
}