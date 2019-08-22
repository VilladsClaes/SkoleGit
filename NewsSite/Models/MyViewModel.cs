using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace NewsSite.Models
{
    public class MyViewModel
    {
        //Popup-model
        public tblPopup Popup { get; set; }

        public List<tblPopup> AllPopups { get; set; }

        //Note-model
        public NoteTabel Note { get; set; }

        public List<NoteTabel> Noter { get; set; }


        //Nyheder-model
        public NyhederTable News { get; set; }

        public List<NyhederTable> AllNews { get; set; }


        //VidsteDu-model

        public VidsteDuTable VidsteDu { get; set; }

        public List<VidsteDuTable> AllVidsteDu { get; set; }

        //Login-model
        public Logins Logins { get; set; }


        //Kontaktinfo-model
        public Kontaktoplysninger Kontaktoplysning { get; set; }   

        //Kategori-model
        public KategoriTabel Kategori { get; set; }
        public List<KategoriTabel> AllKategori { get; set; }


        //Gåder-Tabel
        public GaadeTabel Gaade { get; set; }

        public List<GaadeTabel> AllGaader { get; set; }

        //Oplist filer som uploades via Admin-index
        public List<FileInfo> MyFileInfo { get; set; }
        public FileInfo Filen { get; set; }
        public string Msg { get; set; }

   
        //Mail-model
        [Required(ErrorMessage = "Du skal skrive din emailadresse")]
        [EmailAddress(ErrorMessage = "Det skal være en den mail - TORSK!")]
        public string MailAdressFrom { get; set; }
        public string MailAdressTo { get; set; }
        [Required(ErrorMessage = "Du skal skrive dit navn")]
        public string MailName { get; set; }
        [Required(ErrorMessage = "Du skal skrive noget i emnefeltet")]

        public string MailSubject { get; set; }
        [Required(ErrorMessage = "Du skal skrive en besked til os!")]

        public string MailBody { get; set; }
        [Required]
        public Boolean CheckNewsLetter { get; set; }
        public string Message { get; set; }

    }
}