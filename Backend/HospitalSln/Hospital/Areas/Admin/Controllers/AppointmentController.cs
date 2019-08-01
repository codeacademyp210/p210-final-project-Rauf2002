using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Areas.Admin.Controllers
{
    public class AppointmentController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Admin/Appointment
        public ActionResult Index()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                var appointments = db.Appointments.Include(a => a.Departments).Include(a => a.Doctors);
                return View(appointments.ToList());
            }
            return RedirectToAction("Index", "Login");
        }

       

        // POST: Admin/Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Time,Phone,DoctorId,DepartmentId")] Appointments appointments)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointments);
                db.SaveChanges();
                return RedirectToAction("Index","Home",new { Area = ""});
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameAz", appointments.DepartmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", appointments.DoctorId);
            return View(appointments);
        }

       



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
