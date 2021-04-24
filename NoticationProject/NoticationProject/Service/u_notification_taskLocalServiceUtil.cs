using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticationProject.Service
{
    class u_notification_taskLocalServiceUtil<u_notification_task>
    {
        private Provider Provider = new Provider();
        public List<u_notification_task> GetAllData(string vidagis_organizationid)
        {
            string sql = @"SELECT * FROM vidagis_u_notification_task;";
            var dt = Provider.excuteddb(sql);
            return Provider.ConvertDatatableToList<u_notification_task>(dt);
        }
    } 
}
