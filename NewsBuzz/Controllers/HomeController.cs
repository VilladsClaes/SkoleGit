using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Dette namespace/model skal jeg selv skrive
using NewsBuzz.Models;
//Dette er til mails
using System.Net.Mail;

namespace NewsBuzz.Controllers
{

    //Denne klasse kommer fra Controller-klassen
    public class HomeController : Controller
    {











       //NYHEDER på FORSIDE
        public ActionResult Index()
        {
            //Her kommer mine nyheder. 
            //Instans af Nyhed i en Nyhedsliste
            List<Nyhed> Nyhedsliste = new List<Nyhed>()
            {


            new Nyhed()
            {

                BilledeTilNyhed = "nyhed1.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",      
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

             new Nyhed()
            {

                BilledeTilNyhed = "nyhed2.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

              new Nyhed()
            {

                BilledeTilNyhed = "nyhed3.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

               new Nyhed()
            {

                BilledeTilNyhed = "nyhed4.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

                new Nyhed()
            {

                BilledeTilNyhed = "nyhed5.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

                 new Nyhed()
            {

                BilledeTilNyhed = "nyhed6.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

            new Nyhed()
            {

                BilledeTilNyhed = "nyhed7.jpg",
                AlternativTekstTilBillede = "Billede af nyhed2",
                OverskriftTilNyhed = "Overskrift2",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("02-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            }
        };
            
            //Husk at returnere et view.
            //Medtag Nyhed-instansen til viewet
            return View(Nyhedsliste);
        }





















        //KONTAKTFORMULAR
        //Kontaktformular (næsten ligesom nyhedsbrevet)
        public ActionResult Contact()
        {
            //Vi laver en instans af mailviewmodellen. 
            NewsBuzz.Models.MailViewModel Beskedvisning = new Models.MailViewModel();

            //instansen returneres til viewet
            return View(Beskedvisning);

        }


        //Ovenstående Actionresult kopieres for at lave et httppost
        //Dette httppost aktiveres kun når der trykkes på knappen med action=post
        //Denne Actionresult skal bruge modellen "MailViewModel" og vi kalder den SendEnMAil
        [HttpPost]
        public ActionResult Contact(NewsBuzz.Models.MailViewModel SendEnMail)
        {



            //Det betinges at emailadressen skal være valid, før den kan afsendes.
            if (ModelState.IsValid)
            {
                SendEnMail.MailAdressFrom = SendEnMail.MailAdressFrom;
                SendEnMail.MailAdressTo = "vill0281@videndjurs.net";
                SendEnMail.MailSubject = "Du skrev vedrørende: " + SendEnMail.MailSubject;
                SendEnMail.MailBody = "Navn: <br/>" + SendEnMail.MailSenderName + "Emne: <br/>" + SendEnMail.MailSubject + "Besked:" + SendEnMail.MailBody.Replace(Environment.NewLine, "< br />");
                SendEnMail.Message = "Du har nu sendt en mail til mig";

                SendMail(SendEnMail);

            }
            else
            {
                SendEnMail.Message = "Øv bøv";
;            }
            


            //Ovenstående model sendes til viewet
            return View(SendEnMail);
        }



        //Vi laver en ny metode 
        public void SendMail(NewsBuzz.Models.MailViewModel myMail)
        {
            //Instans af mailmessage-modellen
            MailMessage mail = new MailMessage();

            //fra denne instans hentes egenskaberne fra modellen
            mail.From = new MailAddress(myMail.MailAdressFrom);

            mail.ReplyToList.Add(myMail.MailAdressTo);

            mail.Subject = myMail.MailSubject;

            mail.Body = myMail.MailBody;

            mail.To.Add(myMail.MailAdressFrom);

            mail.IsBodyHtml = true;

            // "smtp" er en intans af SmtpClient
            SmtpClient smtp = new SmtpClient();

            // Host er webhost udgående mailserver
            // i dette tilfælde bruger vi gmails-smtp
            // tjek med udbyder (eks. smtp.unoeuro.com)
            smtp.Host = "smtp.gmail.com";

            // Tjek med webhost hvilken port du må/skal bruge
            smtp.Port = 587;

            // Ssl er nødvendigt når vi bruger gmail
            // Tjek evt. med webhost
            smtp.EnableSsl = true;


            // Her slå vi standard login-oplsyningerne fra
            smtp.UseDefaultCredentials = false;

            // Her taster vi vores login-oplysninger til gmailen.
            smtp.Credentials = new System.Net.NetworkCredential("mgnitest007@gmail.com", "testlol007");

            // Her pakker vi hele intansen "mail" ned som parameter til metoden "Send"
            // Inkl. alle værdier og data vi har tastet ovenover.
            smtp.Send(mail);
        }

        //LOGIN
        public ActionResult Login()
        {
            NewsBuzz.Models.LoginViewModel vm = new Models.LoginViewModel();


            return View(vm);
        }
        [HttpPost]
        public ActionResult Login(NewsBuzz.Models.LoginViewModel loginsiden)
        {
            //Det kræves af loginsiden.Username at det skal opfylde nogle parametre, som i dette tilfælde er hardcodede, men som kunne komme fra en database
            if (loginsiden.Username == "Villads" && loginsiden.Password == "1234" || loginsiden.Username == "Claes" && loginsiden.Password == "password")
            {
                //Sessionen som hedder Login indeholder noget fra loginsiden-instansen (defineret i dette actionresult) og tager feltet Username fra modellen (som kommer fra viewet). 
                Session["Login"] = loginsiden.Username;
                //Sessionen som hedder password indeholder noget fra  loginformularen
                Session["Adgangskode"] = loginsiden.Password;

                //Timeout er pr default 10 minutter. Her er det 1 minut
                Session.Timeout = 1;

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                loginsiden.Message = "Brugernavn eller adgangskode er forkert";
            }
            return View(loginsiden);
        }
    }
}