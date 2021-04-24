using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace NoticationProject
{
    public partial class Form1 : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void user_name_Click(object sender, EventArgs e)
        {

        }

        private void list_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = list_city.GetItemText(list_city.SelectedItem);
            MessageBox.Show(text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    "192.168.1.28", "5433", "postgres",
                    "abc123456", "vidagiscloud_tenant_SDIHue");
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "SELECT vidagis_device_token FROM vidagis_u_user where vidagis_userid = '2'";
                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // connect grid to DataTable
                dataGridView1.DataSource = dt;
                var deviceId = dt.Rows[0]["vidagis_device_token"].ToString();
                // since we only showing the result we don't need connection anymore
                conn.Close();
                new FireBase().sendnotication(deviceId);
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show(msg.ToString());
                throw;
            }
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            label2.Text = i.ToString();
        }
    }
}
