using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ditmit.Models
{
    public class HelpViewModel
    {
        //Hjælpetypentabellen
        public HelpType Helptypeitem { get; set; }
        //Hjælpetabellen
        public Help Helpitem{ get; set; }
        //hjælpene som liste
        public List<Help> helpitems { get; set; }
        


    }
}

