using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsBuzz.Models
{
    //Her er modellen for Nyhed
    public class Nyhed
    {
        //Modellen indeholder egenskaber/properties som kan fyldes med indhold i Controlleren inden afsendelse til Viewet
        public string BilledeTilNyhed { get; set; }
        public string AlternativTekstTilBillede { get; set; }
        public string OverskriftTilNyhed { get; set; }
        public DateTime UdgivelsesdatoTilNyhed { get; set; }
        public string IndholdTilNyhed { get; set; }
        
       
    }
}