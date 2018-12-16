using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace DataAccessLayer
{
    public class SqlExecute
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public Int32 ExecuteData(string sql)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public DataSet DisplayData(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
