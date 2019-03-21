using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    public class FilController : Controller
    {
        // Bruges sammen med IOTools og MyViewModel-Filelist
        public ActionResult Index()
        {
            Models.MyViewModel Files = new Models.MyViewModel();
            Files.MyFileInfo = IOTools.DirInfo("~/Uploads");
            return View(Files);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase MyFile)
        {

            Models.MyViewModel MyViewModel = new Models.MyViewModel();

            string[] Ex = { ".jpg", ".png", ".gif" };
            MyViewModel.Msg = IOTools.FileUplader("~/Uploads", MyFile, Ex);

            MyViewModel.MyFileInfo = IOTools.DirInfo("~/Uploads");

            return View(MyViewModel);
        }
    }
}