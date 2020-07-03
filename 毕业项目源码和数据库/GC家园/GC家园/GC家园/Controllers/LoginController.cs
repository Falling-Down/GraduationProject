using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Models;

namespace GC家园.Controllers
{
    public class LoginController : Controller
    {
        GCHomeBLL GCHomeBLL = new GCHomeBLL();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Count,string Password,int Role)
        {
            if (Role==1)
            {
                if (GCHomeBLL.SelectAdmin(Count, Password,Role)!=null)
                {
                    Session["Role"] = Role;
                    Session["Count"] = Count;
                    Session["Name"] = GCHomeBLL.SelectAdmin(Count, Password, Role).AdminName;
                    Session["Adminst"] = GCHomeBLL.SelectAdmin(Count, Password, Role);
                    return RedirectToAction("Home", "Login");
                }
                else
                {
                    return Content("<script>alert('用户不存在');history.back()</script>");
                }
            }
            else if (Role==0)
            {
                if (GCHomeBLL.SelectAdmin(Count, Password, Role) != null)
                {
                    Session["Role"] = Role;
                    Session["Count"] = Count;
                    Session["Name"] = GCHomeBLL.SelectAdmin(Count, Password, Role).AdminName;
                    Session["Adminst"] = GCHomeBLL.SelectAdmin(Count, Password, Role);
                    return RedirectToAction("Home1", "Login");
                }
                else
                {
                    return Content("<script>alert('用户不存在');history.back()</script>");
                }
            }
            if (Role==2)
            {
                if (GCHomeBLL.SelectStudent(Count, Password) != null)
                {
                    Session["Role"] = 2;
                    Session["Count"] = Count;
                    Session["Name"] = GCHomeBLL.SelectStudent(Count, Password).StuName;
                    return RedirectToAction("Home", "Login");
                }
                else
                {
                    return Content("<script>alert('用户不存在');history.back()</script>");
                }
            }
            return View();
        }

        public ActionResult Home() {
            return View();
        }

        public ActionResult Home1()
        {
            return View();
        }

        public ActionResult SignOut() {
            Session["Count"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}