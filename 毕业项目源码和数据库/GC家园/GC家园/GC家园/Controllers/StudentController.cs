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

        public ActionResult MoveintoFun() {
            return View();
        }

        public ActionResult AttendanceFun() {
            return View();
        }

        public ActionResult ExchangeFun() {
            return View();
        }

        public ActionResult Moveout() {
            return View();
        }
    }
}