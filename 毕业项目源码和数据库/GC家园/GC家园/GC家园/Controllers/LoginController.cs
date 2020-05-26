using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Models;

namespace GC家园.Controllers
{
    public class LoginController : Controller
    {
        GCHomeBLL GCHomeBLL = new GCHomeBLL();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home() {
            return View();
        }
    }
}