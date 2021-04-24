using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticationProject.Model
{
    public class u_notification_task
    {
        public string vidagis_taskid { get; set; }

        public string vidagis_message { get; set; }

        public string vidagis_link { get; set; }

        public string vidagis_type { get; set; }

        public DateTime? vidagis_schedule_time_trigger { get; set; }

        public string vidagis_notificationid { get; set; }

        public string vidagis_organizationid { get; set; }

        public DateTime? createdate { get; set; }
    }
}
