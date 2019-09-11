using RetroCamMarlene.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Net.Mime;
using System.Net.Mail;

namespace RetroCamMarlene.Controllers
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
            if (ModelState.IsValid)
            {
                //Afsender mail
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;

                System.Net.NetworkCredential credential =
                    new System.Net.NetworkCredential("nutesterjeg@gmail.com", "websertMester1");
                client.UseDefaultCredentials = false;
                client.Credentials = credential;

                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("nutesterjeg@gmail.com", "Umbraco Demositets kontakttformular");
                msg.To.Add(new MailAddress("nutesterjeg@gmail.com"));
                msg.ReplyToList.Add(model.Email);
                msg.Subject = "Besked fra din kontaktformular";
                msg.IsBodyHtml = true;
                msg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                msg.Body = "Besked: " + model.Besked + ", Tlf: " + model.Tlf;

                string BodyHtmlFormat = string.Format("<!DOCTYPE html><html><head></head>" +
                                                        "<body style='background-color:#ddd;'>") + model.Besked + "<br/>" +
                                                        "Tlf: " + model.Tlf + "</body></html>";

                ContentType mimeType = new ContentType("text/html");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(BodyHtmlFormat, mimeType);
                msg.AlternateViews.Add(htmlView);

                try
                {
                    client.Send(msg);

                    var CurrentNode = Umbraco.TypedContent(UmbracoContext.PageId);
                    var ContentService = Services.ContentService;
                    var NyNode = ContentService.CreateContent(model.Navn, CurrentNode.Id, "besked", 0);
                    NyNode.SetValue("msgEmail", model.Email);
                    NyNode.SetValue("msgTlf", model.Tlf);
                    NyNode.SetValue("msgBesked", model.Besked);
                    ContentService.SaveAndPublishWithStatus(NyNode);

                    TempData.Add("Success", true);
                    return RedirectToCurrentUmbracoPage();
                }
                catch (Exception e)
                {
                    TempData.Add("Error", e.Message);
                    return CurrentUmbracoPage();
                }

            }

            else
            {
                TempData.Add("Error", "Fejl! Felterne er ikke udfyldt korrekt!");
                return CurrentUmbracoPage();
            }

        }
    }
}