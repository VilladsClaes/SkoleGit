using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NewsSite.Models
{
    public class MyViewModel
    {

        public NyhederTable News { get; set; }

        public List<NyhederTable> AllNews { get; set; }

        public VidsteDuTable VidsteDu { get; set; }

        public List<VidsteDuTable> AllVidsteDu { get; set; }

        public Logins Logins { get; set; }

        public Kontaktoplysninger Kontaktoplysning { get; set; }

        public GaadeTabel Gaade { get; set; }

        public List<GaadeTabel> AllGaader { get; set; }

        //Oplist filer som uploades via Admin-index
        public List<FileInfo> MyFileInfo { get; set; }
        public string Msg { get; set; }

    }
}