using System;
using NewsSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace NewsSite.Controllers
{
    public class HomeController : Controller
    {

      
        NewsSiteEntities db = new NewsSiteEntities();

        //Vis forside
        public ActionResult Index()
        {
            ViewBag.Title = "Forsiden";


            MyViewModel myViewModel = new MyViewModel();
            myViewModel.AllNews = db.NyhederTable.ToList();
            myViewModel.AllVidsteDu = db.VidsteDuTable.ToList();
            myViewModel.AllGaader = db.GaadeTabel.ToList();



            return View(myViewModel);
        }

        //Søgeformular på Layout SoegNyhed 
        public ActionResult SoegNyhed()
        {
            return View();
        }
        [HttpPost] //der er posted noget fra søge-formular
        public ActionResult SoegNyhed(string Soegetxt)
        {
            //søg efter søgeord i modellen/tabellen joke
            List<NyhederTable> NyhedsResultat = new List<NyhederTable>();
            NyhedsResultat = db.NyhederTable.Where(j => j.NyhederOverskrift.Contains(Soegetxt) || j.NyhederTekst.Contains(Soegetxt)).ToList();

            return View(NyhedsResultat);
        }

        //Visning af den enkelte nyhed baseret på id fra tabellen
        public ActionResult EnkeltNyhed(int? Id)
        {
            //hvis der ikke et id med.
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            NyhederTable Nyhed = db.NyhederTable.Where(c => c.id == Id).FirstOrDefault();

            if (Nyhed == null)
            {
                return RedirectToAction("Index");
            }

            return View(Nyhed);


        }

        //Kontaktoplysninger i Footer (PARTIAL)
        [ChildActionOnly]
        public ActionResult Footer()
        {

            MyViewModel ModeltilKontaktoplysninger = new MyViewModel();
            ModeltilKontaktoplysninger.Kontaktoplysning = db.Kontaktoplysninger.FirstOrDefault();

                  
            return PartialView("Footer", ModeltilKontaktoplysninger);

        }

        //Overskrifter på liste (PARTIAL)

        [ChildActionOnly]
        public ActionResult Overskrifter()
        {
            MyViewModel myOverskrifter = new MyViewModel();
            myOverskrifter.AllNews = db.NyhederTable.ToList();


            return PartialView("Overskrifter", myOverskrifter);

        }

        //Dagens Joke på liste (PARTIAL)

        [ChildActionOnly]
        public ActionResult DagensGaade()
        {
            MyViewModel NyeGaader = new MyViewModel();
            NyeGaader.AllGaader = db.GaadeTabel.OrderBy(i => i.GaadeID).Take(5).ToList();
            
            return PartialView("DagensGaade", NyeGaader);

        }


        //NYHEDSBREV OG EMAIL-FORMULAR
        [ChildActionOnly]
        public ActionResult Newsletter()
        {
            NewsSite.Models.MailViewModel vm = new Models.MailViewModel();

            return PartialView("Newsletter", vm);
        }
        [HttpPost]
        public ActionResult Newsletter(NewsSite.Models.MailViewModel NewsletterSend)
        {
            if (ModelState.IsValid)
            {
                if (NewsletterSend.CheckNewsLetter)
                {
                    NewsletterSend.Message = "du er nu tilmeld - din gamle idiot";

                    NewsletterSend.MailAdressFrom = NewsletterSend.MailAdressFrom;

                    NewsletterSend.MailAdressTo = NewsletterSend.MailAdressFrom;

                    NewsletterSend.MailSubject = "Nyhedsbrev";

                    NewsletterSend.MailBody = "Du er nu tilmeldt nyhedsmailen";

                    SendMail(NewsletterSend);
                }
                else
                {
                    NewsletterSend.Message = "du har IKKE checket ja til nyhedsbrev";
                }

            }
            else
            {

            }
            return View(NewsletterSend);
        }




        //contact form
        [ChildActionOnly]
        public ActionResult ContactForm()
        {
            NewsSite.Models.MailViewModel vm = new Models.MailViewModel();
            return PartialView("ContactForm", vm);
        }
        [HttpPost]
        public ActionResult ContactForm(NewsSite.Models.MailViewModel ContactSend)
        {
            if (ModelState.IsValid)
            {
                ContactSend.MailAdressFrom = ContactSend.MailAdressFrom;
                ContactSend.MailAdressTo = "ditmitkundeservice@gmail.com";
                ContactSend.MailSubject = "fra mailform " + ContactSend.MailSubject;
                ContactSend.MailBody = @"Hej<b>" + ContactSend.MailName + "<br/> "
                    + "</b>" +
                    "emne <b>" + ContactSend.MailSubject + "</b> <br/>" +
                    "Besked" + ContactSend.MailBody.Replace(Environment.NewLine, "<br/>");

                SendMail(ContactSend);

                return View(ContactSend);
            }
            else
            {
                ContactSend.Message = "Du har ikke udfyldt Kontakt formularen korrekt, prøv igen.";
            }



            return View(ContactSend);
        }
        public void SendMail(NewsSite.Models.MailViewModel myMail)
        {
            //En instans af .net classen mailmessage (kend den på den grønne farve (kaldes et andet sted fra))
            // starter på en ny mail (laver en ny instans af mailmessage).. 
            //HUSK using (NameSpace eller bibliotek) Husk System.net.mail i toppen af controleren (intelesans tjek)
            MailMessage mail = new MailMessage();

            // From er = mailens afsender. Det er ikke mailens afsender når vi bruger gmails smtp, CHeck ved webhost.
            mail.From = new MailAddress(myMail.MailAdressFrom);

            //Dette er den mail som man besvare til (tilbage til kd efter kontakt med hjemmeside staf(input MailFrom i Contact form))
            mail.ReplyToList.Add(myMail.MailAdressFrom);

            // Emnefelt på mail. VIGTIG, men ikke et ultimativt krav.
            mail.Subject = myMail.MailSubject;

            //Er den besked der skrives i textarea. (Da vi har valgt at sende HTML kan vi erstate NewLine(Enviroment) med <br/>)
            mail.Body = myMail.MailBody;
            mail.IsBodyHtml = true;

            //er den mail add der modtager mail fra form
            //statisk i Contact form, dynamisk i newlettter = mail from.
            mail.To.Add(myMail.MailAdressTo);

            //"smtp" er en instans af smtpClient,  Smtp = simpel mail transport protocol
            SmtpClient smtp = new SmtpClient();

            //host er udbyderen udgående mailserver.
            //her er der gmails smtp og port.
            //tjek med webhost........!
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            //ssl er nøjvendigt  for gmail - tjek med udbyder..
            smtp.EnableSsl = true;

            //Her slår vi standart longi-oplysningerne fra.. 
            smtp.UseDefaultCredentials = false;

            //her skriver vi vores login oplysninger.
            smtp.Credentials = new System.Net.NetworkCredential("ditmitkundeservice@gmail.com", "Ballevej27");

            //Her pakker vi hele instansen(alt data tastet ovenfor) "mail" ned som parameter til metoden send.
            smtp.Send(mail);

        }

    }
}


