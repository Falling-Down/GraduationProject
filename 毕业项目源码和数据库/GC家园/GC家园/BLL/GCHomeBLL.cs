using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Data;

namespace BLL
{
    public class GCHomeBLL
    {
        GCHomeDAL GCHomeDAL = new GCHomeDAL();
        ImportExcel im = new ImportExcel();
        SaveExcelToDB sa = new SaveExcelToDB();
        public DataTable GetExcelDataTable(string filePath) {
            return im.GetExcelDataTable(filePath);
        }

        public string InsertDataToDB(DataTable dt) {
            return sa.InsertDataToDB(dt);
        }

        public List<Student> select() {
            return GCHomeDAL.select();
        }

        public Student select(int StuID) {
            return GCHomeDAL.select(StuID);
        }

        public List<Student> LikeSelect1(List<Student> stus, int? State, string StuName = "") {
            return GCHomeDAL.LikeSelect1(stus, State, StuName);
        }

        public Admin SelectAdmin(string Count, string Password,int Role) {
            return GCHomeDAL.SelectAdmin(Count,Password,Role);
        }

        public List<Moveinto> SelectStudentByFloorID(int? FloorID) {
            return GCHomeDAL.SelectStudentByFloorID(FloorID);
        }

        public Student SelectStudent(string Count, string Password)
        {
            return GCHomeDAL.SelectStudent(Count,Password);
        }

        public bool AddStudent(Student stu) {
            return GCHomeDAL.AddStudent(stu);
        }

        public List<Student> LikeStudent1(string StuNumber = "") {
            return GCHomeDAL.LikeStudent1(StuNumber);
        }

        public int? ReturnPeople(int? FloorID, int? DormID) {
            return GCHomeDAL.ReturnPeople(FloorID,DormID);
        }

        public int? ReturnMoinPeople(int? FloorID, int? DormID) {
            return GCHomeDAL.ReturnMoinPeople(FloorID,DormID);
        }

        public int? ReturnMoinPeople1(int? FloorID, int? DormID) {
            return GCHomeDAL.ReturnMoinPeople1(FloorID,DormID);
        }

        public int? UpdateMoinPeople(int? FloorID, int? DormID) {
            return GCHomeDAL.UpdateMoinPeople(FloorID,DormID);
        }

        public List<Student> StuSelect() {
            return GCHomeDAL.StuSelect();
        }

        public List<Student> selectStudent() {
            return GCHomeDAL.selectStudent();
        }

        public List<Student> LikeStudent(string StuNumber="") {
            return GCHomeDAL.LikeStudent(StuNumber);
        }

        public List<Student> LikeSelect(int? State, string StuName="") {
            return GCHomeDAL.LikeSelect(State,StuName);
        }

        public Student SelectStu(int StuID)
        {
            return GCHomeDAL.SelectStu(StuID);
        }

            public Student EditStu(int StuID)
        {
            return GCHomeDAL.EditStu(StuID);
        }

        public bool EditStudent(Student stu) {
            return GCHomeDAL.EditStudent(stu);
        }

        public int StuNumberOrNot(string StuNumber) {
            return GCHomeDAL.StuNumberOrNot(StuNumber);
        }

        public List<Floor> FloorSelect() {
            return GCHomeDAL.FloorSelect();
        }

        public List<Dorm> DormSelect(int FloorID) {
            return GCHomeDAL.DormSelect(FloorID);
        }

        public int ReturnStuIDByStuNumber(string StuNumber) {
            return GCHomeDAL.ReturnStuIDByStuNumber(StuNumber);
        }

        public List<Moveinto> SelectMoveinto() {
            return GCHomeDAL.SelectMoveinto();
        }

        public Moveinto SelectMoveinto(int StuID) {
            return GCHomeDAL.SelectMoveinto(StuID);
        }

        public bool AddMoveinto(Moveinto moin) {
            return GCHomeDAL.AddMoveinto(moin);
        }

        public bool UpdateState(int? StuID) {
            return GCHomeDAL.UpdateState(StuID);
        }

        public int[] AttendanReturnStuID(string NumberOrName) {
            return GCHomeDAL.AttendanReturnStuID(NumberOrName);
        }

        public List<Attendance> AttendanSelect() {
            return GCHomeDAL.AttendanSelect();
        }

        public bool StuNumberNewOrnot(string StuNumber) {
            return GCHomeDAL.StuNumberNewOrnot(StuNumber);
        }

        public bool StuNumberNewOrnot1(string StuNumber)
        {
            return GCHomeDAL.StuNumberNewOrnot1(StuNumber);
        }

        public bool ExchangeMoveinDormPeople(Exchange ex) {
            return GCHomeDAL.ExchangeMoveinDormPeople(ex);
        }

        public bool ExchangePeople(Exchange ex) {
            return GCHomeDAL.ExchangePeople(ex);
        }

        public int ajaxOrnotStuNumberandMoin(string StuNumber) {
            return GCHomeDAL.ajaxOrnotStuNumberandMoin(StuNumber);
        }

        public Moveinto ajaxFloorAndDorm(string StuNumber) {
            return GCHomeDAL.ajaxFloorAndDorm(StuNumber);
        }

        public Attendance AttendanSelectNew(int? StuID) {
            return GCHomeDAL.AttendanSelectNew(StuID);
        }

        public List<Attendance> AttendanSelectNew1(int? StuID) {
            return GCHomeDAL.AttendanSelectNew1(StuID);
        }

        public bool AddAttendace(Attendance ad) {
            return GCHomeDAL.AddAttendace(ad);
        }

        public bool UpdateMoinFloorAndDorm(Exchange ex) {
            return GCHomeDAL.UpdateMoinFloorAndDorm(ex);
        }

        public List<Exchange> ExchangeSelect() {
            return GCHomeDAL.ExchangeSelect();
        }

        public bool AddExchange(Exchange ex) {
            return GCHomeDAL.AddExchange(ex);
        }

        public bool DelAttendance(int id) {
            return GCHomeDAL.DelAttendance(id);
        }

        public List<Moveout> MoveoutSelect(string StuNumber) {
            return GCHomeDAL.MoveoutSelect(StuNumber);
        }

        public bool AddMoveout(Moveout moout){
            return GCHomeDAL.AddMoveout(moout);
        }

        public bool UpdateStuOccState(int? StuID){
            return GCHomeDAL.UpdateStuOccState(StuID);
        }

        public bool UpdateDormMoveinDormPeople(int? StuID) {
            return GCHomeDAL.UpdateDormMoveinDormPeople(StuID);
        }

        public List<Admin> SelectAdmin() {
            return GCHomeDAL.SelectAdmin();
        }

        public List<Admin> LikeAdmin(string AdminName = "") {
            return GCHomeDAL.LikeAdmin(AdminName);
        }

        public bool AddFloor(Floor fr) {
            return GCHomeDAL.AddFloor(fr);
        }

        public bool AjaxFloorName(string FloorName) {
            return GCHomeDAL.AjaxFloorName(FloorName);
        }

        public bool AjaxDorm(int FloorID, string DormName) {
            return GCHomeDAL.AjaxDorm(FloorID,DormName);
        }

        public bool AddDorm(Dorm dm) {
            return GCHomeDAL.AddDorm(dm);
        }

        public bool AddAdmin(Admin ad) {
            return GCHomeDAL.AddAdmin(ad);
        }

        public Admin EditAdm(int AdminID) {
            return GCHomeDAL.EditAdm(AdminID);
        }

        public bool EditAdmin(Admin adm) {
            return GCHomeDAL.EditAdmin(adm);
        }

        public List<Fix> SelectFix() {
            return GCHomeDAL.SelectFix();
        }

        public List<Fix> SelectFix(int StuID) {
            return GCHomeDAL.SelectFix(StuID);
        }
     }
}
