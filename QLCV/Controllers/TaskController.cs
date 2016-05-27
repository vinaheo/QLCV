using QLCV.Models.Task;
using QLCV.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;

namespace QLCV.Controllers
{
    public class TaskController : BaseController
    {
        
        DAO_Task dao_task = new DAO_Task();
        DAO_User dao_user = new DAO_User();
        //public NGUOIDUNG userLogin = Session["USER"] as NGUOIDUNG;
        //public void CheckLoggingIn()
        //{
        //    if (Session["USER"] == null)
        //    {
        //        RedirectToAction("Login", "Account");
        //    }
        //    else
        //    {
        //        userLogin = Session["USER"] as NGUOIDUNG;
        //    }
        //}
        //
        // GET: /Task/
        public ActionResult Index(int idFilter)
        {
            //CheckLoggingIn();
            ViewBag.idFilter = idFilter;
            return View();
        }

        [HttpGet]
        public ActionResult Insert()
        {
            //CheckLoggingIn();
            List<NGUOIDUNG> nds = dao_user.GetNGUOIDUNGs();
            ViewBag.NDs = nds;
            return View();
        }

        [HttpPost]
        public ActionResult Insert(TaskInsertViewModel model)
        {
            //CheckLoggingIn();
            if (model.numPC == 0)
            {
                ModelState.AddModelError("Error", "Ex: This login failed");
                return RedirectToAction("Insert");
            }
            CONGVIEC cv = new CONGVIEC();
            cv.TIEUDE = model.tieude;
            cv.NOIDUNG = model.noidung;
            cv.IDNGUOITAO = model.idNguoiTao ;
            cv.NGAYTAO = DateTime.Now;
            cv.HOANTHANH = false;
            cv.XOA = false;
            string extension = "";
            if (model.taptin != null)
            {
                cv.TAPTIN = model.taptin.FileName;
                extension = Path.GetExtension(model.taptin.FileName);
            }
            
            //cv.THUMUC = "1_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second +extension;
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
            int idFileName = dao_task.InsertTask(cv);
            if (model.taptin != null)
            {
                string fileName = idFileName.ToString() + "_" + cv.NGAYTAO.GetValueOrDefault().Day + cv.NGAYTAO.GetValueOrDefault().Month + cv.NGAYTAO.GetValueOrDefault().Year + cv.NGAYTAO.GetValueOrDefault().Hour + cv.NGAYTAO.GetValueOrDefault().Minute + cv.NGAYTAO.GetValueOrDefault().Second;
                Upload(model.taptin, fileName);
                dao_task.UpdateTaskAfterInsert(idFileName, fileName + extension);
            }

            return RedirectToAction("Detail", new { id = idFileName });
        }

        public ActionResult Detail(int id)
        {
            //CheckLoggingIn();
            NGUOIDUNG userLogin = Session["USER"] as NGUOIDUNG;
            CONGVIEC cv = dao_task.GetCONGVIEC(id);
            List<int> listNguoiDungTrongCongViec = dao_task.GetNGuoiDungTrongCongViec(id);
            if (!listNguoiDungTrongCongViec.Contains(userLogin.ID) && userLogin.ID != cv.IDNGUOITAO)
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
                    ////var path = "~/App_Data/" + thumuc;
                    //var folder = Server.MapPath("~/App_Data/" + thumuc);
                    //if (!Directory.Exists(folder))
                    //{
                    //    Directory.CreateDirectory(folder);
                    //}
                    //string fileName = tieude + "_" + date.Day + date.Month + date.Year;
                    string extension = Path.GetExtension(file.FileName);
                    //MyFile = Path.GetFileName(file.FileName);

                    //if (System.IO.File.Exists(Server.MapPath(Path.Combine(path, Path.GetFileName(file.FileName)))))
                    //{
                    //    System.IO.File.Delete(Server.MapPath(Path.Combine(path, Path.GetFileName(file.FileName))));
                    //    file.SaveAs(Server.MapPath(Path.Combine(path, Path.GetFileName(file.FileName))));
                    //}
                    //else
                    //{
                    //    file.SaveAs(Server.MapPath(Path.Combine(path, Path.GetFileName(file.FileName))));
                    //}
                    //string fileName = idNguoiTao.ToString() + "_" + date.Day + date.Month + date.Year + date.Hour + date.Minute + date.Second;
                    if (System.IO.File.Exists(Server.MapPath("~/App_Data/") + fileName + extension))
                    {
                        System.IO.File.Delete(Server.MapPath("~/App_Data/") + fileName + extension);
                        file.SaveAs(Server.MapPath("~/App_Data/") + fileName + extension);
                    }
                    else
                    {
                        file.SaveAs(Server.MapPath("~/App_Data/") + fileName + extension);
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
            return File(Server.MapPath("~/App_Data/") + file, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }
    }
}