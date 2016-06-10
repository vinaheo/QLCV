using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLCV.DAO
{
    public class DAO_Group
    {
        public List<GROUP> GetGroupContainUser(int idNguoiDung)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var param = new SqlParameter("@idNguoiDung", idNguoiDung);
                var result = e.GROUPS.SqlQuery("select * from GROUPS where ID IN(select DISTINCT IDGROUP from GROUP_NGUOIDUNG where IDNGUOIDUNG = @idNguoiDung)", param).ToList();
                return result;
            }
        }

        public List<GROUP> GetGroupContainAction(string actionName)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var param = new SqlParameter("@actionName", actionName);
                var result = e.GROUPS.SqlQuery("select * from GROUPS where ID in (select IDGROUP from GROUP_ACTION where IDACTION in(SELECT ID FROM ACTIONS WHERE ACTION = @actionName))", param).ToList();
                return result;
            }
        }

        public List<ACTION> GetActions()
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.ACTIONS.ToList();
                return result;
            }
        }

        public void UpdateAction(ACTION ac)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.ACTIONS.Find(ac.ID);
                result.ACTIONNAME = ac.ACTIONNAME;
                result.ACTIONURL = ac.ACTIONURL;
                e.SaveChanges();
            }
        }

        public void InsertAction(ACTION ac)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                e.ACTIONS.Add(ac);
                e.SaveChanges();
            }
        }

        public List<GROUP> GetGroups()
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                var result = e.GROUPS.Include("ACTIONS").ToList();
                return result;
            }
        }

        public void InsertGroup(GROUP gr)
        {
            using (QLCVEntities e = new QLCVEntities())
            {
                e.GROUPS.Add(gr);
                e.SaveChanges();
            }
        }
    }
}