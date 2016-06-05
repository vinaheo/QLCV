using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCV.DAO
{
    public class DAO_User
    {
        public List<NGUOIDUNG> GetNGUOIDUNGs()
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                List<NGUOIDUNG> nds = e.NGUOIDUNGs.ToList();
                return nds;
            }
        }

        public Boolean CheckLogin(string username, string password)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                if (e.NGUOIDUNGs.Where(a => a.TENDANGNHAP == username && a.MATKHAU == password && a.TRANGTHAI == true).ToList().Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public NGUOIDUNG GetNguoiDung(string username, string password)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.NGUOIDUNGs.Where(m => m.TENDANGNHAP == username && m.MATKHAU == password).FirstOrDefault();
                return result;
            }
        }

        public NGUOIDUNG GetNguoiDungById(int id)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.NGUOIDUNGs.Find(id);
                return result;
            }
        }

        public List<PHONGBAN> GetPhongBans()
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.PHONGBANs.ToList();
                return result;
            }
        }

        public List<GROUP> GetGroups()
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.GROUPS.ToList();
                return result;
            }
        }

        public void InsertNguoiDung(NGUOIDUNG nd)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                e.NGUOIDUNGs.Add(nd);
                e.SaveChanges();
            }
        }

        public void UpdateNguoiDung(NGUOIDUNG updateND)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                NGUOIDUNG nd = e.NGUOIDUNGs.Find(updateND.ID);
                nd.TENNGUOIDUNG = updateND.TENNGUOIDUNG;
                nd.EMAIL = updateND.EMAIL;
                nd.IDPHONGBAN = updateND.IDPHONGBAN;
                nd.IDGROUP = updateND.IDGROUP;
                e.SaveChanges();
            }
        }
    }
}