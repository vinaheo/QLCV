using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using QLCV.SentMail;
using QLCV.Utility;
using QLCV.DAO;
using System.Threading;
namespace QLCV
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            Schedule s = new Schedule();
            s.SetUpTimer(new TimeSpan(7, 00, 00));
            Schedule s1 = new Schedule();
            s1.SetUpTimer(new TimeSpan(12, 00, 00));
            Schedule s2 = new Schedule();
            s2.SetUpTimer(new TimeSpan(16, 00, 00));
            //MailReminder mailReminder = new MailReminder();
            //mailReminder.DoSentMail();
        }

        //public System.Threading.Timer timer;
        //public void SetUpTimer(TimeSpan alertTime)
        //{
        //    DateTime current = DateTime.Now;
        //    TimeSpan timeToGo = alertTime - current.TimeOfDay;
        //    if (timeToGo < TimeSpan.Zero)
        //    {
        //        return;//time already passed
        //    }
        //    this.timer = new System.Threading.Timer(x =>
        //    {
        //        SendMailReminder();
        //    }, null, timeToGo, Timeout.InfiniteTimeSpan);
        //}

        //public void SendMailReminder()
        //{
        //    MailReminder mailReminder = new MailReminder();
        //    mailReminder.DoSentMail("quanvmh1407@gmail.com");
        //    //DAO_Task dao_task = new DAO_Task();
        //    //DAO_User dao_user = new DAO_User();
        //    //List<CONGVIEC> cvs = dao_task.GetCongViecTrongNgay(DateTime.Now);

        //    //foreach (CONGVIEC cv in cvs)
        //    //{
        //    //    List<int> idnds = dao_task.GetNGuoiDungTrongCongViec(cv.ID);
        //    //    foreach (int id in idnds.Distinct())
        //    //    {
        //    //        mailReminder.DoSentMail(dao_user.GetNguoiDungById(id).EMAIL);
        //    //    }
        //    //}
        //    //mailReminder.DoSentMail();
        //}
    }
}
