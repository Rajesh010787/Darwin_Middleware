using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Middleware.Models
{
    public class Employeedetails
    {
        public string smitsubdepart { get; set; }
        public string fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string fullname { get; set; }

        public string UserUniqueID { get; set; }
        public string group_company { get; set; }
        public string department_cost_center_name { get; set; }

        public string functional_area_dept { get; set; }
        public string functional_area_subdept { get; set; }
        public string Level_Grade { get; set; }
        public string Deisgnation { get; set; }
        public string department { get; set; }
        public string date_of_joining { get; set; }
        public string job_level { get; set; }

        public string marital_status { get; set; }
        public string anniversary_date { get; set; }
        public string office_location { get; set; }


        public string office_mobile_no { get; set; }
        public string personal_mobile_no { get; set; }
        public string employee_status { get; set; }
        public string hrbp_name { get; set; }

        public string direct_manager_name { get; set; }

        public string direct_manager_email { get; set; }
        public string employee_id { get; set; }
        public string company_email_id { get; set; }

        public string api_key { get; set; }

        public string date_of_exit { get; set; }

        


               public string Visitor_Status { get; set; }
        public string CStatus { get; set; }
        public int self_service { get; set; }

        public string user_id { get; set; }
        public string HR_Position_Type { get; set; }
        public string HR_Access_Domain_Email { get; set; }
        public string HR_Proposed_Emailid { get; set; }
        public string HR_Chosse_Domain { get; set; }
        public string HR_Emp_Being_Replace_Ecode { get; set; }
        public string HR_Replace_Name { get; set; }
        public string HR_Replace_Designation { get; set; }
        public string HR_Replace_Grade { get; set; }

        public string HR_Replace_Email { get; set; }
        public string HR_Access_Required_Group { get; set; }
        public string HR_Employee_Type { get; set; }

        public string HR_Till_Date { get; set; }

        public string ConfrimJoiningdate { get; set; }
        public string Fwdr_Email_Status { get; set; }
        public string Fwdr_Email_Description { get; set; }

        public string GivenEmail_Onedrive { get; set; }
        public string GivenEmail_Onedrive_des { get; set; }

        public string Requestype { get; set; }

        public string effectivedate { get; set; }

        public string remark { get; set; }

        public string pststatus { get; set; } 
        public string pstlocation { get; set; }


        public string itemailcreated { get; set; }
        public string itemailpassword { get; set; } 
        public string Accessblockdate { get; set; }

        public string Hrremark { get; set; }

    }

    public class Root
    {
        public int status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }

        public string self_service_message { get; set; }

        public string Flag { get; set; }

    }

    public class EmployeeStatusModel
    {
        public string UserUniqueID { get; set; }
        public bool IsChecked { get; set; }

    }
    public class JobStatusModel
    {
        public string UserUniqueID { get; set; }
        public bool IsChecked { get; set; }

    }
    public class Response
    {
        public int status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }

        public string self_service_message { get; set; }

        public string Flag { get; set; }

    }
    public class EmailRequestBoady {
        public string api_key { get; set; }
        public string email_id { get; set; }
        public int self_service { get; set; }
        public string user_id { get; set; }
        public int activate { get; set; }
    }
    public class EmailUpdateModel
    {
        public string Fullname { get; set; } 
        public string password { get; set; } 
        public string UniqueID { get; set; }
        public string EmailId { get; set; }
    }
    public class Remove_EmailID
    {
        public string Fwdr_Email_Status { get; set; } 
        public string Fwdr_Email_Description { get; set; }

        public string GivenEmail_Onedrive { get; set; }
        public string GivenEmail_Onedrive_des { get; set; }
        public string Delete_Email_Date { get; set; }
        public string status { get; set; }
        public string pststatus { get; set; }
        public string pstlocation { get; set; } 
        public string Remark { get; set; }
        public string UniquieID { get; set; } 
    }


    public class Request_Email_Data
    {
                public string SMITSubdepart { get; set; }
        public string Teamstatus { get; set; }
        public string UserUniqueID { get; set; }
        public string HR_Position_Type { get; set; }
        public string accessdomainandenmailidrequest { get; set; }
        public string HR_Proposed_Emailid { get; set; }
        public string HR_Chosse_Domain { get; set; }
        public string HR_Emp_Being_Replace_Ecode { get; set; }
        public string HR_Replace_Name { get; set; }
        public string HR_Replace_Designation { get; set; }
        public string HR_Replace_Grade { get; set; }
        public string HR_Replace_Email { get; set; } 
        public string HR_Access_Required_Group { get; set; }
        public string HR_Employee_Type { get; set; }
        public string HR_JoiningConfirm_Date { get; set; }
        public string HR_Till_Date { get; set; }
        public string Remark { get; set; }
        public string ITEmailCreated { get; set; }
        public string Status { get; set; } 
        public string MS_LicenceAllocated { get; set; }
        public string NewLicence_Prucure { get; set; }

        public string Effectivedate { get; set; } 
        public string password { get; set; }
        public string Position_ID { get; set; } 
        public string Critical_Posotion { get; set; }
        public string SilikaCEmail { get; set; }

    }
    public class jobdetails
    {
        
               public string job_id { get; set; }
        public string group_company { get; set; }
        public string job_code { get; set; }
        public string business_unit { get; set; }
        public string Joblocation { get; set; }
        public string department { get; set; }
        public string job_title { get; set; }
        public string experience_from { get; set; }
        public string experience_to { get; set; }
        public string job_created_timestamp { get; set; } 
        public int Status { get; set; }
        public string Url { get; set; } 
    }
    public class Tracking_History
    {
        public string Actionid { get; set; } 
        public string fullname { get; set; }
        public string actionby { get; set; }
        public string peningon { get; set; }
        public string actiondate { get; set; }
        public string remark { get; set; }
        public string requestfor { get; set; }
        public string department { get; set; }

        public string id { get; set; } 
        public string Cstatus { get; set; } 
    }


    public class loandata {
        public string Gid { get; set; } 
        public string id { get; set; }
        public string attcahement { get; set; }
        public string CStatus { get; set; }
        public string pending { get; set; }
        public string Actiontype { get; set; }
        public string fullname { get; set; }
        public string ecode { get; set; }
        public string department { get; set; }
        public string DOJ { get; set; }
        public string salarydown { get; set; }

        public string rupess { get; set; }

        public string deviation { get; set; }
        public string purpose { get; set; }
        public string purposedesc { get; set; }
        public string installments { get; set; }

        public string Garantees1 { get; set; }
        public string Garantees2 { get; set; }

        public string Garanteesecode1 { get; set; }
        public string Garanteesecode2 { get; set; }

        public string salarybasicvdahra { get; set; }
        public string whetherconfirmedornot { get; set; }
        public string eligibilityamount { get; set; }
        public string noofinstallmentforreppayment { get; set; }


        public string deviationsanctionedamount { get; set; }
        public string deviationfinalamount { get; set; }
        public string deviationnoofinstallmentforreppayment { get; set; }


        public string finalsanctionedamount { get; set; }
        public string finalnoofinstallmentforreppayment { get; set; }

        public string receipt_rs { get; set; }
        public string cash_cheque_no { get; set; }
        public string amount_received_date { get; set; }
        public string Remark { get; set; }

        public string loanstatus { get; set; }
        public string closeddate { get; set; }
        public string sapecode { get; set; }

        public string finalamount { get; set; }



        public string bankname { get; set; }
        public string ifsccode { get; set; }
        public string accountnumber { get; set; }
        public string gcofinalamount { get; set; }
        public string gcoinstalment { get; set; }


        public string businessvertical { get; set; }
        public string division { get; set; } 
        public string plant { get; set; }

        public string pan { get; set; }
        public string costcenter { get; set; }
        public string sapcostcenter { get; set; }
        public string prfofitcenter { get; set; }
        public string newcostcenter { get; set; }


    }
}
