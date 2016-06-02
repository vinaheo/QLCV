using QLCV.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Annotation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class GroupAnnotation : BaseAuthorizeAnnotation
    {
        DAO_Group dao_group = new DAO_Group();
        public string Action { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
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