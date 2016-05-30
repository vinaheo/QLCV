using QLCV.DAO;
using QLCV.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace QLCV.Controllers
{
    public class ReportController : Controller
    {
        DAO_Task dao_task = new DAO_Task();
        DAO_User dao_user = new DAO_User();
        //
        // GET: /Report/
        [WebMethod]
        public JsonResult GetTask(List<int> ids)
        {
            //ReportTaskViewModel model = new ReportTaskViewModel();
            List<CONGVIEC> cvs = new List<CONGVIEC>();
            foreach (int id in ids)
            {
                List<CONGVIEC> cvs_temp = dao_task.GetCongViecLienQuan(id);
                cvs.AddRange(cvs_temp);
            }
            //IList<CONGVIEC> cvs_temp = dao_task.GetCongViecLienQuan(id);
            //List<CONGVIEC> list = dao_task.GetCongViecs();
            //list.Add(id);
            return Json(new {list = cvs.ToList()},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexAll()
        {
            return View();
        }
	}
}