using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLCV.DAO
{
    public class DAO_Task
    {
        public int InsertTask(CONGVIEC cv)
        {
            using(QLCVEntities e = new QLCVEntities())
            {
                e.CONGVIECs.Add(cv);
                e.SaveChanges();
                int id = cv.ID;
                return id;
            }
        }

        public void UpdateTask(CONGVIEC cv1)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                CONGVIEC cv2 = e.CONGVIECs.Find(cv1.ID);
                cv2.TIEUDE = cv1.TIEUDE;
                cv2.NOIDUNG = cv1.NOIDUNG;
                cv2.TAPTIN = cv1.TAPTIN;
                cv2.NGAYCAPNHAT = cv1.NGAYCAPNHAT;
                cv2.PHANCONGs.Clear();
                cv2.PHANCONGs = cv1.PHANCONGs;
                e.SaveChanges();
            }
        }

        public void UpdateTaskAfterInsert(int id, string thumuc)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                CONGVIEC cv = e.CONGVIECs.Find(id);
                cv.THUMUC = thumuc;
                e.SaveChanges();
            }
        }
        public List<CONGVIEC> GetCONGVIECs()
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                List<CONGVIEC> cvs = e.CONGVIECs.ToList();
                return cvs;
            }
        }

        public CONGVIEC GetCONGVIEC(int id)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                CONGVIEC cv = e.CONGVIECs.Include("PHANCONGs").Where(a => a.ID == id).FirstOrDefault();
                return cv;
            }
        }

        public List<int> GetIDNGUOIDUNGinPHANCONG(int idcv, int idpc)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                List<int> idnds = e.PHANCONGs.Where(a => a.IDCONGVIEC == idcv && a.IDPHANCONG == idpc).Select(a => a.NGUOIDUNG.ID).ToList();
                return idnds;
            }
        }

        public PHANCONG GetPHANCONG(int idcv, int idpc)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                PHANCONG pc = e.PHANCONGs.Where(a => a.IDCONGVIEC == idcv && a.IDPHANCONG == idpc).FirstOrDefault();
                return pc;
            }
        }


        public List<BAOCAOCONGVIEC> GetBAOCAO(int idcv, int idpc)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                List<BAOCAOCONGVIEC> bcs = e.BAOCAOCONGVIECs.Include("NGUOIDUNG").Where(a => a.IDCONGVIEC == idcv && a.IDPHANCONG == idpc).ToList();
                return bcs;
            }
        }

        public void InsertBAOCAO(BAOCAOCONGVIEC bc)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                e.BAOCAOCONGVIECs.Add(bc);
                e.SaveChanges();
            }
        }

        public List<CONGVIEC> GetCongViecLienQuan(int idNguoiDung)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var param = new SqlParameter("@idNguoiDung", idNguoiDung);
                var result = e.CONGVIECs.SqlQuery("select DISTINCT cv.* from CONGVIEC cv, PHANCONG pc where cv.IDNGUOITAO = @idNguoiDung or pc.IDNGUOINHAN = @idNguoiDung and pc.IDCONGVIEC = cv.ID", param).ToList();
                return result;
            }
        }

        public List<CONGVIEC> GetCongViecTheoPhanCong(int idNguoiDung, int trangthaiphancong)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var param = new SqlParameter("@idNguoiDung", idNguoiDung);
                var param1 = new SqlParameter("@trangthaiphancong", trangthaiphancong);
                var result = e.CONGVIECs.SqlQuery("select * from CONGVIEC where ID IN (select DISTINCT IDCONGVIEC from PHANCONG where IDTRANGTHAI = @trangthaiphancong and IDNGUOINHAN = @idNguoiDung)", param, param1).ToList();
                return result;
            }
        }

        public List<int> GetNGuoiDungTrongCongViec(int idCongViec)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var param = new SqlParameter("@idCongViec", idCongViec);
                var result = e.Database.SqlQuery<int>("select ID from NGUOIDUNG where ID in (select pc.IDNGUOINHAN from CONGVIEC cv, PHANCONG pc where cv.ID = @idCongViec and cv.ID = pc.IDCONGVIEC group by pc.IDNGUOINHAN)",param).ToList();
                return result;
            }
        }

        public void UpdateTrangThaiPhanCong(int idCongViec, int idPhanCong)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.PHANCONGs.Where(a => a.IDCONGVIEC == idCongViec && a.IDPHANCONG == idPhanCong).ToList();
                result.ForEach(a => {
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
                e.SaveChanges();
            }
        }

        public TRANGTHAI GetTrangThai(int id)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.TRANGTHAIs.Find(id);
                return result;
            }
        }

        public void UpdateTrangThaiCongViec(int idCongViec)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                CONGVIEC cv = e.CONGVIECs.Find(idCongViec);
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
                e.SaveChanges();
            }
        }

        public List<PHANCONG> GetPhanCongTheoCongViec(int idCongViec)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.PHANCONGs.Where(a => a.IDCONGVIEC == idCongViec).ToList();
                return result;
            }
        }

        public List<CONGVIEC> GetCongViecs()
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.CONGVIECs.ToList();
                return result;
            }
        }

        public List<CONGVIEC> GetCongViecTrongNgay(DateTime datetime)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.PHANCONGs.Where(a => a.NGAYKETTHUC == datetime).Select(a => a.CONGVIEC).ToList();
                return result;
            }
        }

    }
}