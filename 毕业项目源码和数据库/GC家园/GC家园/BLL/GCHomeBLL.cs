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

        public List<Student> LikeSelect(int? State, string StuName="") {
            return GCHomeDAL.LikeSelect(State,StuName);
        }

        public Student EditStu(int StuID)
        {
            return GCHomeDAL.EditStu(StuID);
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
     }
}
