using QLCV.Models.Task;
using QLCV.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using QLCV.Annotation;
using QLCV.SentMail;
using Microsoft.AspNet.SignalR;
using QLCV.Utility;

namespace QLCV.Controllers
{
    public class TaskController : BaseController
    {
        private DAO_Task dao_task;
        private DAO_User dao_user;
        private DAO_Group dao_group;
        private QLCVEntities _context;

        public TaskController()
        {
            _context = new QLCVEntities();
            dao_group = new DAO_Group(_context);
            dao_user = new DAO_User(_context);
            dao_task = new DAO_Task(_context);
        }

        MailInsertTask mailInsertTask = new MailInsertTask();
        MailUpdateTask mailUpdateTask = new MailUpdateTask();

        public ActionResult Index(int idFilter)
        {
            ViewBag.idFilter = idFilter;
            return View();
        }

        [HttpGet]
        [GroupAnnotation(Action = "~/Task/Insert")]
        public ActionResult Insert()
        {
            List<NGUOIDUNG> nds = dao_user.GetNGUOIDUNGs();
            ViewBag.NDs = nds;
            return View();
        }

        [HttpPost]
        [GroupAnnotation(Action = "~/Task/Insert")]
        public ActionResult Insert(TaskInsertViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.numPC == 0)
                    {
                        List<NGUOIDUNG> nds = dao_user.GetNGUOIDUNGs();
                        ViewBag.NDs = nds;
                        ViewBag.Message = "Phải có phân công";
                        return View();
                    }
                    CONGVIEC cv = new CONGVIEC();
                    cv.TIEUDE = model.tieude;
                    cv.NOIDUNG = model.noidung;
                    cv.IDNGUOITAO = model.idNguoiTao;
                    cv.NGAYTAO = DateTime.Now;
                    cv.NGAYCAPNHAT = DateTime.Now;
                    cv.HOANTHANH = false;
                    cv.XOA = false;
                    string extension = "";
                    if (model.taptin != null)
                    {
                        cv.TAPTIN = model.taptin.FileName;
                        extension = Path.GetExtension(model.taptin.FileName);
                    }

                    List<PHANCONG> pcs = new List<PHANCONG>();
                    for (int i = 0; i < model.numPC; i++)
                    {
                        string[] idNguoiNhans = model.listIDNguoiNhans[i].ToString().Split(',');
                        foreach (string id in idNguoiNhans)
                        {
                            PHANCONG pc = new PHANCONG();
                            pc.IDPHANCONG = i;
                            pc.TENPHANCONG = model.pcs[i].TENPHANCONG;
                            pc.NOIDUNG = model.pcs[i].NOIDUNG;
                            pc.IDNGUOINHAN = int.Parse(id);
                            pc.NGAYBATDAU = model.pcs[i].NGAYBATDAU;
                            pc.NGAYKETTHUC = model.pcs[i].NGAYKETTHUC;
                            
                            pc.IDTRANGTHAI = 1;
                            pcs.Add(pc);
                        }
                    }
                    //Upload(model.taptin, DateTime.Now, 1);
                    cv.PHANCONGs = pcs;
                    int idCongViec = dao_task.InsertTask(cv);
                    if (model.taptin != null)
                    {
                        string fileName = idCongViec.ToString() + "_" + cv.NGAYTAO.GetValueOrDefault().Day + cv.NGAYTAO.GetValueOrDefault().Month + cv.NGAYTAO.GetValueOrDefault().Year + cv.NGAYTAO.GetValueOrDefault().Hour + cv.NGAYTAO.GetValueOrDefault().Minute + cv.NGAYTAO.GetValueOrDefault().Second;
                        Upload(model.taptin, fileName);
                        dao_task.UpdateTaskAfterInsert(idCongViec, fileName + extension);
                    }
                    List<int> listNguoiDungTrongCongViec = dao_task.GetNGuoiDungTrongCongViec(idCongViec);
                    //foreach (int idND in listNguoiDungTrongCongViec.Distinct())
                    //{
                    //    mailInsertTask.DoSentMail(dao_user.GetNguoiDungById(cv.IDNGUOITAO.GetValueOrDefault()).TENNGUOIDUNG, cv.TIEUDE, dao_user.GetNguoiDungById(idND).EMAIL);
                    //}

                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
                    foreach (string IDNguoiNhan in model.listIDNguoiNhans)
                    {
                        string[] idNguoiNhans = IDNguoiNhan.ToString().Split(',');
                        foreach (string i in idNguoiNhans)
                        {
                            hubContext.Clients.All.Send("Bạn có công việc mới!", i);
                        }
                    }
                    //hubContext.Clients.All.Send("Bạn có công việc mới!");
                    return RedirectToAction("Detail", new { id = idCongViec });
                }
                catch (Exception ex)
                {
                    //return ex.;
                    //return RedirectToAction("ErrorPage", "Home", new { message = ex});
                    return RedirectToAction("ErrorPage", "Home", new { message = ConfigurationManager.AppSettings["DatabaseError"] });
                }
            }
            else
            {
                List<NGUOIDUNG> nds = dao_user.GetNGUOIDUNGs();
                ViewBag.NDs = nds;
                return View(model);
                //return RedirectToAction("ErrorPage", "Home", new { message = ConfigurationManager.AppSettings["DatabaseError"] });
            }
        }

        public ActionResult Detail(int id)
        {
            //CheckLoggingIn();
            NGUOIDUNG userLogin = Session["USER"] as NGUOIDUNG;
            CONGVIEC cv = dao_task.GetCONGVIEC(id);
            List<int> listNguoiDungTrongCongViec = dao_task.GetNGuoiDungTrongCongViec(id);
            if (!listNguoiDungTrongCongViec.Contains(userLogin.ID) && userLogin.ID != cv.IDNGUOITAO && userLogin.IDCHUCVU != 1)
            {
                return RedirectToAction("Login", "Account");
            }
            List<NGUOIDUNG> nds1 = dao_user.GetNGUOIDUNGs();
            ViewBag.NDs = nds1;
            //CONGVIEC cv = dao_task.GetCONGVIEC(id);
            TaskDetailViewModel model = new TaskDetailViewModel();
            model.id = cv.ID;
            model.tieude = cv.TIEUDE;
            model.noidung = cv.NOIDUNG;
            model.ngaytao = cv.NGAYTAO.GetValueOrDefault();
            if (cv.NGAYCAPNHAT != null)
            {
                model.ngaycapnhat = cv.NGAYCAPNHAT.GetValueOrDefault();
            }
            //model.ngaycapnhat = cv.NGAYCAPNHAT.GetValueOrDefault();
            model.hoanthanh = cv.HOANTHANH.GetValueOrDefault();
            model.idNguoiTao = cv.IDNGUOITAO.GetValueOrDefault();
            //model.pcs = cv.PHANCONGs.ToList();
            List<PHANCONG> pcs = new List<PHANCONG>();
            List<string> listId = new List<string>();
            List<int> listIdPC = new List<int>();
            foreach (PHANCONG pc in cv.PHANCONGs)
            {
                List<int> nds = dao_task.GetIDNGUOIDUNGinPHANCONG(cv.ID, pc.IDPHANCONG);
                listId.Add(string.Join(",", nds));
                listIdPC.Add(pc.IDPHANCONG);
            }
            List<int> listIdPCDistinct = listIdPC.Distinct().ToList();
            for (int i = 0; i < listIdPC.Distinct().Count(); i++)
            {
                PHANCONG pc = dao_task.GetPHANCONG(cv.ID, listIdPCDistinct[i]);
                pcs.Add(pc);
            }
            model.pcs = pcs;
            model.listId = listId.ToList();
            model.numPC = listIdPC.Distinct().Count();
            model.taptin = cv.TAPTIN;
            model.thumuc = cv.THUMUC;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TaskDetailViewModel model)
        {
            CONGVIEC cv = dao_task.GetCONGVIEC(model.id);
            cv.TIEUDE = model.tieude;
            cv.NOIDUNG = model.noidung;
            if (model.taptinUpload != null)
            {
                cv.TAPTIN = model.taptinUpload.FileName;
                string extension = Path.GetExtension(model.taptinUpload.FileName);
                string fileName = model.id.ToString() + "_" + cv.NGAYTAO.GetValueOrDefault().Day + cv.NGAYTAO.GetValueOrDefault().Month + cv.NGAYTAO.GetValueOrDefault().Year + cv.NGAYTAO.GetValueOrDefault().Hour + cv.NGAYTAO.GetValueOrDefault().Minute + cv.NGAYTAO.GetValueOrDefault().Second;
                cv.THUMUC = fileName + extension;
                Upload(model.taptinUpload, fileName);
            }
            List<PHANCONG> pcs = new List<PHANCONG>();
            for (int i = 0; i < model.numPC; i++)
            {
                string[] idNguoiNhans = model.listIDNguoiNhans[i].ToString().Split(',');
                foreach (string id in idNguoiNhans)
                {
                    PHANCONG pc = new PHANCONG();
                    pc.IDPHANCONG = i;
                    pc.TENPHANCONG = model.pcs[i].TENPHANCONG;
                    pc.NOIDUNG = model.pcs[i].NOIDUNG;
                    pc.IDNGUOINHAN = int.Parse(id);
                    pc.NGAYBATDAU = model.pcs[i].NGAYBATDAU;
                    pc.NGAYKETTHUC = model.pcs[i].NGAYKETTHUC;
                    pc.IDTRANGTHAI = model.pcs[i].IDTRANGTHAI;
                    //pc.NGAYCAPNHAT = DateTime.Now;
                    pcs.Add(pc);
                }
            }
            cv.NGAYCAPNHAT = DateTime.Now;
            cv.PHANCONGs = pcs;
            dao_task.UpdateTask(cv);
            List<int> listNguoiDungTrongCongViec = dao_task.GetNGuoiDungTrongCongViec(model.id);
            NGUOIDUNG userLogin = Session["USER"] as NGUOIDUNG;

            //foreach (int idND in listNguoiDungTrongCongViec.Distinct())
            //{
            //    mailUpdateTask.DoSentMail(userLogin.TENNGUOIDUNG, cv.TIEUDE, dao_user.GetNguoiDungById(idND).EMAIL);
            //}
            return RedirectToAction("Detail", new { id = model.id });
        }

        public string InsertBAOCAO(string idnt, string idcv, string idpc, string noidung)
        {
            BAOCAOCONGVIEC bc = new BAOCAOCONGVIEC();
            bc.IDNGUOITAO = int.Parse(idnt);
            bc.IDCONGVIEC = int.Parse(idcv);
            bc.IDPHANCONG = int.Parse(idpc);
            bc.NOIDUNG = noidung;
            bc.NGAYTAO = DateTime.Now;
            dao_task.InsertBAOCAO(bc);
            return "true";
        }

        public string TiepNhanPhanCong(int idCongViec, int idPhanCong)
        {
            dao_task.UpdateTrangThaiPhanCong(idCongViec, idPhanCong);
            return "true";
        }

        public string HoanThanhCongViec(int idCongViec)
        {
            List<PHANCONG> pcs = dao_task.GetPhanCongTheoCongViec(idCongViec);
            List<int> trangthaiphancong = new List<int>();
            foreach (PHANCONG pc in pcs)
            {
                trangthaiphancong.Add(pc.IDTRANGTHAI.GetValueOrDefault());
            }
            if (!trangthaiphancong.Contains(4))
            {
                return "false";
            }
            else
            {
                dao_task.UpdateTrangThaiCongViec(idCongViec);
                return "true";
            }

        }

        [HttpPost]
        public Boolean Upload(HttpPostedFileBase file, string fileName)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName);

                    //if (System.IO.File.Exists(Server.MapPath("~/App_Data/") + fileName + extension))
                    //{
                    //    System.IO.File.Delete(Server.MapPath("~/App_Data/") + fileName + extension);
                    //    file.SaveAs(Server.MapPath("~/App_Data/") + fileName + extension);
                    //}
                    //else
                    //{
                    //    file.SaveAs(Server.MapPath("~/App_Data/") + fileName + extension);
                    //}
                    if (System.IO.File.Exists(ConfigurationManager.AppSettings["FolderUpload"] + fileName + extension))
                    {
                        System.IO.File.Delete(ConfigurationManager.AppSettings["FolderUpload"] + fileName + extension);
                        file.SaveAs(ConfigurationManager.AppSettings["FolderUpload"] + fileName + extension);
                    }
                    else
                    {
                        file.SaveAs(ConfigurationManager.AppSettings["FolderUpload"] + fileName + extension);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public FileResult Download(string file)
        {
            return File(ConfigurationManager.AppSettings["FolderUpload"] + file, System.Net.Mime.MediaTypeNames.Application.Octet, file);
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