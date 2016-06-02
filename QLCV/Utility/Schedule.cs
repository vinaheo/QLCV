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
        private System.Threading.Timer timer;
        public void SetUpTimer(TimeSpan alertTime)
        {
            DateTime current = DateTime.Now;
            MailReminder mailReminder = new MailReminder();
            TimeSpan timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
            {
                return;//time already passed
            }
            this.timer = new System.Threading.Timer(x =>
            {
                mailReminder.DoSentMail();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        
    }
}