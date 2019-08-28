using Energiforbrug_Umbraco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Energiforbrug_Umbraco.Controllers
{
    public class ForbrugSurfaceController : SurfaceController
    {
        // GET: ForbrugSurface
        public ActionResult Index()
        {
            return PartialView("Forbrug", new Forbrugsting());
        }

        [HttpPost]
        public ActionResult Forbrug(Forbrugsting model)
        {

            var NyNode = Services.ContentService.CreateContent(model.ForbrugskildeNavn, 1077, "forbrug", 0);
            NyNode.SetValue("ForbrugskildeNavn", model.ForbrugskildeNavn);
            NyNode.SetValue("AntalMinutterIBrug", model.AntalMinutterIBrug);
            NyNode.SetValue("ForbrugMaengde", model.ForbrugMaengde);
            NyNode.SetValue("Forbrugstype", model.Forbrugstype);
            Services.ContentService.SaveAndPublishWithStatus(NyNode);


            return CurrentUmbracoPage();
        }
    }
}