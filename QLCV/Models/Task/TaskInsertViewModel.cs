using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCV.Models.Task
{
    public class TaskInsertViewModel
    {
        public string tieude { get; set; }
        public string noidung { get; set; }
        public List<PHANCONG> pcs { get; set; }
        public List<string> listIDNguoiNhans { get; set; }
        public int numPC { get; set; }
        public HttpPostedFileBase taptin { get; set; }
        public List<string> listIDNguoiNhan { get; set; }
        public int idNguoiTao { get; set; }

    }
}