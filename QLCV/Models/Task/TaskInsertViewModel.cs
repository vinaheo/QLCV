using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLCV.Models.Task
{
    public class TaskInsertViewModel
    {
        [Required(ErrorMessage = "Nhập tiêu đề")]
        [MinLength(5, ErrorMessage ="Tiêu đề phải gồm 5 ký tự")]
        public string tieude { get; set; }
        [Required(ErrorMessage = "Nhập nội dung")]
        [MinLength(5, ErrorMessage = "Nội dung phải gồm 5 ký tự")]
        public string noidung { get; set; }
        public List<PHANCONG> pcs { get; set; }
        public List<string> listIDNguoiNhans { get; set; }
        [Range(1,100,ErrorMessage = "Phân công không được nhỏ hơn 0")]
        public int numPC { get; set; }
        public HttpPostedFileBase taptin { get; set; }
        public List<string> listIDNguoiNhan { get; set; }
        public int idNguoiTao { get; set; }

    }
}