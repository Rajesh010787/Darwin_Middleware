using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Middleware.Models
{
    public class Master_Data
    {
        public string domainname { get; set; }

        public string groupemailids { get; set; }

        public string mslicense { get; set; }

        public string smitdepart { get; set; }

        public string gpersonname { get; set; }

        public string gperson_Ecode { get; set; }



        public string purpose { get; set; }
    }
    public class Dropdown_Model<T>
    {


        public T value { get; set; }
        public string name { get; set; }

    }
    
}
