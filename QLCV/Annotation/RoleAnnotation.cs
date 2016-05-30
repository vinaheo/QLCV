using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Annotation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAnnotation : AuthorizeAttribute
    {
        public int RoleId { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var result = false;
            var session = HttpContext.Current.Session["USER"] as NGUOIDUNG;
            int role_nguoidung = session.IDCHUCVU.GetValueOrDefault();
            switch(RoleId)
            {
                case 1:
                    if (role_nguoidung != 1)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                    break;
                case 2:
                    result = true;
                    break;
            }
            return result;
        }
    }
}