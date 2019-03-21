using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace formular.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(Session["startcss"] != null)
            {
                ViewBag.knap = "stop";
            }
            else
            {
                ViewBag.knap = "start";
            }
            return View();
        }
        //navne form
        public ActionResult YourName()
        {
            //hvorfor er den her oprettet som var? og ikke som alle de andre instanster.
            var ViewModel = new formular.Models.NameViewModel();

            ViewModel.Message = "skriv dit fornavn";
            //ViewModel.Message += "<h1>bla bla bla</h1>";
            ViewModel.Birth = DateTime.Now;
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult YourName(formular.Models.NameViewModel viewModel)
        {
            viewModel.Message = viewModel.FirstName + " godt klaret ";
            viewModel.Message += viewModel.Birth.ToLongDateString();

            return View(viewModel);
        }



        //nyhedsbrev

        public ActionResult Newsletter()
        {
            formular.Models.MailViewModel vm = new Models.MailViewModel();

            return View(vm);
        }
        [HttpPost]
        public ActionResult Newsletter(formular.Models.MailViewModel NewsletterSend)
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

        public ActionResult ContactForm()
        {
            formular.Models.MailViewModel vm = new Models.MailViewModel();
            return View(vm);
        }
        [HttpPost]
        public ActionResult ContactForm(formular.Models.MailViewModel ContactSend)
        {
            if (ModelState.IsValid)
            {
                ContactSend.MailAdressFrom = ContactSend.MailAdressFrom;
                ContactSend.MailAdressTo = "krarup90@gmail.com";
                ContactSend.MailSubject = "fra mailform " + ContactSend.MailSubject;
                ContactSend.MailBody = @"Hej<b>"+ContactSend.MailName + "<br/> "
                    + "</b>"+
                    "emne <b>" + ContactSend.MailSubject +"</b> <br/>"+
                    "Besked"+ ContactSend.MailBody.Replace(Environment.NewLine,"<br/>");

                SendMail(ContactSend);
            }
            else
            {
                ContactSend.Message = "Du har ikke udfyldt Kontakt formularen korrekt, prøv igen.";
            }
           
             

            return View(ContactSend);
        }




        public ActionResult LoginPage()
        {
            formular.Models.LoginViewModell vm = new Models.LoginViewModell();
          
            return View(vm);
        }

        [HttpPost]
        public ActionResult LoginPage(formular.Models.LoginViewModell login)
        {
         if(login.Username =="marlene" && login.Password =="1234" || login.Username == "Jens" && login.Password == "1234")
            {
                Session["Login"] = login.Username;
                Session["Password"] = login.Password;
                Session.Timeout = 1;

                return RedirectToAction("index", "Admin");
            }
            else
            {
                login.Message = "Dit brugernav og adgangskode matcher ikke";
            }
            return View(login);
        }


        [HttpPost]
        public ActionResult startCss()
        {
          
            if (Session["startCss"] != null)
            {
               
                Session.RemoveAll();
               
            }
            else
            {
              ;
                Session["startcss"] = "ja";
              
            }
            return RedirectToAction("index", "Home");
        }

        public void SendMail(formular.Models.MailViewModel myMail)
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
            smtp.Credentials = new System.Net.NetworkCredential("nutesterjeg@gmail.com", "*******");

            //Her pakker vi hele instansen(alt data tastet ovenfor) "mail" ned som parameter til metoden send.
            smtp.Send(mail);

        }


    }


}