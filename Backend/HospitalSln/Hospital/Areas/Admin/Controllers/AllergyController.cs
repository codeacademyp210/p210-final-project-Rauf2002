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
    public class AllergyController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Admin/Allergy
        public ActionResult Index()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                return View(db.Allergies.ToList());
            }
            return RedirectToAction("Index", "Login");
        }



        // GET: Admin/Allergy/Create
        public ActionResult Create()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Allergy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameAz,NameEng")] Allergies allergies)
        {
            if (ModelState.IsValid)
            {
                db.Allergies.Add(allergies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allergies);
        }

        // GET: Admin/Allergy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Allergies allergies = db.Allergies.Find(id);
                if (allergies == null)
                {
                    return HttpNotFound();
                }
                return View(allergies);
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Allergy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameAz,NameEng")] Allergies allergies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allergies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allergies);
        }

        public ActionResult Delete(int Id)
        {
            Models.Allergies foundAllergy = db.Allergies.FirstOrDefault(a => a.Id == Id);

            if (foundAllergy != null)
            {
                db.Allergies.Remove(foundAllergy);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
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
