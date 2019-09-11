using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroCam.Models
{
    public class KontaktformularModel
    {
        [Required(ErrorMessage = "Indtast dit navn")]
        public string Navn { get; set; }

        [EmailAddress(ErrorMessage = "Ugyldig e-mail adresse!")]
        [Required(ErrorMessage = "Indtast din e-mail adresse")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Indtast din alder")]
        //[MaxLength(3, ErrorMessage = "Du kan da ikke være over 999 år gammel?!")]
        public string Alder { get; set; }

        [Required(ErrorMessage = "Indtast en besked")]
        public string Besked { get; set; }
    }
}