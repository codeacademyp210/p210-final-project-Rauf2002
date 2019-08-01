using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Hospital.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login

        private readonly HospitalEntities db;

        public LoginController()
        {
            db = new HospitalEntities();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Signin([Bind(Include ="Username,Password")] Models.Admin admin)
        {
            Models.Admin foundAdmin = db.Admin.FirstOrDefault(a => a.Username == a.Username);

            if (string.IsNullOrEmpty(admin.Username) || string.IsNullOrEmpty(admin.Password))
            {
                Session["loginError"] = "Neither email nor password can be left empty";
                return RedirectToAction("Index");
            }

            if (foundAdmin != null)
            {
                if (foundAdmin.Password == Crypto.Hash(admin.Password,"md5"))
                {
                    Session["isLogin"] = true;
                    return RedirectToAction("Index", "Allergy");
                }
            }

            Session["loginError"] = "Either email or password can not be found";

            return RedirectToAction("Index");
        }

        public ActionResult Signout()
        {
            Session["isLogin"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}