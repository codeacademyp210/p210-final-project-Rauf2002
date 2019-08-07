using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class GalleryController : BaseController
    {
        // GET: Gallery
        public ActionResult Index()
        {
            ViewBag.cult = Thread.CurrentThread.CurrentUICulture.Name;

            return View(model);
        }
    }
}