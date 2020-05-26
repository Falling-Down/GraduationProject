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
        GCHomeEntities db = new GCHomeEntities();
        public List<Student> select() {
           return db.Student.ToList();
        }
    }
}
