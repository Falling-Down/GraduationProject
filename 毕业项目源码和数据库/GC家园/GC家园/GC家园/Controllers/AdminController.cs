using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using Newtonsoft.Json;

namespace GC家园.Controllers
{
    public class AdminController : Controller
    {
        GCHomeBLL GCHomeBLL = new GCHomeBLL();
        // GET: Admin

        public ActionResult icon() {
            return View();
        }

        public ActionResult Index(string AdminName="")
        {
            ViewBag.FloorList = GCHomeBLL.FloorSelect();
            ViewBag.AdminList = GCHomeBLL.LikeAdmin(AdminName);
            ViewBag.AdminName = AdminName;
            return View();
        }

        public ActionResult FloorFun() {
            return View();
        }

        public ActionResult AddFloor(Floor fr) {
            fr.IsDelete = 0;
            if (GCHomeBLL.AddFloor(fr))
            {
                return RedirectToAction("icon","Admin");
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
            dm.MoveinDormPeople = 0;
            if (GCHomeBLL.AddDorm(dm))
            {
                return RedirectToAction("icon", "Admin");
            }
            return View();
        }

        public ActionResult AddAdmin(Admin ad) {
            ad.IsDelete = 0;
            ad.AdminKinds = 0;
            if (GCHomeBLL.AddAdmin(ad))
            {
                return RedirectToAction("Index","Admin");
            }
            return View();
        }

        [HttpPost]
        public JsonResult EditAdm(int AdminID)
        {
            Admin adm = GCHomeBLL.EditAdm(AdminID);
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string ret = JsonConvert.SerializeObject(adm, jsSettings);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditAdmin(Admin adm) {
            if (GCHomeBLL.EditAdmin(adm))
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
    }
}