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
           return db.Student.ToList();
        }

        public Admin SelectAdmin(string Count, string Password,int Role) {
            return db.Admin.FirstOrDefault(p => p.AdminCount == Count && p.AdminPwd == Password && p.AdminKinds == Role);
        }

        public Student SelectStudent(string Count, string Password)
        {
            return db.Student.FirstOrDefault(p => p.StuCount == Count && p.StuPwd == Password);
        }
    }
}
