using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLCV.Controllers
{
    public class BaseController : Controller
    {
        //protected override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    //base.OnResultExecuted(filterContext);
        //    if (Session["USER"] == null)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
        //        {
        //            controller = "Account",
        //            action = "Login"
        //        }));
        //    }
        //}
        //protected override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //    //base.OnActionExecuted(filterContext);
        //    if (Session["USER"] == null)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
        //        {
        //            controller = "Account",
        //            action = "Login"
        //        }));
        //    }
        //}
        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //    if (Session["USER"] == null)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
        //        {
        //            controller = "Account",
        //            action = "Login"
        //        }));
        //    }
        //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = Session["USER"];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login"
                }));
            }
            base.OnActionExecuting(filterContext);
        }
	}
}