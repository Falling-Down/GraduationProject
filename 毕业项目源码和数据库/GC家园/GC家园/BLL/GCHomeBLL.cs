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
    }
}
