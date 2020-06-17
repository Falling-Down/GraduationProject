﻿using System;
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
            int StuID = GCHomeBLL.AttendanReturnStuID(NumberOrName);
            ViewBag.AttendanList = GCHomeBLL.AttendanSelectNew(StuID);
            ViewBag.Name = NumberOrName;
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

        public ActionResult AddAttendace(Attendance ad,string StuNumber) {
            ad.StuID = GCHomeBLL.ReturnStuIDByStuNumber(StuNumber);
            if (GCHomeBLL.AddAttendace(ad))
            {
                return RedirectToAction("AttendanceFun","Student");
            }
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