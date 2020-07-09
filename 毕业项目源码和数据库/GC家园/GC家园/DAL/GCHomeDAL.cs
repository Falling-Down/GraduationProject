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

        public Student select(int StuID)
        {
            try
            {
                return db.Student.FirstOrDefault(p=>p.StuID==StuID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Student> selectStudent()
        {
            try
            {
                return db.Student.OrderBy(p => p.StuNumber).Where(p=>p.OccupancyStatus==0).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Student> LikeStudent(string StuNumber="") {
            return db.Student.Where(p => p.OccupancyStatus == 0 && (p.StuNumber.Contains(StuNumber) || p.StuNumber == "")).ToList();
        }

        public List<Student> LikeStudent1(string StuNumber = "")
        {
            return db.Student.Where(p => p.OccupancyStatus == 1 && (p.StuNumber.Contains(StuNumber) || p.StuNumber == "")).ToList();
        }

        public List<Student> LikeStudent2(string StuNumber = "")
        {
            return db.Student.Where(p => p.OccupancyStatus == 2 && (p.StuNumber.Contains(StuNumber) || p.StuNumber == "")).ToList();
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

        public List<Moveinto> SelectStudentByFloorID(int? FloorID) {
            return db.Moveinto.Where(p => p.FloorID == FloorID).ToList();
        }


        public Student SelectStudent(string Count, string Password)
        {
            try
            {
                return db.Student.FirstOrDefault(p => p.StuCount == Count && p.StuPwd == Password&&p.OccupancyStatus==1);
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

                return false;
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

                return null;
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

                return null;
            }
        }

        public int? ReturnMoinPeople1(int? FloorID, int? DormID)
        {
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

                return null;
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

                return null;
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
                    return list.Where(p => p.OccupancyStatus == 0 && (p.StuName.Contains(StuName) || p.StuName == "")).ToList();
                }
                else if (State == 1)
                {
                    return list.Where(p => p.OccupancyStatus == 1 && (p.StuName.Contains(StuName) || p.StuName == "")).ToList();
                }
                else
                {
                    return list.Where(p => p.StuName.Contains(StuName) || p.StuName == null || p.StuName == "").OrderBy(p => p.StuNumber).ToList();
                }
            }
            catch (Exception)
            {

                List<Student> stlist = new List<Student>() { };
                return stlist;
            }
        }

        public List<Student> LikeSelect1(List<Student> stus, int? State, string StuName = "") {
            try
            {
                if (State == 1)
                {
                    return stus.Where(p => p.OccupancyStatus == 1 && (p.StuName.Contains(StuName) || p.StuName == "")).ToList();
                }
                else if (State == 2)
                {
                    return stus.Where(p => p.OccupancyStatus == 2 && (p.StuName.Contains(StuName) || p.StuName == "")).ToList();
                }
                else
                {
                    return stus.Where(p => p.StuName.Contains(StuName) || p.StuName == null || p.StuName == "").OrderBy(p => p.StuNumber).ToList();
                }
            }
            catch (Exception)
            {

                List<Student> stlist = new List<Student>() { };
                return stlist;
            }
        }

        public Student SelectStu(int StuID) {
            return db.Student.FirstOrDefault(p => p.StuID == StuID&&(p.OccupancyStatus==0||p.OccupancyStatus==1));
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

                return false;
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

                return 0;
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
                Moveinto mo = new Moveinto() { };
                return mo;
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
                Student stu = db.Student.FirstOrDefault(p => p.StuNumber == StuNumber);
                if (stu!=null)
                {
                    return stu.StuID;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<Moveinto> SelectMoveinto()
        {
            try
            {
                return db.Moveinto.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Moveinto SelectMoveinto(int StuID)
        {
            try
            {
               return db.Moveinto.FirstOrDefault(p=>p.StuID==StuID);
            }
            catch (Exception)
            {
                Moveinto move = new Moveinto() { };
                return move;
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

                return false;
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

                return false;
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
                int[] a = new int[10] ;
                return a;
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
                Attendance ad = new Attendance() { };
                return ad;
            }
        }

        public List<Attendance> AttendanSelectNew1(int? StuID)
        {
            try
            {
                return db.Attendance.Where(p => p.StuID == StuID && p.IsDelete == 0).ToList();
            }
            catch (Exception)
            {
                List<Attendance> alist = new List<Attendance>() { };
                return alist;
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

                return false;
            }
        }

        public bool ajaxOrnotStuCount(string StuCount)
        {
            try
            {
                if (db.Student.FirstOrDefault(p => p.StuCount == StuCount) != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
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

                return false;
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

                return 0;
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

                return false;
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

                return false;
            }
        }

        public bool AddFix(Fix fx)
        {
            try
            {
                db.Fix.Add(fx);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
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
                List<Exchange> elist = new List<Exchange>() { };
                return elist;
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

                return false;
            }
        }

        public bool DelMoveout(int MoveoutID) {
            try
            {
                Moveout moveout = db.Moveout.Find(MoveoutID);
                db.Moveout.Remove(moveout);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
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

                return false;
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

                return false;
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

                return false;
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

                return false;
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

                return false;
            }
        }

        public bool DelAttendance(int id)
        {
            try
            {
                Attendance ad = db.Attendance.Find(id);
                db.Attendance.Remove(ad);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DelExchange(int ExchangeID) {
            try
            {
                Exchange ex = db.Exchange.Find(ExchangeID);
                db.Exchange.Remove(ex);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Moveout> MoveoutSelect(string StuNumber)
        {
            try
            {
                if (db.Student.FirstOrDefault(p => p.StuNumber==StuNumber) != null)
                {
                    int StuID = db.Student.FirstOrDefault(p => p.StuNumber==StuNumber).StuID;
                    foreach (var item in db.Moveout.ToList())
                    {
                        if (StuID==item.StuID)
                        {
                            return db.Moveout.Where(p => p.StuID == StuID).ToList();
                        }
                    }
                }
                return db.Moveout.OrderByDescending(p => p.MoveoutID).ToList();
            }
            catch (Exception)
            {
                List<Moveout> mlist = new List<Moveout>() { };
                return mlist;
            }
        }

        public List<Admin> SelectAdmin() {
            try
            {
                return db.Admin.Where(p => p.AdminKinds == 0).OrderByDescending(m => m.AdminID).ToList();
            }
            catch (Exception)
            {
                List<Admin> admin = new List<Admin>() { };
                return admin;
            }
        }

        public bool DelAdmin(int AdminID) {
            try
            {
                Admin ad = db.Admin.Find(AdminID);
                db.Admin.Remove(ad);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Admin> LikeAdmin(string AdminName = "")
        {
            try
            {
                return db.Admin.Where(p => p.AdminKinds == 0 && (p.AdminName.Contains(AdminName) || p.AdminName == "")).OrderByDescending(m => m.AdminID).ToList();
            }
            catch (Exception)
            {
                List<Admin> alist = new List<Admin>() { };
                return alist;
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

                return false;
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

                return false;
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

                return false;
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

                return false;
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

                return false;
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

                return false;
            }
        }

        public List<Fix> SelectFix(string Adress="") {
            return db.Fix.Where(p=>p.Adress.Contains(Adress)||p.Adress=="").ToList();
        }

        public List<Fix> SelectFix(int StuID)
        {
            return db.Fix.Where(p=>p.StuID==StuID).ToList();
        }

        public bool DelFix(int FixID) {
            try
            {
                Fix fx = db.Fix.Find(FixID);
                db.Fix.Remove(fx);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateFixState(int FixID) {
            try
            {
                Fix fx = db.Fix.Find(FixID);
                fx.FixState = 0;
                db.Entry(fx).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateFixState1(int FixID)
        {
            try
            {
                Fix fx = db.Fix.Find(FixID);
                fx.FixState = 2;
                db.Entry(fx).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateFixState2(int FixID)
        {
            try
            {
                Fix fx = db.Fix.Find(FixID);
                fx.FixState = 3;
                db.Entry(fx).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateFixXsReason(string XsReason, int FixID)
        {
            try
            {
                Fix fx = db.Fix.Find(FixID);
                fx.XsReason = XsReason;
                fx.XsDate = DateTime.Now;
                db.Entry(fx).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdatePwdStudent(string OldPwd, string NewPwd,int StuID,string StuCount) {
            try
            {
                if (db.Student.FirstOrDefault(p => p.StuID == StuID && p.StuCount == StuCount && p.StuPwd == OldPwd) != null)
                {
                    Student stu = db.Student.Find(StuID);
                    stu.StuPwd = NewPwd;
                    db.Entry(stu).State = System.Data.Entity.EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdatePwdAdmin(string OldPwd, string NewPwd, int Role, string AdminCount)
        {
            try
            {
                Admin ad = db.Admin.FirstOrDefault(p => p.AdminKinds == Role && p.AdminCount == AdminCount && p.AdminPwd == OldPwd);
                Admin ad1 = db.Admin.Find(ad.AdminID);
                if (ad1 != null)
                {
                    ad1.AdminPwd = NewPwd;
                    db.Entry(ad1).State = System.Data.Entity.EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
