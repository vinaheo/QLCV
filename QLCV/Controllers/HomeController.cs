using Microsoft.AspNet.SignalR;
using QLCV.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login","Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            hubContext.Clients.All.Send("Quan");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error(string URL)
        {
            ViewBag.URL = URL;
            return View();
        }

        public ActionResult ErrorPage(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public void TestSignalR()
        {
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            //hubContext.Clients.All.Send("Quan");
            MyHub hub = new MyHub();
            hub.Send("quan");
            
        }
    }
}