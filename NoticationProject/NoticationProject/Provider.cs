using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoticationProject
{
    class Provider
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public NpgsqlConnection getConnection()
        {
            // PostgeSQL-style connection string
            string connstring = String.Format("Server={0};Port={1};" +
                "User Id={2};Password={3};Database={4};",
                "192.168.1.28", "5433", "postgres",
                "abc123456", "vidagiscloud_notification");
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            return conn;
        }
        public DataTable excuteddb(string sql)
        {
            var conn = this.getConnection();
            conn.Open();
            // quite complex sql statement
            //sql = "SELECT vidagis_device_token FROM vidagis_u_user where vidagis_userid = '2'";
            // data adapter making request from our connection
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            // i always reset DataSet before i do
            // something with it.... i don't know why :-)
            ds.Reset();
            // filling DataSet with result from NpgsqlDataAdapter
            da.Fill(ds);
            //// since it C# DataSet can handle multiple tables, we will select first
            dt = ds.Tables[0];
            // connect grid to DataTable

            //var deviceId = dt.Rows[0]["vidagis_device_token"].ToString();
            // since we only showing the result we don't need connection anymore
            conn.Close();
            return dt;
        }
        public List<T> ConvertDatatableToList<T>(DataTable dt)
        {
            List<T> lst = new List<T>();
            int i = 0;
            DataColumnCollection dclCollection = dt.Columns;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                DataRow drw = dt.Rows[i];
                T t = (T)Activator.CreateInstance(typeof(T));
                //System.Reflection.PropertyInfo[] proInfos = drw.GetType().GetProperties();
                System.Reflection.PropertyInfo[] proInfosT = t.GetType().GetProperties();
                try
                {
                    foreach (DataColumn dcl in dclCollection)
                    {
                        string proName = dcl.ColumnName;
                        object proVal = drw[proName];
                        if (proVal == DBNull.Value) continue;

                        foreach (PropertyInfo proInfoT in proInfosT)
                        {
                            string proNameT = proInfoT.Name;
                            if (proName == proNameT)
                            {
                                proInfoT.SetValue(t, proVal);// proInfoT.SetValue(t, Convert.ChangeType(proVal, proInfoT.PropertyType));
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                lst.Add(t);
            }
            return lst;
        }
    }
}
