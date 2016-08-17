using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCV.DAO
{
    public class DAO_User
    {
        private QLCVEntities _context;
        
        public DAO_User()
        {
            _context = new QLCVEntities();
        }

        public DAO_User(QLCVEntities ctx)
        {
            _context = ctx;
        }

        public List<NGUOIDUNG> GetNGUOIDUNGs()
        {
            List<NGUOIDUNG> nds = _context.NGUOIDUNGs.ToList();
            return nds;
        }

        public Boolean CheckLogin(string username, string password)
        {
            if (_context.NGUOIDUNGs.Where(a => a.TENDANGNHAP == username && a.MATKHAU == password && a.TRANGTHAI == true).ToList().Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public NGUOIDUNG GetNguoiDung(string username, string password)
        {
            var result = _context.NGUOIDUNGs.Where(m => m.TENDANGNHAP == username && m.MATKHAU == password).FirstOrDefault();
            return result;
        }

        public NGUOIDUNG GetNguoiDungById(int id)
        {
            var result = _context.NGUOIDUNGs.Find(id);
            return result;
        }

        public List<PHONGBAN> GetPhongBans()
        {
            var result = _context.PHONGBANs.ToList();
            return result;
        }

        public List<GROUP> GetGroups()
        {
            var result = _context.GROUPS.ToList();
            return result;
        }

        public void InsertNguoiDung(NGUOIDUNG nd)
        {
            _context.NGUOIDUNGs.Add(nd);
            _context.SaveChanges();
        }

        public void UpdateNguoiDung(NGUOIDUNG updateND, string password = null)
        {
            NGUOIDUNG nd = _context.NGUOIDUNGs.Find(updateND.ID);
            nd.TENNGUOIDUNG = updateND.TENNGUOIDUNG;
            nd.EMAIL = updateND.EMAIL;
            nd.IDPHONGBAN = updateND.IDPHONGBAN;
            nd.TRANGTHAI = nd.TRANGTHAI;
            nd.IDGROUP = updateND.IDGROUP;
            if (password != null)
            {
                nd.MATKHAU = password;
            }
            else
            {
                nd.MATKHAU = nd.MATKHAU;
            }

            _context.SaveChanges();

        }
    }
}