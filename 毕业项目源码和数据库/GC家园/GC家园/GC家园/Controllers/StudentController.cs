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
        public ActionResult Peopleicon(int People) {
            ViewBag.People = People;
            return View();
        }

        public ActionResult Failicon()
        {
            return View();
        }

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
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string ret = JsonConvert.SerializeObject(stu, jsSettings);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditStudent(Student stu) {
            if (GCHomeBLL.EditStudent(stu))
            {
                return RedirectToAction("Index","Student");
            }
            return View();
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
            if (GCHomeBLL.ReturnPeople(moin.FloorID, moin.DormID)>GCHomeBLL.ReturnMoinPeople(moin.FloorID,moin.DormID))
            {
                if (GCHomeBLL.AddMoveinto(moin))
                {
                    if (GCHomeBLL.UpdateState(moin.StuID))
                    {
                        if (GCHomeBLL.UpdateMoinPeople(moin.FloorID, moin.DormID) != null)
                        {
                            return RedirectToAction("Peopleicon", "Student",new { People= (GCHomeBLL.ReturnPeople(moin.FloorID, moin.DormID) - (GCHomeBLL.ReturnMoinPeople(moin.FloorID, moin.DormID)))}) ;
                        }
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Failicon","Student");
        }

        public ActionResult AttendanceFun() {
            ViewBag.AttendanList = GCHomeBLL.AttendanSelect();
            return View();
        }

        [HttpPost]
        public ActionResult AttendanceFun(string NumberOrName)
        {
            if (GCHomeBLL.AttendanReturnStuID(NumberOrName)==null)
            {
                ViewBag.AttendanList = GCHomeBLL.AttendanSelect();
                ViewBag.Name = NumberOrName;
            }
            else
            {
                List<Attendance> alist = new List<Attendance>();
                foreach (var item in GCHomeBLL.AttendanReturnStuID(NumberOrName))
                {
                    Attendance ad = GCHomeBLL.AttendanSelectNew(item);
                    alist.Add(ad);
                }
                ViewBag.AttendanList = alist;
                ViewBag.Name = NumberOrName;
            }
            return View();
        }

        [HttpPost]
        public int ajaxOrnotStuNumber(string StuNumber) {
            if (GCHomeBLL.StuNumberNewOrnot(StuNumber))
            {
                return 1;
            }
            return 0;
        }

        [HttpPost]
        public JsonResult ajaxFloorAndDorm(string StuNumber) {
            var moin = GCHomeBLL.ajaxFloorAndDorm(StuNumber);
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string ret = JsonConvert.SerializeObject(moin, jsSettings);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddAttendace(Attendance ad,string StuNumber) {
            ad.StuID = GCHomeBLL.ReturnStuIDByStuNumber(StuNumber);
            if (GCHomeBLL.AddAttendace(ad))
            {
                return RedirectToAction("AttendanceFun","Student");
            }
            return View();
        }

        public ActionResult DelAttendance(int id) {
            if (GCHomeBLL.DelAttendance(id))
            {
                return RedirectToAction("AttendanceFun", "Student");
            }
            return View();
        }

        public ActionResult ExchangeFun() {
            ViewBag.ExchangeList = GCHomeBLL.ExchangeSelect();
            ViewBag.FloorList = GCHomeBLL.FloorSelect();
            return View();
        }

        public ActionResult AddExchange(Exchange ex, string StuNumber) {
            ex.StuID = GCHomeBLL.ReturnStuIDByStuNumber(StuNumber);
            if (GCHomeBLL.AddExchange(ex))
            {
                if (GCHomeBLL.UpdateMoinFloorAndDorm(ex))
                {
                    return RedirectToAction("ExchangeFun", "Student");
                }
            }
            return View();
        }

        public ActionResult Moveout(string StuNumber) {
            ViewBag.StuList = GCHomeBLL.StuSelect();
            ViewBag.MoveoutList = GCHomeBLL.MoveoutSelect(StuNumber);
            ViewBag.StuNumber = StuNumber;
            return View();
        }

        public ActionResult AddMoveout(int id) {
            ViewBag.ID = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddMoveout(Moveout moout)
        {
            if (GCHomeBLL.AddMoveout(moout))
            {
                if (GCHomeBLL.UpdateStuOccState(moout.StuID))
                {
                    if (GCHomeBLL.UpdateDormMoveinDormPeople(moout.StuID))
                    {
                        return RedirectToAction("Moveout","Student");
                    }
                }
            }
            return View();
        }
    }
}