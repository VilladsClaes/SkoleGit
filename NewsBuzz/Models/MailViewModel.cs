using System;
using System.Collections.Generic;
using System.Linq;
//Vi skal koble dataannotations for at bruge "requiered"
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NewsBuzz.Models
{
    public class MailViewModel
    {
        //Her kommer de ting som er required
        //Henne i viewet skal vi beskrive hvor det skal spyttes ud
        [Required(ErrorMessage = "Du skal taste din rigtige emailadresse")]
        public string MailAdressFrom { get; set; }

        //Emailadress sætter krav til @ og .dk osv
        [EmailAddress(ErrorMessage = "ikke gyldig emailadresse. Husk et @")]
        public string MailAdressTo { get; set; }

        [Required(ErrorMessage = "Dit navn skal stå her")]
        public string MailSenderName { get; set; }

        [Required(ErrorMessage = "Du kan ikke sende en tom besked")]
        public string MailBody { get; set; }

        [Required(ErrorMessage = "Der skal stå noget i emnefeltet")]
        public string MailSubject { get; set; }

        public Boolean CheckNewsLetter { get; set; }

        public string Message { get; set; }

    }


}