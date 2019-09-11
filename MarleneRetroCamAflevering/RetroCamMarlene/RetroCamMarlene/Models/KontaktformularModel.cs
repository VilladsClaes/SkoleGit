using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroCamMarlene.Models
{
    public class KontaktformularModel
    {
        [Required(ErrorMessage = "Indtast dit navn")]
        public string Navn { get; set; }

        [Required(ErrorMessage = "Indtast din mail")]
        [EmailAddress(ErrorMessage = "Ugyldig e-mail")]
        public string Email { get; set; }
        public string Tlf { get; set; }

       [Required(ErrorMessage = "Indtast din besked")]
        public string Besked { get; set; }
    }
}