using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLCV.Annotation
{
    public class CheckLoginAnnotation : ValidationAttribute
    {
        public NGUOIDUNG NguoiDung { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //if (HttpContext.Current.Session["USER"] == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            return base.IsValid(value, validationContext);
        }
    }
}