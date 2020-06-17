using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public class GCHomeBLL
    {
        GCHomeDAL GCHomeDAL = new GCHomeDAL();
        public List<Student> select() {
            return GCHomeDAL.select();
        }

        public Admin SelectAdmin(string Count, string Password,int Role) {
            return GCHomeDAL.SelectAdmin(Count,Password,Role);
        }

        public Student SelectStudent(string Count, string Password)
        {
            return GCHomeDAL.SelectStudent(Count,Password);
        }

        public bool AddStudent(Student stu) {
            return GCHomeDAL.AddStudent(stu);
        }

        public int? ReturnPeople(int? FloorID, int? DormID) {
            return GCHomeDAL.ReturnPeople(FloorID,DormID);
        }

        public int? ReturnMoinPeople(int? FloorID, int? DormID) {
            return GCHomeDAL.ReturnMoinPeople(FloorID,DormID);
        }

        public int? UpdateMoinPeople(int? FloorID, int? DormID) {
            return GCHomeDAL.UpdateMoinPeople(FloorID,DormID);
        }

        public List<Student> LikeSelect(int? State, string StuName="") {
            return GCHomeDAL.LikeSelect(State,StuName);
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

        public Moveinto ajaxFloorAndDorm(string StuNumber) {
            return GCHomeDAL.ajaxFloorAndDorm(StuNumber);
        }

        public Attendance AttendanSelectNew(int? StuID) {
            return GCHomeDAL.AttendanSelectNew(StuID);
        }
        public bool AddAttendace(Attendance ad) {
            return GCHomeDAL.AddAttendace(ad);
        }

        public bool DelAttendance(int id) {
            return GCHomeDAL.DelAttendance(id);
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
     }
}
