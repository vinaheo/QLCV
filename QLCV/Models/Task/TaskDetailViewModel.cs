using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCV.Models.Task
{
    public class TaskDetailViewModel
    {
        public int id { get; set; }
        public string tieude { get; set; }
        public string noidung { get; set; }
        public List<PHANCONG> pcs { get; set; }
        public string thumuc { get; set; }
        public string taptin { get; set; }
        public int idNguoiTao { get; set; }
        public HttpPostedFileBase taptinUpload { get; set; }
        //id nguoi dung
        public List<string> listId { get; set; }
        public List<string> listIDNguoiNhans { get; set; }
        public int numPC { get; set; }
        public Boolean hoanthanh { get; set; }
        public DateTime ngaycapnhat { get; set; }
        public DateTime ngaytao { get; set; }
    }
}