using QLCV.Annotation;
using QLCV.DAO;
using QLCV.Models.Account;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCV.Controllers
{
    public class AccountController : Controller
    {
        private DAO_User dao_user;
        private DAO_Group dao_group;
        private DAO_Task dao_task;
        private QLCVEntities _context;

        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.UTF8Encoding.UTF8.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.UTF8Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public AccountController()
        {
            _context = new QLCVEntities();
            dao_group = new DAO_Group(_context);
            dao_user = new DAO_User(_context);
            dao_task = new DAO_Task(_context);
        }
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
            if (ModelState.IsValid)
            {
                try
                {
                    if (dao_user.CheckLogin(model.username, model.password))
                    {
                        if (dao_user.GetNguoiDung(model.username, model.password).TRANGTHAI == false)
                        {
                            ViewBag.Message = ConfigurationManager.AppSettings["AccountDisable"];
                            return View();
                        }
                        else
                        {
                            Session["USER"] = dao_user.GetNguoiDung(model.username, model.password);
                            return RedirectToAction("Index", "Task", new { idFilter = 0 });
                        }
                        
                    }
                    else
                    {
                        ViewBag.Message = ConfigurationManager.AppSettings["LoginError"];
                        return View();
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("ErrorPage", "Home", new { message = ConfigurationManager.AppSettings["DatabaseError"] });
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        [GroupAnnotation(Action = "~/Account/ManagementAccount")]
        public ActionResult ManagementAccount()
        {
            return View();
        }

        public string InsertNguoiDung(NGUOIDUNG nd)
        {
            nd.TRANGTHAI = true;
            nd.NGAYTAO = DateTime.Now;
            nd.MATKHAU = ConfigurationManager.AppSettings["PasswordDefault"];
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
                ViewBag.Message = ConfigurationManager.AppSettings["OldPasswordWrong"];
            }
            else
            {
                if (newPassword != confirmNewPassword)
                {
                    ViewBag.Message = ConfigurationManager.AppSettings["NewPasswordMatch"];
                }
                else
                {
                    nd.MATKHAU = newPassword;
                    dao_user.UpdateNguoiDung(nd, newPassword);
                    ViewBag.Message = ConfigurationManager.AppSettings["ChangePasswordSuccess"];
                }
            }
            return View();
        }

        public string ResetPassword(int id)
        {
            NGUOIDUNG nd = dao_user.GetNguoiDungById(id);
            dao_user.UpdateNguoiDung(nd, ConfigurationManager.AppSettings["PasswordDefault"]);
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

        [GroupAnnotation(Action = "~/Account/ManagementAction")]
        public ActionResult ManagementAction()
        {
            return View();
        }

        [GroupAnnotation(Action = "~/Account/ManagementGroup")]
        public ActionResult ManagementGroup()
        {
            return View();
        }

        public string InsertGroup(string groupName, List<int> listIdActions)
        {
            GROUP gr = new GROUP();
            gr.TENGROUP = groupName;
            foreach (int idAction in listIdActions)
            {
                gr.ACTIONS.Add(dao_group.GetActionById(idAction));
            }
            dao_group.InsertGroup(gr);
            return "true";
        }

        public string EditGroup(int groupId, string groupName, List<int> listIdActions)
        {
            List<ACTION> actions = new List<ACTION>();
            foreach (int idAction in listIdActions)
            {
                actions.Add(dao_group.GetActionById(idAction));
            }
             var newGroup = new GROUP {
                 ACTIONS = actions,
                 TENGROUP = groupName,
            };
             dao_group.EditGroup(groupId, newGroup);
            return "true";
        }

        #region Dispose pattern

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Free any other managed objects here.
                //
                if (_context != null)
                    _context.Dispose();
            }

            // Free any unmanaged objects here.
            //

            // Call the base class implementation.
            base.Dispose(disposing);
        }

        #endregion
    }
}