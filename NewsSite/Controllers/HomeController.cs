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
            //Viser kun de første fem
            myViewModel.AllGaader = db.GaadeTabel.OrderBy(p => p.GaadeSpoergsmaal).Take(5).ToList();
            
     

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
                    NewsletterSend.Message = MailTool.SendMail(NewsletterSend.MailAdressFrom, NewsletterSend.MailAdressFrom, "Nyhedsbrev", "Du er nu tilmeldt nyhedsmailen", "smtp.gmail.com", 587, "ditmitkundeservice@gmail.com", "Ballevej27");
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
                string MailBody = @"Hej<b>" + ContactSend.MailName + "<br/> "
                    + "</b>" +
                    "emne <b>" + ContactSend.MailSubject + "</b> <br/>" +
                    "Besked" + ContactSend.MailBody.Replace(Environment.NewLine, "<br/>");

                //Returnerer "sendt" eller "ikke sendt" da der i MailTool er try-catch
               ContactSend.Message = MailTool.SendMail(ContactSend.MailAdressFrom, "ditmitkundeservice@gmail.com", "fra mailform " + ContactSend.MailSubject, MailBody, "smtp.gmail.com", 587, "ditmitkundeservice@gmail.com", "Ballevej27"  );

                return View(ContactSend);
            }
            else
            {
                ContactSend.Message = "Du har ikke udfyldt Kontakt formularen korrekt, prøv igen.";
            }



            return View(ContactSend);
        }





        //EKSEMPEL PÅ HARDCODEDE NYHEDER
        //NYHEDER på FORSIDE
        public ActionResult EksempelNyheder()
        {
            //Her kommer mine nyheder. 
            //Instans af Nyhed i en Nyhedsliste
            List<EksempelNyhed> Nyhedsliste = new List<EksempelNyhed>()
            {


            new EksempelNyhed()
            {

                BilledeTilNyhed = "nyhed1.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

             new EksempelNyhed()
            {

                BilledeTilNyhed = "nyhed2.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

              new EksempelNyhed()
            {

                BilledeTilNyhed = "nyhed3.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

               new EksempelNyhed()
            {

                BilledeTilNyhed = "nyhed4.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

                new EksempelNyhed()
            {

                BilledeTilNyhed = "nyhed5.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

                 new EksempelNyhed()
            {

                BilledeTilNyhed = "nyhed6.jpg",
                AlternativTekstTilBillede = "Billede af nyhed1",
                OverskriftTilNyhed = "Overskrift1",
                UdgivelsesdatoTilNyhed = Convert.ToDateTime("21-10-1986"),
                IndholdTilNyhed = "Nyhedsindholdet til denne nyhed"

            },

            new EksempelNyhed()
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



    }
}


