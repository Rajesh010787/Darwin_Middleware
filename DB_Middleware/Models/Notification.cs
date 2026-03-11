using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Middleware.Models
{
    public class Notification
    {

    }
    public class Notification_Main
    {


        public string CountNotification { get; set; }
        public List<ListNotification> Notodetails { get; set; }


    }
    public class ListNotification
    {
        public string ID { get; set; }
        public string CustomerName { get; set; }
    }
    public class Dashboarddata
    {
        public string AllEmployee { get; set; }
        public string TotalActive { get; set; }
        public string TotalPrejoining { get; set; }
        public string TotalResigned { get; set; }

        public string PendingEmailCreation { get; set; }
        public string Pendingtransfer { get; set; } 
        public string PendingEmailBlock { get; set; }
        public string PendingEmailDelete { get; set; }
        public string TotalAction { get; set; }

        public string TotalRequest { get; set; }
        public string Months { get; set; }


        public string EmailCreationpageurl { get; set; }
        public string EmailBlockpageurl { get; set; }

        public string EmailRemovepageurl { get; set; }
        public string Intertranferpegurl { get; set; }


    }
    }

