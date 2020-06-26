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

		 public List<Student> select()
        {
            try
            {
                return db.Student.OrderBy(p => p.StuNumber).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   

        public Admin SelectAdmin(string Count, string Password,int Role) {
            try
            {
                return db.Admin.FirstOrDefault(p => p.AdminCount == Count && p.AdminPwd == Password && p.AdminKinds == Role);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Student SelectStudent(string Count, string Password)
        {
            try
            {
                return db.Student.FirstOrDefault(p => p.StuCount == Count && p.StuPwd == Password);
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public bool AddStudent(Student stu) {
            try
            {
                db.Student.Add(stu);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int? ReturnPeople(int? FloorID, int? DormID) {
            try
            {
                if (db.Dorm.FirstOrDefault(p => p.FloorID == FloorID && p.DormID == DormID) != null)
                {
                    return db.Dorm.FirstOrDefault(p => p.FloorID == FloorID && p.DormID == DormID).DormPeople;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int? ReturnMoinPeople(int? FloorID, int? DormID)
        {
            try
            {
                if (db.Dorm.FirstOrDefault(p => p.FloorID == FloorID && p.DormID == DormID) != null)
                {
                    return db.Dorm.FirstOrDefault(p => p.FloorID == FloorID && p.DormID == DormID).MoveinDormPeople;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int? UpdateMoinPeople(int? FloorID, int? DormID)
        {
            try
            {
                if (db.Dorm.FirstOrDefault(p => p.FloorID == FloorID && p.DormID == DormID) != null)
                {
                    Dorm dm = db.Dorm.FirstOrDefault(p => p.FloorID == FloorID && p.DormID == DormID);
                    dm.MoveinDormPeople += 1;
                    db.Entry(dm).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return db.Dorm.FirstOrDefault(p => p.FloorID == FloorID && p.DormID == DormID).MoveinDormPeople;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Student> StuSelect() {
            try
            {
                return db.Student.Where(p => p.OccupancyStatus == 1).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Student> LikeSelect(int? State,string StuName="") {
            try
            {
                List<Student> list = db.Student.Where(p => p.OccupancyStatus == 0 || p.OccupancyStatus == 1).ToList();
                if (State == 0)
                {
                    return list.Where(p => p.OccupancyStatus == 0 && p.StuName.Contains(StuName) || p.StuName == "").ToList();
                }
                else if (State == 1)
                {
                    return list.Where(p => p.OccupancyStatus == 1 && p.StuName.Contains(StuName) || p.StuName == "").ToList();
                }
                else
                {
                    return list.Where(p => p.StuName.Contains(StuName) || p.StuName == null || p.StuName == "").OrderBy(p => p.StuNumber).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Student EditStu(int StuID) {
            try
            {
                return db.Student.Find(StuID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EditStudent(Student stu) {
            try
            {
                db.Entry(stu).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int StuNumberOrNot(string StuNumber) {
            try
            {
                if (db.Student.FirstOrDefault(p => p.StuNumber == StuNumber) != null)
                {
                    List<Moveinto> list = db.Moveinto.Where(p => p.StuID == db.Student.FirstOrDefault(m => m.StuNumber == StuNumber).StuID).ToList();
                    if (list.Count > 0)
                    {
                        return 2;
                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Moveinto ajaxFloorAndDorm(string StuNumber) {
            try
            {
                Student stu = db.Student.FirstOrDefault(p => p.StuNumber == StuNumber);
                return db.Moveinto.FirstOrDefault(p => p.StuID == stu.StuID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Floor> FloorSelect() {
            try
            {
                return db.Floor.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Dorm> DormSelect(int FloorID) {
            try
            {
                return db.Dorm.Where(p => p.FloorID == FloorID).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ReturnStuIDByStuNumber(string StuNumber) {
            try
            {
                return db.Student.FirstOrDefault(p => p.StuNumber == StuNumber).StuID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddMoveinto(Moveinto moin) {
            try
            {
                db.Moveinto.Add(moin);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateState(int? StuID) {
            try
            {
                Student stu = db.Student.Find(StuID);
                stu.OccupancyStatus = 1;
                db.Entry(stu).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int[] AttendanReturnStuID(string NumberOrName) {
            try
            {
                if (NumberOrName == "" || db.Student.FirstOrDefault(p => p.StuNumber == NumberOrName || p.StuName.Contains(NumberOrName)) == null)
                {
                    return null;
                }
                List<Student> stu = db.Student.Where(p => p.StuNumber == NumberOrName || p.StuName.Contains(NumberOrName)).ToList();
                int[] a = new int[stu.Count()];
                int i = 0;
                foreach (var item in stu as List<Student>)
                {
                    a[i] = item.StuID;
                    i++;
                }
                return a;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Attendance> AttendanSelect() {
            try
            {
                return db.Attendance.OrderByDescending(p => p.AttendanceID).Where(p => p.IsDelete == 0).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Attendance AttendanSelectNew(int? StuID)
        {
            try
            {
                return db.Attendance.FirstOrDefault(p => p.StuID == StuID && p.IsDelete == 0);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool StuNumberNewOrnot(string StuNumber) {
            try
            {
                if (db.Student.FirstOrDefault(p => p.StuNumber == StuNumber) != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool StuNumberNewOrnot1(string StuNumber)
        {
            try
            {
                if (db.Student.FirstOrDefault(p => p.StuNumber == StuNumber && p.OccupancyStatus == 1) != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ajaxOrnotStuNumberandMoin(string StuNumber)
        {
            try
            {
                Student stu = db.Student.FirstOrDefault(p => p.StuNumber == StuNumber);
                if (stu != null)
                {
                    if (db.Moveinto.FirstOrDefault(p => p.StuID == stu.StuID) == null)
                    {
                        return 1;
                    }
                    return 2;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddAttendace(Attendance ad) {
            try
            {
                ad.IsDelete = 0;
                db.Attendance.Add(ad);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddExchange(Exchange ex)
        {
            try
            {
                ex.IsDelete = 0;
                db.Exchange.Add(ex);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Exchange> ExchangeSelect()
        {
            try
            {
                return db.Exchange.OrderByDescending(p => p.ExchangeID).Where(p => p.IsDelete == 0).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddMoveout(Moveout moout) {
            try
            {
                db.Moveout.Add(moout);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateStuOccState(int? StuID)
        {
            try
            {
                Student stu = db.Student.FirstOrDefault(p => p.StuID == StuID);
                stu.OccupancyStatus = 2;
                db.Entry(stu).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateDormMoveinDormPeople(int? StuID) {
            try
            {
                Moveinto moin = db.Moveinto.FirstOrDefault(p => p.StuID == StuID);
                Dorm dm = db.Dorm.FirstOrDefault(p => p.FloorID == moin.FloorID && p.DormID == moin.DormID);
                dm.MoveinDormPeople -= 1;
                db.Entry(dm).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ExchangeMoveinDormPeople(Exchange ex)
        {
            try
            {
                Dorm dm = db.Dorm.FirstOrDefault(p => p.FloorID == ex.OldFloorID && p.DormID == ex.OldDormID);
                dm.MoveinDormPeople -= 1;
                db.Entry(dm).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ExchangePeople(Exchange ex)
        {
            try
            {
                Dorm dm = db.Dorm.FirstOrDefault(p => p.FloorID == ex.NewFloorID && p.DormID == ex.NewDormID);
                dm.MoveinDormPeople += 1;
                db.Entry(dm).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateMoinFloorAndDorm(Exchange ex) {
            try
            {
                Moveinto moin = db.Moveinto.FirstOrDefault(p => p.StuID == ex.StuID);
                moin.FloorID = ex.NewFloorID;
                moin.DormID = ex.NewDormID;
                db.Entry(moin).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DelAttendance(int id)
        {
            try
            {
                Attendance ad = db.Attendance.Find(id);
                ad.IsDelete = 1;
                db.Entry(ad).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Moveout> MoveoutSelect(string StuNumber)
        {
            try
            {
                if (db.Student.FirstOrDefault(p => p.StuNumber == StuNumber) != null)
                {
                    int StuID = db.Student.FirstOrDefault(p => p.StuNumber == StuNumber).StuID;
                    return db.Moveout.Where(p => p.StuID == StuID).ToList();
                }
                return db.Moveout.OrderByDescending(p => p.MoveoutID).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Admin> SelectAdmin() {
            try
            {
                return db.Admin.Where(p => p.AdminKinds == 0).OrderByDescending(m => m.AdminID).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Admin> LikeAdmin(string AdminName = "")
        {
            try
            {
                return db.Admin.Where(p => p.AdminKinds == 0 && p.AdminName.Contains(AdminName) || p.AdminName == "").OrderByDescending(m => m.AdminID).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddFloor(Floor fr) {
            try
            {
                db.Floor.Add(fr);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AjaxFloorName(string FloorName) {
            try
            {
                if (db.Floor.FirstOrDefault(p => p.FloorName == FloorName) == null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AjaxDorm(int FloorID, string DormName) {
            try
            {
                List<Dorm> Dlist = db.Dorm.Where(p => p.FloorID == FloorID).ToList();
                foreach (var item in Dlist as List<Dorm>)
                {
                    if (item.DormName == DormName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddDorm(Dorm dm){
            try
            {
                db.Dorm.Add(dm);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddAdmin(Admin ad)
        {
            try
            {
                db.Admin.Add(ad);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Admin EditAdm(int AdminID)
        {
            try
            {
                return db.Admin.Find(AdminID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EditAdmin(Admin adm)
        {
            try
            {
                db.Entry(adm).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
