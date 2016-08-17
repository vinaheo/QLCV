using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLCV.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nhập username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Nhập password")]
        public string password { get; set; }
    }
}