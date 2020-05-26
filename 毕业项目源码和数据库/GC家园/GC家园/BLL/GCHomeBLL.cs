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
    }
}
