using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Controllers
{
    public class DoctorController : BaseController
    {
        // GET: Doctor
        public ActionResult Index()
        {
            ViewBag.cult = Thread.CurrentThread.CurrentUICulture.Name;

            return View(model);
        }

        public ActionResult One(int Id)
        {
            Doctors foundDoctor = db.Doctors.FirstOrDefault(d => d.Id == Id);
            ViewBag.cult = Thread.CurrentThread.CurrentUICulture.Name;

            return View(foundDoctor);
        }

        public ActionResult Schedule(int Id)
        {
            ViewBag.cult = Thread.CurrentThread.CurrentUICulture.Name;
            List<Models.Appointments> newAppList = db.Appointments.Where(a => a.DoctorId == Id).ToList();
            if (newAppList.FirstOrDefault() != null)
            {
                return View(newAppList);

            }
            else
            {
                return View("NoAppointment");
            }
        }

        public ActionResult Speciality(int Id)
        {
            Models.ViewModelHome newModel = new Models.ViewModelHome();
            List<Models.Doctors> doctorList = db.Doctors.Where(d => d.DepartmentId == Id).ToList();
            newModel.Doctors = doctorList;
            newModel.Departments = db.Departments.ToList();
            return View(newModel);
        }
    }
}