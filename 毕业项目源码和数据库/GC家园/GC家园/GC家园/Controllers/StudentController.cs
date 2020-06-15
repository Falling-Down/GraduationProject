using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace GC家园.Controllers
{
    public class StudentController : Controller
    {
        GCHomeBLL GCHomeBLL = new GCHomeBLL();
        // GET: Student
        public ActionResult Index(int? State, string StuName="")
        {
            ViewBag.list = GCHomeBLL.LikeSelect(State, StuName);
            ViewBag.State = State;
            ViewBag.StuName = StuName;
            return View();
        }

        [HttpPost]
        public ActionResult Index(Student stu) {
            stu.OccupancyStatus = 0;
            stu.IsDelete = 0;
            if (GCHomeBLL.AddStudent(stu))
            {
                return RedirectToAction("Index");
            }
            return View() ;
        }

        [HttpPost]
        public JsonResult EditStu(int StuID)
        {
            Student stu = GCHomeBLL.EditStu(StuID);
            return Json(stu, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MoveintoFun() {
            ViewBag.FloorList = GCHomeBLL.FloorSelect();
            return View();
        }

        public int AjaxStuNumber(string StuNumber) {
            if (GCHomeBLL.StuNumberOrNot(StuNumber)==2) {
                return 2;
            }
            else if(GCHomeBLL.StuNumberOrNot(StuNumber) == 1)
            {
                return 1;
            }
            return 0;
        }

        [HttpPost]
        public JsonResult AjaxDorm(int FloorID) {
            var Dormlist = GCHomeBLL.DormSelect(FloorID);
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string ret = JsonConvert.SerializeObject(Dormlist, jsSettings);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMoveinto(Moveinto moin,string StuNumber) {
            moin.StuID = GCHomeBLL.ReturnStuIDByStuNumber(StuNumber);
            moin.IsDelete = 0;
            if (GCHomeBLL.AddMoveinto(moin))
            {
                if (GCHomeBLL.UpdateState(moin.StuID))
                {
                    return RedirectToAction("Index");
                }
            }
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