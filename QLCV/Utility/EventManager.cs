using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLCV.Utility
{
    public class EventManager
    {
        public DataTable FilteredData(DateTime start, DateTime end)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [event] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
            da.SelectCommand.Parameters.AddWithValue("start", start);
            da.SelectCommand.Parameters.AddWithValue("end", end);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}