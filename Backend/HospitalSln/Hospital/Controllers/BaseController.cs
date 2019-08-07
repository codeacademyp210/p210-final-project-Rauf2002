using Hospital.Models;
using Hospital.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class BaseController : Controller
    {
        protected HospitalEntities db;
        protected ViewModelHome model;
        public BaseController()
        {
            db = new HospitalEntities();
            model = new ViewModelHome();

            model.Admin = db.Admin.FirstOrDefault();
            model.Allergies = db.Allergies.ToList();
            model.Appointments = db.Appointments.ToList();
            model.Departments = db.Departments.ToList();
            model.Doctors = db.Doctors.ToList();
            model.Galleries = db.Gallery.ToList();
            model.Messages = db.Messages.ToList();
            model.Nurses = db.Nurses.ToList();
            model.Patients = db.Patients.ToList();
            model.Setting = db.Settings.FirstOrDefault();
            model.Vaccines = db.Vaccines.ToList();
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = LanguageMang.GetDefaultLanguage();
                }
            }
            new LanguageMang().SetLanguage(lang);

            return base.BeginExecuteCore(callback, state);
        }

    }
}