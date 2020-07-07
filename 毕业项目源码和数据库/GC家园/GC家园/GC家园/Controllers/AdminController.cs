using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using Newtonsoft.Json;
using q.Filter;

namespace GC家园.Controllers
{
    [Login]
    public class AdminController : Controller
    {
        GCHomeBLL GCHomeBLL = new GCHomeBLL();
        // GET: Admin

        public ActionResult icon() {
            return View();
        }

        public ActionResult Index(string AdminName="")
        {
            try
            {
                ViewBag.FloorList = GCHomeBLL.FloorSelect();
                ViewBag.AdminList = GCHomeBLL.LikeAdmin(AdminName);
                ViewBag.AdminName = AdminName;
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据！');history.go(-1);</script>");
            }
        }

        public ActionResult FloorFun() {
            return View();
        }

        public ActionResult AddFloor(Floor fr) {
            try
            {
                fr.IsDelete = 0;
                if (GCHomeBLL.AddFloor(fr))
                {
                    return RedirectToAction("icon", "Admin");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据！');history.go(-1);</script>");
            }
        }

        public ActionResult DelAdmin(int AdminID) {
            try
            {
                if (GCHomeBLL.DelAdmin(AdminID))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult DormFun()
        {
            try
            {
                ViewBag.FloorList = GCHomeBLL.FloorSelect();
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public int AjaxFloorName(string FloorName) {
            try
            {
                if (GCHomeBLL.AjaxFloorName(FloorName))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        [HttpPost]
        public int AjaxDorm(int FloorID, string DormName) {
            try
            {
                if (GCHomeBLL.AjaxDorm(FloorID, DormName))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public ActionResult AddDorm(Dorm dm) {
            try
            {
                dm.IsDelete = 0;
                dm.MoveinDormPeople = 0;
                if (GCHomeBLL.AddDorm(dm))
                {
                    return RedirectToAction("icon", "Admin");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult AddAdmin(Admin ad) {
            try
            {
                ad.IsDelete = 0;
                ad.AdminKinds = 0;
                if (GCHomeBLL.AddAdmin(ad))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        [HttpPost]
        public JsonResult EditAdm(int AdminID)
        {
            try
            {
                Admin adm = GCHomeBLL.EditAdm(AdminID);
                JsonSerializerSettings jsSettings = new JsonSerializerSettings();
                jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                string ret = JsonConvert.SerializeObject(adm, jsSettings);
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult EditAdmin(Admin adm) {
            try
            {
                if (GCHomeBLL.EditAdmin(adm))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult UpdatePwdAdmin() {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePwdAdmin(string OldPwd,string NewPwd) {
            try
            {
                int Role = int.Parse(Session["Role"].ToString());
                string AdminCount = Session["Count"].ToString();
                if (GCHomeBLL.UpdatePwdAdmin(OldPwd, NewPwd, Role, AdminCount))
                {
                    return RedirectToAction("Updatetishi");
                }
                return Content("<script>alert('您的输入有误');history.go(-1);</script>");
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult Updatetishi() {
            return View();
        }
    }
}