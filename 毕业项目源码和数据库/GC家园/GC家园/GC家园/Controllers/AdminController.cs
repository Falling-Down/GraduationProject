using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GC家园.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FloorFun() {
            return View();
        }

        public ActionResult DormFun()
        {
            return View();
        }
    }
}