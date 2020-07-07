using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace q.Filter
{
    public class LoginAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Count"]==null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}