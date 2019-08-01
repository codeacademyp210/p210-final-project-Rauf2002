using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Controllers
{
    public class DoctorController :  BaseController
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View(model);
        }

        public ActionResult One(int Id)
        {
            Doctors foundDoctor = db.Doctors.FirstOrDefault(d => d.Id == Id);

            return View(foundDoctor);
        }

        public ActionResult Schedule()
        {
            return View();
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