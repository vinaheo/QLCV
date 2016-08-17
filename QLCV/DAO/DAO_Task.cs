using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLCV.DAO
{
    public class DAO_Task
    {
        private QLCVEntities _context;

        public DAO_Task()
        {
            _context = new QLCVEntities();
        }

        public DAO_Task(QLCVEntities ctx)
        {
            _context = ctx;
        }

        public int InsertTask(CONGVIEC cv)
        {
            _context.CONGVIECs.Add(cv);
            _context.SaveChanges();
            int id = cv.ID;
            return id;
        }

        public void UpdateTask(CONGVIEC cv1)
        {
            CONGVIEC cv2 = _context.CONGVIECs.Find(cv1.ID);
            cv2.TIEUDE = cv1.TIEUDE;
            cv2.NOIDUNG = cv1.NOIDUNG;
            cv2.TAPTIN = cv1.TAPTIN;
            cv2.NGAYCAPNHAT = cv1.NGAYCAPNHAT;
            //cv2.PHANCONGs.Clear();
            cv2.PHANCONGs = cv1.PHANCONGs;
            _context.SaveChanges();
        }

        public void UpdateTaskAfterInsert(int id, string thumuc)
        {
            CONGVIEC cv = _context.CONGVIECs.Find(id);
            cv.THUMUC = thumuc;
            _context.SaveChanges();
        }
        public List<CONGVIEC> GetCONGVIECs()
        {
            List<CONGVIEC> cvs = _context.CONGVIECs.ToList();
            return cvs;
        }

        public CONGVIEC GetCONGVIEC(int id)
        {
            CONGVIEC cv = _context.CONGVIECs.Include("PHANCONGs").Where(a => a.ID == id).FirstOrDefault();
            return cv;
        }

        public List<int> GetIDNGUOIDUNGinPHANCONG(int idcv, int idpc)
        {
            List<int> idnds = _context.PHANCONGs.Where(a => a.IDCONGVIEC == idcv && a.IDPHANCONG == idpc).Select(a => a.NGUOIDUNG.ID).ToList();
            return idnds;
        }

        public PHANCONG GetPHANCONG(int idcv, int idpc)
        {
            PHANCONG pc = _context.PHANCONGs.Where(a => a.IDCONGVIEC == idcv && a.IDPHANCONG == idpc).FirstOrDefault();
            return pc;
        }

        public List<BAOCAOCONGVIEC> GetBAOCAO(int idcv, int idpc)
        {
            List<BAOCAOCONGVIEC> bcs = _context.BAOCAOCONGVIECs.Include("NGUOIDUNG").Where(a => a.IDCONGVIEC == idcv && a.IDPHANCONG == idpc).ToList();
            return bcs;
        }

        public void InsertBAOCAO(BAOCAOCONGVIEC bc)
        {
            _context.BAOCAOCONGVIECs.Add(bc);
            _context.SaveChanges();
        }

        public List<CONGVIEC> GetCongViecLienQuan(int idNguoiDung)
        {
            var param = new SqlParameter("@idNguoiDung", idNguoiDung);
            var result = _context.CONGVIECs.SqlQuery("select DISTINCT cv.* from CONGVIEC cv, PHANCONG pc where cv.IDNGUOITAO = @idNguoiDung or pc.IDNGUOINHAN = @idNguoiDung and pc.IDCONGVIEC = cv.ID", param).ToList();
            return result;
        }

        public List<CONGVIEC> GetCongViecTheoPhanCong(int idNguoiDung, int trangthaiphancong)
        {
            var param = new SqlParameter("@idNguoiDung", idNguoiDung);
            var param1 = new SqlParameter("@trangthaiphancong", trangthaiphancong);
            var result = _context.CONGVIECs.SqlQuery("select * from CONGVIEC where ID IN (select DISTINCT IDCONGVIEC from PHANCONG where IDTRANGTHAI = @trangthaiphancong and IDNGUOINHAN = @idNguoiDung) AND HOANTHANH = 'false'", param, param1).ToList();
            return result;
        }

        public List<int> GetNGuoiDungTrongCongViec(int idCongViec)
        {
            var param = new SqlParameter("@idCongViec", idCongViec);
            var result = _context.Database.SqlQuery<int>("select ID from NGUOIDUNG where ID in (select pc.IDNGUOINHAN from CONGVIEC cv, PHANCONG pc where cv.ID = @idCongViec and cv.ID = pc.IDCONGVIEC group by pc.IDNGUOINHAN)", param).ToList();
            return result;
        }

        public void UpdateTrangThaiPhanCong(int idCongViec, int idPhanCong)
        {
            var result = _context.PHANCONGs.Where(a => a.IDCONGVIEC == idCongViec && a.IDPHANCONG == idPhanCong).ToList();
            result.ForEach(a =>
            {
                if (a.IDTRANGTHAI == 5)
                {
                    a.NGAYCAPNHAT = DateTime.Now;
                    a.IDTRANGTHAI = 3;
                }
                else
                {
                    a.NGAYCAPNHAT = DateTime.Now;
                    a.IDTRANGTHAI = a.IDTRANGTHAI + 1;
                }
            });
            //result.ForEach(a => a.IDTRANGTHAI = a.IDTRANGTHAI + 1);
            _context.SaveChanges();
        }

        public TRANGTHAI GetTrangThai(int id)
        {
            var result = _context.TRANGTHAIs.Find(id);
            return result;
        }

        public void UpdateTrangThaiCongViec(int idCongViec)
        {
            CONGVIEC cv = _context.CONGVIECs.Find(idCongViec);
            if (cv.HOANTHANH == true)
            {
                cv.NGAYCAPNHAT = DateTime.Now;
                cv.HOANTHANH = false;
            }
            else
            {
                cv.NGAYCAPNHAT = DateTime.Now;
                cv.HOANTHANH = true;
            }
            _context.SaveChanges();
        }

        public List<PHANCONG> GetPhanCongTheoCongViec(int idCongViec)
        {
            var result = _context.PHANCONGs.Where(a => a.IDCONGVIEC == idCongViec).ToList();
            return result;
        }

        public List<CONGVIEC> GetCongViecs()
        {
            var result = _context.CONGVIECs.ToList();
            return result;
        }

        public List<CONGVIEC> GetCongViecTrongNgay(DateTime datetime)
        {
            var result = _context.PHANCONGs.Where(a => a.NGAYKETTHUC == datetime).Select(a => a.CONGVIEC).ToList();
            return result;
        }

        public List<PHANCONG> GetPhanCongLienQuan(int idNguoiDung)
        {
            var result = _context.PHANCONGs.Where(a => a.IDNGUOINHAN == idNguoiDung).ToList();
            return result;
        }

    }
}