using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Energiforbrug_Umbraco.Models
{
    public class Forbrugsting
    {
        public string ForbrugskildeNavn { get; set; }
        public int ForbrugMaengde { get; set; }

        public string Forbrugstype { get; set; }

        public string AntalMinutterIBrug  { get; set; }
    }
}

