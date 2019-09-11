using RetroCam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace RetroCam.Controllers
{
    public class KontaktformularSurfaceController : SurfaceController
    {
        // GET: KontaktformularSurface
        public ActionResult Index()
        {
            return PartialView("Kontaktformular", new KontaktformularModel());
        }

        [HttpPost]
        public ActionResult KontaktformularAfsendelse(KontaktformularModel model)
        {
            // Afsender en e-mail

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("din@gmail.com", "adgangskode"); // Husk mail og adgangskode!
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(model.Email);
            msg.To.Add(new MailAddress("modtagers@email.dk"));  // Husk modtagers e-mail her!

            msg.Subject = "Der er mail fra din kontaktformular!";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body>" + model.Besked + "<br />Alder: " + model.Alder + "</body></html>");

            try
            {
                // Afsender mailen
                client.Send(msg);

                // Gemmer beskeden i Content-træet under kontakt-siden
                var contentService = Services.ContentService;
                var nyNode = contentService.CreateContent(model.Navn, CurrentPage.Id, "besked", 0);
                nyNode.SetValue("msgEmail", model.Email);
                nyNode.SetValue("msgAlder", model.Alder);
                nyNode.SetValue("msgBesked", model.Besked);
                contentService.SaveAndPublishWithStatus(nyNode);


                // TempData er en midlertidig pladsholder til f.eks. en tekset som kan udskrives efter afsendelse.
                // I dette eksempel bruges TempData til at indeholde en TRUE/FALSE værdi.
                TempData.Add("Success", true);

                // Genindlæser nuværende side - men UDEN udfyldte inputs
                return RedirectToCurrentUmbracoPage();
            }
            catch (Exception)
            {
                TempData.Add("Success", false);
                // Genindlæser nuværende side uden at tømme inputs.
                return CurrentUmbracoPage();
            }
        }
    }
}