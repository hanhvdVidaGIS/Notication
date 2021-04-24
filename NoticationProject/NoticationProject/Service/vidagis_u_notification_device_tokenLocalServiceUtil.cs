using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticationProject.Service
{
    class vidagis_u_notification_device_tokenLocalServiceUtil<vidagis_u_notification_device_token>
    {
        private Provider Provider = new Provider();
        public List<vidagis_u_notification_device_token> GetDataByTask(string vidagis_taskid)
        {
            string sql = @"SELECT * FROM vidagis_u_notification_device_token where vidagis_taskid = '" + vidagis_taskid + "'";
            var dt = Provider.excuteddb(sql);
            return Provider.ConvertDatatableToList<vidagis_u_notification_device_token>(dt);
        }
    }
}
