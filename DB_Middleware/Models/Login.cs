using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DB_Middleware.Models

{
    public class LIU_Login 
    {
      
        public string Email { get; set; } 

        public string OTP { get; set; }
        public string ReturnToken { get; set; }
    }
    public class Login_Detail
    {
        public string URL { get; set; }
        public string Flag { get; set; }
        public string Usertpe { get; set; }
        public string username { get; set; }
        public string passord { get; set; }


        public string Emailid { get; set; }
        public string Salesoffice { get; set; }
        public string Fullname { get; set; }

        public string Message { get; set; }
        public string userid { get; set; }

        public string Activestatus { get; set; }

        public string Rolename { get; set; }
    }

}