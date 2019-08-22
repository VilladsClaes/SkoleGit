using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

        //Visning af Kategorier af jokes
        public ActionResult VisGaaderUdFraKategori(int Id)
        {

            List<GaadeTabel> JokeListe = new List<GaadeTabel>();
            JokeListe = db.GaadeTabel.Where(j => j.FK_Kategori == Id).ToList();
            return View(JokeListe);

        }

        //Visning til API-test-siden
        public ActionResult APIsiden()
        {
            return View();
        }


        //Søgning i Vidstedu-tabel visning af Søgeside
        //Dette view er tomt fordi det styres af API-kald og ikke Controller-Model-kald
        public ActionResult _SoegVidsteDu()
        {
            return PartialView();
        }

      

        //Søgeformular på Layout SoegNyhed 
        //Behøves dette tomme view overhovedet at blive skrevet?
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

            //Hvis nyheden ikke findes, så spark tilbage til Index
            if (Nyhed == null)
            {
                return RedirectToAction("Index");
            }

            return View(Nyhed);


        }



        //Kontaktoplysninger i Footer (PARTIAL)
        [ChildActionOnly]
        public ActionResult _Footer()
        {

            MyViewModel ModeltilKontaktoplysninger = new MyViewModel();
            ModeltilKontaktoplysninger.Kontaktoplysning = db.Kontaktoplysninger.FirstOrDefault();


            return PartialView("_Footer", ModeltilKontaktoplysninger);

        }



        //Overskrifter på liste (PARTIAL)
        [ChildActionOnly]
        public ActionResult _Overskrifter()
        {


            MyViewModel myOverskrifter = new MyViewModel();
            //I Viewmodellen er NyhederTable lavet til List og gjort public
            // Sådan:  public List<NyhederTable> AllNews { get; set; }

            myOverskrifter.AllNews = db.NyhederTable.ToList();


            return PartialView("_Overskrifter", myOverskrifter);

        }



        //Dagens Joke på liste (PARTIAL)
        [ChildActionOnly]
        public ActionResult _DagensGaade()
        {
            MyViewModel NyeGaader = new MyViewModel();
            //Tager de sidste/nyeste baseret på ID
            //NyeGaader.AllGaader = db.GaadeTabel.OrderBy(i => i.GaadeID).Take(5).ToList();
            //Tager en tilfældig gåde fra tabellen
            NyeGaader.AllGaader = db.GaadeTabel.OrderBy(j => Guid.NewGuid()).Take(1).ToList();

            return PartialView("_DagensGaade", NyeGaader);

        }




        //NYHEDSBREV
        public ActionResult _Newsletter()
        {
            MyViewModel vm = new MyViewModel();

            return View("_Newsletter", vm);
        }

        [HttpPost]
        public ActionResult NewsletterTilmeldt(MyViewModel NewsletterSend)
        {
            if (ModelState.IsValid)
            {
                //Inde i Model skal der stå [required] på denne property
                if (NewsletterSend.CheckNewsLetter)
                {
                    NewsletterSend.Message = MailTool.SendMail(NewsletterSend.MailAdressFrom, NewsletterSend.MailAdressFrom, "Nyhedsbrev", "Du er nu tilmeldt nyhedsmailen", "smtp.gmail.com", 587, "ditmitkundeservice@gmail.com", "Ballevej27");
                }
                else
                {
                    NewsletterSend.Message = "du har IKKE krydset ja til nyhedsbrev";
                }

            }

            return RedirectToAction("Index", "Home");


        }




        [ChildActionOnly]
        //EMAIL-FORMULAR
        public ActionResult VisKontaktformular()
        {
            MyViewModel vm = new Models.MyViewModel();
            return PartialView("_ContactForm", vm);
        }
        [HttpPost]
        public ActionResult ContactFormSendt(MyViewModel ContactSend)
        {
            if (ModelState.IsValid)
            {
                string MailBody = 
                    @"Hej<b> " + ContactSend.MailName + "</b><br/>" +
                    "Emne:<b> " + ContactSend.MailSubject + "</b><br/>" +
                    "Besked: " + ContactSend.MailBody.Replace(Environment.NewLine, "<br/>");

                //Returnerer "sendt" eller "ikke sendt" da der i MailTool er try-catch
                ContactSend.Message = MailTool.SendMail(ContactSend.MailAdressFrom, "ditmitkundeservice@gmail.com", "Fra hjemmesiden:  " + ContactSend.MailSubject, MailBody, "smtp.gmail.com", 587, "ditmitkundeservice@gmail.com", "Ballevej27");

            }
            else
            {
                ContactSend.Message = "Du har ikke udfyldt Kontakt formularen korrekt, prøv igen.";
            }


            return View("_ContactForm", ContactSend);
        }





        //EKSEMPEL PÅ HARDCODEDE NYHEDER
        //NYHEDER på FORSIDE
        [ChildActionOnly]

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

                }

             
         
            };

            //Husk at returnere et view.
            //Medtag Nyhed-instansen til viewet
            return View(Nyhedsliste);
        }






        //API-øvelser
     

        public ActionResult Starwars()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Byvejr()
        {
            return View();
        }

        public ActionResult PostnummerOpslag()
        {
            return View();
        }

        public ActionResult FilmAPI()
        {
            return View();
        }

        public ActionResult Folketinget()
        {
            return View();
        }

    }
}


