using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.Helpers;
using Newtonsoft.Json;
using PagedList;
using System.Data;
using System.IO;

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

        public ActionResult ExcelToUpload()
        {  //用来存储excel表中读出来的数据
            DataTable excelTable = new DataTable();
            string msg = "";
            if (Request.Files.Count > 0) //request.files.count客户端传过来几个文件
            {
                try
                {
                    HttpPostedFileBase mypost = Request.Files[0];//取客户端传过来多个文件的第一个
                    string fileName = Path.GetFileName(mypost.FileName);//通过文件流取文件名称
                    string serverpath = Server.MapPath(
                    string.Format("~/{0}", "ExcelFiles")); //获取要存入的服务器上的地址
                    string path = Path.Combine(serverpath, fileName); //将定义的服务器路径和文件名结合，形成完整路径
                    mypost.SaveAs(path); //将文件保存
                    msg = "文件上传成功！";
                    excelTable = GCHomeBLL.GetExcelDataTable(path);//获得表数据
                    msg = GCHomeBLL.InsertDataToDB(excelTable);// 写入基础数据

                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            else
            {
                msg = "请选择文件";
            }
            return Json(msg);
        }

        // GET: Student
        public ActionResult Index(int? page,int? State, string StuName="")
        {
            List<Student> stulist = GCHomeBLL.LikeSelect(State, StuName);
            var pageSize = 5;
            var pageNumber = page ?? 1;
            IPagedList<Student> pagedList = stulist.ToPagedList(pageNumber, pageSize);
            ViewBag.State = State;
            ViewBag.StuName = StuName;
            return View(pagedList);
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
            int[] a = GCHomeBLL.AttendanReturnStuID(NumberOrName);
            if (a == null)
            {
                ViewBag.AttendanList = GCHomeBLL.AttendanSelect();
                ViewBag.Name = NumberOrName;
            }
            else
            {
                List<Attendance> alist = new List<Attendance>();
                foreach (var item in a)
                {
                    if (GCHomeBLL.AttendanSelectNew(item)!=null)
                    {
                        Attendance ad = GCHomeBLL.AttendanSelectNew(item);
                        alist.Add(ad);
                    }             
                }
                ViewBag.AttendanList = alist;
                ViewBag.Name = NumberOrName;
            }
            return View();
        }

        [HttpPost]
        public int ajaxOrnotStuNumber(string StuNumber) {
            if (GCHomeBLL.StuNumberNewOrnot1(StuNumber))
            {
                return 1;
            }
            return 0;
        }

        [HttpPost]
        public int ajaxOrnotStuNumberandMoin(string StuNumber) {
            if (GCHomeBLL.ajaxOrnotStuNumberandMoin(StuNumber)==0)
            {
                return 0;
            }
            else if (GCHomeBLL.ajaxOrnotStuNumberandMoin(StuNumber) == 1)
            {
                return 1;
            }
            return 2;
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
                    if (GCHomeBLL.ExchangeMoveinDormPeople(ex))
                    {
                        if (GCHomeBLL.ExchangePeople(ex))
                        {
                            return RedirectToAction("ExchangeFun", "Student");
                        }
                    }
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