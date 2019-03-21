using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    public class FilController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Models.ViewModelFile Files = new Models.ViewModelFile();
            Files.MyFileInfo = IOTools.DirInfo("~/Uploads");
            return View(Files);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase MyFile)
        {

            Models.ViewModelFile MyViewModel = new Models.ViewModelFile();

            string[] Ex = { ".jpg", ".png", ".gif" };
            MyViewModel.Msg = IOTools.FileUplader("~/Uploads", MyFile, Ex);

            MyViewModel.MyFileInfo = IOTools.DirInfo("~/Uploads");

            return View(MyViewModel);
        }
    }
}