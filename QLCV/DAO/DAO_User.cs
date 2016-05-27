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
    }
}