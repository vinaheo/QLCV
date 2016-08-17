using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events;
using DayPilot.Web.Mvc.Events.Month;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Newtonsoft.Json;
using QLCV.DAO;
using QLCV.Models;
using QLCV.Models.Report;
using QLCV.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace QLCV.Controllers
{
    public class ReportController : Controller
    {
        private DAO_Task dao_task;
        private DAO_User dao_user;
        private DAO_Group dao_group;
        private QLCVEntities _context;

        public ReportController()
        {
            _context = new QLCVEntities();
            dao_group = new DAO_Group(_context);
            dao_user = new DAO_User(_context);
            dao_task = new DAO_Task(_context);
        }

        // GET: /Report/
        [WebMethod]
        public string GetTask(List<int> ids)
        {
            List<CONGVIEC> cvs = new List<CONGVIEC>();
            List<CONGVIECAjax> cvas = new List<CONGVIECAjax>();
            foreach (int id in ids)
            {
                //List<CONGVIECAjax> cvas_temp = new List<CONGVIECAjax>();
                List<CONGVIEC> cvs_temp = dao_task.GetCongViecLienQuan(id);
                //foreach (CONGVIEC cv in cvs_temp.Distinct())
                //{
                //    CONGVIECAjax cva = new CONGVIECAjax();
                //    cva.id = cv.ID;
                //    cva.tieude = cv.TIEUDE;
                //    cva.ngaytao = cv.NGAYTAO.GetValueOrDefault();
                //    cva.hoanthanh = cv.HOANTHANH.GetValueOrDefault() ;
                //    cvas_temp.Add(cva);
                //}
                cvs.AddRange(cvs_temp);
                //cvas.AddRange(cvas_temp);
            }
            foreach (CONGVIEC cv in cvs.Distinct())
            {
                CONGVIECAjax cva = new CONGVIECAjax();
                cva.id = cv.ID;
                cva.tieude = cv.TIEUDE;
                cva.ngaytao = cv.NGAYTAO.GetValueOrDefault();
                cva.hoanthanh = cv.HOANTHANH.GetValueOrDefault();
                cvas.Add(cva);
            }

            var output = JsonConvert.SerializeObject(cvas.Distinct(), Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            });

            return output;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexAll()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.InitialDate = DateTime.Now;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var nd = Session["USER"] as NGUOIDUNG;
            var phanCongs = dao_task.GetPhanCongLienQuan(nd.ID);
            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();
            foreach (PHANCONG pc in phanCongs)
            {
                CalendarEvent ce = new CalendarEvent();
                ce.id = pc.IDPHANCONG;
                ce.text = pc.NOIDUNG;
                ce.start_date = pc.NGAYBATDAU.GetValueOrDefault();
                ce.end_date = pc.NGAYKETTHUC.GetValueOrDefault();
                calendarEvents.Add(ce);
            }
            var data = new SchedulerAjaxData(calendarEvents);

            return (ContentResult)data;
        }

        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        class Dpc : DayPilotMonth
        {
            //DataClasses1DataContext db = new DataClasses1DataContext();
            QLCVEntities context = new QLCVEntities();
            protected override void OnInit(InitArgs e)
            {
                Update();
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = from e in context.PHANCONGs select e;

                DataIdField = "IDPHANCONG";
                DataStartField = "NGAYBATDAU";
                DataEndField = "NGAYKETTHUC";
                DataTextField = "TENPHANCONG";
            }

        }
    }
}