using QLCV.DAO;
using QLCV.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class AccountController : Controller
    {
        DAO_User dao_user = new DAO_User();
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(LoginViewModel model)
        {
            if (dao_user.CheckLogin(model.username, model.password))
            {
                Session["USER"] = dao_user.GetNguoiDung(model.username, model.password);
                return RedirectToAction("Index", "Task", new { idFilter =0});
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
	}
}