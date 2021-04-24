using NoticationProject.Model;
using NoticationProject.Service;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoticationProject
{
    public partial class Form2 : BaseForm
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private Provider provider = new Provider();
        private u_notification_taskLocalServiceUtil<u_notification_task> db_task = new u_notification_taskLocalServiceUtil<u_notification_task>();
        private vidagis_u_notification_device_tokenLocalServiceUtil<vidagis_u_notification_device_token> db_device = new vidagis_u_notification_device_tokenLocalServiceUtil<vidagis_u_notification_device_token>();
        Queue<u_notification_task> Qtasks = new Queue<u_notification_task>();
        FireBase FireBase = new FireBase();
        public Form2()
        {
            InitializeComponent();
            timer1.Interval = 3000;
            timer1.Start();
            timer2.Interval = 1000;
            timer2.Start();
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            label1.Text = i.ToString();
            var lst = db_task.GetAllData("");
            
            foreach (var task in lst)
            {
                if(!Qtasks.Contains(task)) Qtasks.Enqueue(task);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (var task in Qtasks)
            {
                var Year = DateTime.Now.Year;
                var Month = DateTime.Now.Month;
                var Day = DateTime.Now.Day;
                var Hour = DateTime.Now.Hour;
                var Minute = DateTime.Now.Minute;
                var now = new DateTime(Year, Month, Day, Hour, Minute,0);
                if (task.vidagis_schedule_time_trigger == now)
                {
                    var lst = db_device.GetDataByTask(task.vidagis_taskid);
                    var devices = string.Join(",", lst.Select(x => x.vidagis_device_token));
                    FireBase.sendnotication(devices);
                    Qtasks.Dequeue();
                }
            }
        }
    }
}
