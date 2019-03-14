using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSite.Models
{
    public class MyViewModel
    {

        public NyhederTable News { get; set; }

        public List<NyhederTable> AllNews { get; set; }

    }
}