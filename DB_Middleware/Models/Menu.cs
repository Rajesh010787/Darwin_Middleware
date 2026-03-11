using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Middleware.Models
{
    public class Menu
    {
        public class MenuListdefaultPage
        {

            public int ID { get; set; }

            public string ModuleName { get; set; }

            public string LCModuleName { get; set; }
            public string PageUrl { get; set; }


            public string Icon { get; set; }


            public List<MenulistBulkDetail> MenuList { get; set; }

        }
        public class Menulistdefaultpage
        {

            public int ID { get; set; }

            public string ModuleName { get; set; }

            public string PageUrl { get; set; }


            public string Icon { get; set; }



        }
        public class MenulistBulkDetail
        {


            public int ID { get; set; }

            public string ModuleName { get; set; }
            public string ListLOCModuleName { get; set; } 

            
            public string PageUrl { get; set; }

        }


    }
}
