using QLCV.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLCV.Annotation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class GroupAnnotation : BaseAuthorizeAnnotation
    {
        public string Action { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["USER"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login"
                }));
            }
            else
            {
                DAO_Group dao_group = new DAO_Group();
                var session = HttpContext.Current.Session["USER"] as NGUOIDUNG;
                List<GROUP> GroupAction = dao_group.GetGroupContainAction(Action);
                if (!GroupAction.Any(x => x.ID == session.IDGROUP))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "ErrorPage",
                        message = ConfigurationManager.AppSettings["PermissionError"]
                    }));
                }
                
            }
            
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            using (var context = new QLCVEntities())
            {
                var dao_group = new DAO_Group(context);
                var result = false;
                var session = HttpContext.Current.Session["USER"] as NGUOIDUNG;
                List<GROUP> GroupUser = dao_group.GetGroupContainUser(session.ID);
                List<GROUP> GroupAction = dao_group.GetGroupContainAction(Action);
                //if (GroupUser.SequenceEqual(GroupAction))
                //{
                //    result = true;
                //}
                foreach (GROUP gUser in GroupUser)
                {
                    foreach (GROUP gAction in GroupAction)
                    {
                        if (gUser.ID == gAction.ID)
                        {
                            result = true;
                            break;
                        }
                    }
                }
                //var a = GroupUser.SequenceEqual(GroupAction);

                //var share = GroupUser.Intersect(GroupAction).Any();
                //if (GroupUser.Distinct().Count() > share || GroupAction.Distinct().Count() > share)
                return result;
            }

        }
    }
}