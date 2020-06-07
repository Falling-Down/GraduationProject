using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace GC家园.Controllers
{
    public class StudentController : Controller
    {
        GCHomeEntitiesDB db = new GCHomeEntitiesDB();
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.list = db.Student.ToList();
            return View();
        }

       
    }
}