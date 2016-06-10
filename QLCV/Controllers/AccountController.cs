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
        DAO_Group dao_group = new DAO_Group();
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

        public ActionResult ManagementAccount()
        {
            return View();
        }

        public string InsertNguoiDung(NGUOIDUNG nd)
        {
            nd.TRANGTHAI = true;
            nd.NGAYTAO = DateTime.Now;
            nd.MATKHAU = "123";
            dao_user.InsertNguoiDung(nd);
            return "true";
        }

        public string UpdateNguoiDung(NGUOIDUNG nd)
        {
            dao_user.UpdateNguoiDung(nd);
            return "true";
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {
            NGUOIDUNG nd = Session["USER"] as NGUOIDUNG;
            if (oldPassword != nd.MATKHAU)
            {
                ViewBag.Message = "Khong giong mat khau cu";
            }
            else
            {
                if (newPassword != confirmNewPassword)
                {
                    ViewBag.Message = "2 mat khau khong giong nhau";
                }
                else
                {
                    nd.MATKHAU = newPassword;
                    dao_user.UpdateNguoiDung(nd);
                    ViewBag.Message = "Doi mat khau thanh cong";
                }
            }
            return View();
        }

        public string ResetPassword(int id)
        {
            NGUOIDUNG nd = dao_user.GetNguoiDungById(id);
            nd.MATKHAU = "123";
            dao_user.UpdateNguoiDung(nd);
            return "true";
        }

        public string ChangeTrangThai(int id)
        {
            NGUOIDUNG nd = dao_user.GetNguoiDungById(id);
            if (nd.TRANGTHAI == true)
            {
                nd.TRANGTHAI = false;
            }
            else
            {
                nd.TRANGTHAI = true;
            }
            dao_user.UpdateNguoiDung(nd);
            return "true";
        }

        public string UpdateAction(ACTION ac)
        {
            dao_group.UpdateAction(ac);
            return "true";
        }

        public string InsertAction(ACTION ac)
        {
            dao_group.InsertAction(ac);
            return "true";
        }

        public ActionResult ManagementAction()
        {
            return View();
        }

        public ActionResult ManagementGroup()
        {
            return View();
        }

        public string InsertGroup(GROUP gr)
        {
            dao_group.InsertGroup(gr);
            return "true";
        }
	}
}