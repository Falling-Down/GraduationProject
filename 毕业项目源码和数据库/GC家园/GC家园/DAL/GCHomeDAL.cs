using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class GCHomeDAL
    {
        GCHomeEntitiesDB db = new GCHomeEntitiesDB();
        public List<Student> select() {
           return db.Student.OrderBy(p=>p.StuNumber).ToList();
        }

        public Admin SelectAdmin(string Count, string Password,int Role) {
            return db.Admin.FirstOrDefault(p => p.AdminCount == Count && p.AdminPwd == Password && p.AdminKinds == Role);
        }

        public Student SelectStudent(string Count, string Password)
        {
            return db.Student.FirstOrDefault(p => p.StuCount == Count && p.StuPwd == Password);
        }

        public bool AddStudent(Student stu) {
            db.Student.Add(stu);
            int result = db.SaveChanges();
            if (result>0)
            {
                return true;
            }
            return false;
        }

        public List<Student> LikeSelect(int? State,string StuName="") {
            if (State==0)
            {
               return  db.Student.Where(p => p.OccupancyStatus == 0 && p.StuName.Contains(StuName) || p.StuName == "").ToList();
            }
            else if(State==1)
            {
                return db.Student.Where(p => p.OccupancyStatus == 1 && p.StuName.Contains(StuName) || p.StuName == "").ToList();
            }
            else
            {
               
                return db.Student.Where(p => p.StuName.Contains(StuName) || p.StuName == null || p.StuName=="").OrderBy(p=>p.StuNumber).ToList();
            }
        }

        public Student EditStu(int StuID) {
            return db.Student.Find(StuID);
        }

        public bool EditStudent(Student stu) {
            db.Entry(stu).State = System.Data.Entity.EntityState.Modified;
            int result = db.SaveChanges();
            if (result>0)
            {
                return true;
            }
            return false;
        }

        public int StuNumberOrNot(string StuNumber) {
            if (db.Student.FirstOrDefault(p => p.StuNumber == StuNumber)!=null)
            {
                List<Moveinto> list = db.Moveinto.Where(p => p.StuID == db.Student.FirstOrDefault(m => m.StuNumber == StuNumber).StuID).ToList();
                if (list.Count>0)
                {
                    return 2;
                }
                return 1;
            }
            return 0;
        }

        public List<Floor> FloorSelect() {
           return  db.Floor.ToList();
        }

        public List<Dorm> DormSelect(int FloorID) {
            return db.Dorm.Where(p=>p.FloorID==FloorID).ToList();
        }

        public int ReturnStuIDByStuNumber(string StuNumber) {
            return db.Student.FirstOrDefault(p => p.StuNumber == StuNumber).StuID;
        }

        public bool AddMoveinto(Moveinto moin) {
            db.Moveinto.Add(moin);
            int result = db.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateState(int? StuID) {
            Student stu= db.Student.Find(StuID);
            stu.OccupancyStatus = 1;
            db.Entry(stu).State = System.Data.Entity.EntityState.Modified;
            int result = db.SaveChanges();
            if (result > 0) {
                return true;
            }
            return false;
        }

        public List<Attendance> AttendanSelect() {
            return db.Attendance.OrderByDescending(p=>p.AttendanceID).ToList();
        }

        public bool StuNumberNewOrnot(string StuNumber) {
            if (db.Student.FirstOrDefault(p => p.StuNumber == StuNumber) != null) {
                return true;
            }
            return false;
        }

        public bool AddAttendace(Attendance ad) {
            db.Attendance.Add(ad);
            int result = db.SaveChanges();
            if (result>0)
            {
                return true;
            }
            return false;
        }

        public List<Admin> SelectAdmin() {
            return db.Admin.Where(p => p.AdminKinds == 0).OrderByDescending(m => m.AdminID).ToList();
        }

        public bool AddFloor(Floor fr) {
            db.Floor.Add(fr);
            int result = db.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool AjaxFloorName(string FloorName) {
            if (db.Floor.FirstOrDefault(p => p.FloorName == FloorName)==null)
            {
                return true;
            }
            return false;
        }

        public bool AjaxDorm(int FloorID, string DormName) {
            List<Dorm> Dlist = db.Dorm.Where(p => p.FloorID == FloorID).ToList();
            foreach (var item in Dlist as List<Dorm>)
            {
                if (item.DormName==DormName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddDorm(Dorm dm){
            db.Dorm.Add(dm);
            int result = db.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
