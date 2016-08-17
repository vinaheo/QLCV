using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLCV.DAO
{
    public class DAO_Group
    {
        private QLCVEntities _context;

        public DAO_Group()
        {
            _context = new QLCVEntities();
        }

        public DAO_Group(QLCVEntities ctx)
        {
            _context = ctx;
        }
        public List<GROUP> GetGroupContainUser(int idNguoiDung)
        {
            var param = new SqlParameter("@idNguoiDung", idNguoiDung);
            var result = _context.GROUPS.SqlQuery("select * from GROUPS where ID IN(select DISTINCT IDGROUP from GROUP_NGUOIDUNG where IDNGUOIDUNG = @idNguoiDung)", param).ToList();
            return result;
        }

        public List<GROUP> GetGroupContainAction(string actionName)
        {

            var param = new SqlParameter("@actionName", actionName);
            var result = _context.GROUPS.SqlQuery("select * from GROUPS where ID in (select IDGROUP from GROUP_ACTION where IDACTION in(SELECT ID FROM ACTIONS WHERE ACTIONURL lIKE @actionName))", param).ToList();
            return result;

        }

        public List<ACTION> GetActions()
        {
            var result = _context.ACTIONS.ToList();
            return result;
        }

        public void UpdateAction(ACTION ac)
        {
            var result = _context.ACTIONS.Find(ac.ID);
            result.ACTIONNAME = ac.ACTIONNAME;
            result.ACTIONURL = ac.ACTIONURL;
            _context.SaveChanges();
        }

        public void InsertAction(ACTION ac)
        {
            _context.ACTIONS.Add(ac);
            _context.SaveChanges();
        }

        public List<GROUP> GetGroups()
        {
            var result = _context.GROUPS.Include("ACTIONS").ToList();
            return result;
        }

        public GROUP GetGroup(int id)
        {
            var result = _context.GROUPS.Include("ACTIONS").Where(a => a.ID == id).FirstOrDefault();
            return result;
        }

        public void InsertGroup(GROUP gr)
        {
            _context.GROUPS.Add(gr);
            _context.SaveChanges();
        }

        public void EditGroup(int id, GROUP newGroup)
        {
            var oldGroup = _context.GROUPS.Find(id);
            oldGroup.TENGROUP = newGroup.TENGROUP;
            oldGroup.ACTIONS.Clear();
            oldGroup.ACTIONS = newGroup.ACTIONS;
            SaveChange();
        }

        public ACTION GetActionById(int id)
        {
            var result = _context.ACTIONS.Find(id);
            return result;
        }

        private int SaveChange()
        {
            // doing something
            //...
            //...
            return _context.SaveChanges();
        }
    }
}