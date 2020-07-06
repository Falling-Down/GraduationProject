using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using PagedList;

namespace GC家园.Controllers
{
    public class QianduanController : Controller
    {
        GCHomeBLL GCHomeBLL = new GCHomeBLL();
        // GET: Qianduan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelfCenter() {
            int StuID = GCHomeBLL.ReturnStuIDByStuNumber(Session["StuNumber"].ToString());
            if (StuID!=0)
            {
                ViewBag.Sst = GCHomeBLL.select(StuID);
                ViewBag.Mst = GCHomeBLL.SelectMoveinto(StuID);
            }
            return View();
        }

        public ActionResult FixCenter() {
            int StuID = GCHomeBLL.ReturnStuIDByStuNumber(Session["StuNumber"].ToString());
            ViewBag.StuID = StuID;
            return View();
        }

        [HttpPost]
        public ActionResult FixCenter(Fix fx) {
            fx.IsDelete = 0;
            fx.FixDate = DateTime.Now;
            fx.FixState = 1;
            if (GCHomeBLL.AddFix(fx))
            {
                return RedirectToAction("FixCenterIndex","Qianduan");
            }
            return View();
        }

        public ActionResult FixCenterIndex(int? page)
        {
            int StuID = GCHomeBLL.ReturnStuIDByStuNumber(Session["StuNumber"].ToString());
            if (StuID!=0)
            {
                List<Fix> fx = GCHomeBLL.SelectFix(StuID);
                var pageSize = 5;
                var pageNumber = page ?? 1;
                IPagedList<Fix> pagedList = fx.ToPagedList(pageNumber, pageSize);
                return View(pagedList);
            }
            return View();
        }

        public ActionResult AttendanCenter()
        {
            return View();
        }

        public ActionResult AttendanCenterIndex(int? page)
        {
            int StuID = GCHomeBLL.ReturnStuIDByStuNumber(Session["StuNumber"].ToString());
            if (StuID != 0)
            {
                List<Attendance> atlist = GCHomeBLL.AttendanSelectNew1(StuID);
                var pageSize = 5;
                var pageNumber = page ?? 1;
                IPagedList<Attendance> pagedList = atlist.ToPagedList(pageNumber, pageSize);
                return View(pagedList);
            }
            return View();
        }

        public ActionResult DelFix(int FixID) {
            if (GCHomeBLL.DelFix(FixID))
            {
                return RedirectToAction("FixCenterIndex", "Qianduan");
            }
            return View();
        }

        public ActionResult AddSxReason(int FixID) {
            ViewBag.FixID = FixID;
            return View();
        }

        public ActionResult Sxtishi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSxReason(string XsReason,int FixID) {
            if (GCHomeBLL.UpdateFixXsReason(XsReason,FixID))
            {
                return RedirectToAction("Sxtishi");
            }
            return View();
        }
    }
}