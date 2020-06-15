using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace GC家园.Controllers
{
    public class AdminController : Controller
    {
        GCHomeBLL GCHomeBLL = new GCHomeBLL();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.AdminList = GCHomeBLL.SelectAdmin();
            return View();
        }

        public ActionResult FloorFun() {
            return View();
        }

        public ActionResult AddFloor(Floor fr) {
            fr.IsDelete = 0;
            if (GCHomeBLL.AddFloor(fr))
            {
                //return RedirectToAction("FloorFun","Admin");
                return Content("<script>alert('添加成功');history.back()</script>");
            }
            return View();
        }

        public ActionResult DormFun()
        {
            ViewBag.FloorList = GCHomeBLL.FloorSelect();
            return View();
        }

        public int AjaxFloorName(string FloorName) {
            if (GCHomeBLL.AjaxFloorName(FloorName))
            {
                return 1;
            }
            return 0;
        }

        [HttpPost]
        public int AjaxDorm(int FloorID, string DormName) {
            if (GCHomeBLL.AjaxDorm(FloorID,DormName))
            {
                return 1;
            }
            return 0;
        }

        public ActionResult AddDorm(Dorm dm) {
            dm.IsDelete = 0;
            if (GCHomeBLL.AddDorm(dm))
            {
                return Content("<script>alert('添加成功');history.back()</script>");
            }
            return View();
        }
    }
}