using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSite.Models
{
    public class MailViewModel
    {
        [Required(ErrorMessage ="Du skal skrive din emailadresse")]
        [EmailAddress(ErrorMessage ="Det skal være en den mail - TORSK!")]
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