using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using QLCV.SentMail;
namespace QLCV.Utility
{
    public class Schedule
    {
        public System.Threading.Timer timer;
        public void SetUpTimer(TimeSpan alertTime)
        {
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
            {
                return;//time already passed
            }
            this.timer = new System.Threading.Timer(x =>
            {
                SendMailReminder();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        public void SendMailReminder()
        {
            MailReminder mailReminder = new MailReminder();
            mailReminder.DoSentMail("quanvmh1407@gmail.com");
            //DAO_Task dao_task = new DAO_Task();
            //DAO_User dao_user = new DAO_User();
            //List<CONGVIEC> cvs = dao_task.GetCongViecTrongNgay(DateTime.Now);

            //foreach (CONGVIEC cv in cvs)
            //{
            //    List<int> idnds = dao_task.GetNGuoiDungTrongCongViec(cv.ID);
            //    foreach (int id in idnds.Distinct())
            //    {
            //        mailReminder.DoSentMail(dao_user.GetNguoiDungById(id).EMAIL);
            //    }
            //}
            //mailReminder.DoSentMail();
        }
    }
}