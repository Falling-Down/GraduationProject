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
            try
            {
                ViewBag.People = People;
                return View();
            }
            catch (Exception)
            {
                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult Failicon()
        {
            return View();
        }

        public ActionResult Failibeijing()
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
            try
            {
                List<Student> stulist = GCHomeBLL.LikeSelect(State, StuName);
                var pageSize = 5;
                var pageNumber = page ?? 1;
                IPagedList<Student> pagedList = stulist.ToPagedList(pageNumber, pageSize);
                ViewBag.State = State;
                ViewBag.StuName = StuName;
                return View(pagedList);
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        [HttpPost]
        public ActionResult Index(Student stu) {
            try
            {
                stu.OccupancyStatus = 0;
                stu.IsDelete = 0;
                if (GCHomeBLL.AddStudent(stu))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        [HttpPost]
        public JsonResult EditStu(int StuID)
        {
            try
            {
                Student stu = GCHomeBLL.EditStu(StuID);
                JsonSerializerSettings jsSettings = new JsonSerializerSettings();
                jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                string ret = JsonConvert.SerializeObject(stu, jsSettings);
                return Json(ret, JsonRequestBehavior.AllowGet);     
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult EditStudent(Student stu) {
            try
            {
                if (GCHomeBLL.EditStudent(stu))
                {
                    return RedirectToAction("Index", "Student");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult MoveintoFun1(int? page,string StuNumber="") {
            List<Student> stulist = GCHomeBLL.LikeStudent(StuNumber);
            var pageSize = 5;
            var pageNumber = page ?? 1;
            IPagedList<Student> pagedList = stulist.ToPagedList(pageNumber, pageSize);
            ViewBag.StuNumber = StuNumber;
            return View(pagedList);
        }

        public ActionResult MoveintoFun(string StuNumber) {
            try
            {
                ViewBag.StuNumber = StuNumber;
                ViewBag.FloorList = GCHomeBLL.FloorSelect();
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public int AjaxStuNumber(string StuNumber) {
            try
            {
                if (GCHomeBLL.StuNumberOrNot(StuNumber) == 2)
                {
                    return 2;
                }
                else if (GCHomeBLL.StuNumberOrNot(StuNumber) == 1)
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult AjaxDorm(int FloorID) {
            try
            {
                var Dormlist = GCHomeBLL.DormSelect(FloorID);
                JsonSerializerSettings jsSettings = new JsonSerializerSettings();
                jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                string ret = JsonConvert.SerializeObject(Dormlist, jsSettings);
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult AddMoveinto(Moveinto moin,string StuNumber) {
            try
            {
                moin.StuID = GCHomeBLL.ReturnStuIDByStuNumber(StuNumber);
                moin.IsDelete = 0;
                if (GCHomeBLL.ReturnPeople(moin.FloorID, moin.DormID) > GCHomeBLL.ReturnMoinPeople(moin.FloorID, moin.DormID))
                {
                    if (GCHomeBLL.AddMoveinto(moin))
                    {
                        if (GCHomeBLL.UpdateState(moin.StuID))
                        {
                            if (GCHomeBLL.UpdateMoinPeople(moin.FloorID, moin.DormID) != null)
                            {
                                return RedirectToAction("Peopleicon", "Student", new { People = (GCHomeBLL.ReturnPeople(moin.FloorID, moin.DormID) - (GCHomeBLL.ReturnMoinPeople(moin.FloorID, moin.DormID))) });
                            }
                            return RedirectToAction("Index");
                        }
                    }
                }
                return RedirectToAction("Failicon", "Student");
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult AttendanceFun() {
            try
            {
                ViewBag.AttendanList = GCHomeBLL.AttendanSelect();
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        [HttpPost]
        public ActionResult AttendanceFun(string NumberOrName)
        {
            try
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
                        if (GCHomeBLL.AttendanSelectNew(item) != null)
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
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        [HttpPost]
        public int ajaxOrnotStuNumber(string StuNumber) {
            try
            {
                if (GCHomeBLL.StuNumberNewOrnot1(StuNumber))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public int ajaxOrnotStuNumberandMoin(string StuNumber) {
            try
            {
                if (GCHomeBLL.ajaxOrnotStuNumberandMoin(StuNumber) == 0)
                {
                    return 0;
                }
                else if (GCHomeBLL.ajaxOrnotStuNumberandMoin(StuNumber) == 1)
                {
                    return 1;
                }
                return 2;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult ajaxFloorAndDorm(string StuNumber) {
            try
            {
                var moin = GCHomeBLL.ajaxFloorAndDorm(StuNumber);
                JsonSerializerSettings jsSettings = new JsonSerializerSettings();
                jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                string ret = JsonConvert.SerializeObject(moin, jsSettings);
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult AddAttendace(Attendance ad,string StuNumber) {
            try
            {
                ad.StuID = GCHomeBLL.ReturnStuIDByStuNumber(StuNumber);
                if (GCHomeBLL.AddAttendace(ad))
                {
                    return RedirectToAction("AttendanceFun", "Student");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult DelAttendance(int id) {
            try
            {
                if (GCHomeBLL.DelAttendance(id))
                {
                    return RedirectToAction("AttendanceFun", "Student");
                }
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult ExchangeFun() {
            try
            {
                ViewBag.ExchangeList = GCHomeBLL.ExchangeSelect();
                ViewBag.FloorList = GCHomeBLL.FloorSelect();
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult AddExchange(Exchange ex, string StuNumber) {
            try
            {
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
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult Moveout(string StuNumber) {
            try
            {
                ViewBag.StuList = GCHomeBLL.StuSelect();
                ViewBag.MoveoutList = GCHomeBLL.MoveoutSelect(StuNumber);
                ViewBag.StuNumber = StuNumber;
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult AddMoveout(int id) {
            try
            {
                ViewBag.ID = id;
                return View();
            }
            catch (Exception)
            {

                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        [HttpPost]
        public ActionResult AddMoveout(Moveout moout)
        {
            try
            {
                if (GCHomeBLL.AddMoveout(moout))
                {
                    if (GCHomeBLL.UpdateStuOccState(moout.StuID))
                    {
                        if (GCHomeBLL.UpdateDormMoveinDormPeople(moout.StuID))
                        {
                            return RedirectToAction("Moveout", "Student");
                        }
                    }
                }
                return View();
            }
            catch (Exception)
            {
                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }

        public ActionResult Important(string StuNumber) {
            try
            {
                ViewBag.tishi = StuNumber;
                if (StuNumber != null && StuNumber != "")
                {
                    int StuID = GCHomeBLL.ReturnStuIDByStuNumber(StuNumber);
                    if (StuID != 0)
                    {
                        Student stu = GCHomeBLL.SelectStu(StuID);
                        ViewBag.Sst = stu;
                        Moveinto into = GCHomeBLL.SelectMoveinto(stu.StuID);
                        if (into != null)
                        {
                            ViewBag.Mst = into;
                            ViewBag.People = GCHomeBLL.ReturnMoinPeople1(into.FloorID, into.DormID);
                            return View();
                        }
                        return View();
                    }
                    return RedirectToAction("Failibeijing", "Student");
                }
                return RedirectToAction("Failibeijing", "Student");
            }
            catch (Exception)
            {
                return Content("<script>alert('暂无数据');history.go(-1);</script>");
            }
        }
    }
}