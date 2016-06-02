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
    }
}