using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class DepartmentController : BaseController
    {
        // GET: Department
        public ActionResult Index()
        {
            return View(model);
        }

        public ActionResult One(int Id)
        {
            Departments foundDepartment = model.Departments.FirstOrDefault(d => d.Id == Id);

            return View(foundDepartment);
        }
    }
}