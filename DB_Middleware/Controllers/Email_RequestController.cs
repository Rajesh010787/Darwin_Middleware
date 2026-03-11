using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DB_Middleware.DAO;
using DB_Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DB_Middleware.Controllers
{

    public class Email_RequestController : Controller
    {
        DB_DataAccess_Layer Employedetails_EmailCreation;
        private static byte[] Key = new byte[32]; // AES-256
        private static byte[] IV = new byte[16]; // AES block size is 16 bytes

        // GET: Email_RequestController
        public Email_RequestController(IConfiguration configuration)
        {
            Employedetails_EmailCreation = new DB_DataAccess_Layer(configuration);
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Email_RequestController/Details/5
        public ActionResult Bind_Data_For_EmailCreation(int id)
        {
            return View();
        }
         public ActionResult HR_For_All_Unit_Access()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        
        public ActionResult HR_Validate_New_Request()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Infra_Team_Validate()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Bind_dataoninfra_EmailCreation()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }

        // GET: Email_RequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Email_RequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Email_RequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Email_RequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Email_RequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Email_RequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        public ActionResult HR_Validate(string UniqueID)
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            string Uniqueid = Decode(UniqueID);

            List<Employeedetails> Empdetails = new List<Employeedetails>();

            string query = @"Bind_Pending_Employee_Details";
            DataTable table = new DataTable();

            var con = Employedetails_EmailCreation.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Uniqueid", Uniqueid);
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();


                    litem.fname = table.Rows[i]["first_name"].ToString();
                    litem.Mname = table.Rows[i]["middle_name"].ToString();
                    litem.Lname = table.Rows[i]["last_name"].ToString();
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.department_cost_center_name = table.Rows[i]["department_cost_center_name"].ToString();
                    litem.functional_area_dept = table.Rows[i]["functional_area_dept"].ToString();
                    litem.functional_area_subdept = table.Rows[i]["functional_area_subdept"].ToString();
                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.department = table.Rows[i]["department_name"].ToString();
                    litem.marital_status = table.Rows[i]["marital_status"].ToString();
                    litem.anniversary_date = table.Rows[i]["anniversary_date"].ToString();
                    litem.office_location = table.Rows[i]["office_location"].ToString();
                    litem.Deisgnation = table.Rows[i]["job_level"].ToString();
                    litem.employee_status = table.Rows[i]["employee_status"].ToString();
                    litem.hrbp_name = table.Rows[i]["hrbp_name"].ToString();
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();
                    litem.direct_manager_email = table.Rows[i]["Reporting_Manager_Email"].ToString();
                    litem.personal_mobile_no = table.Rows[i]["Personal_mobile_no"].ToString();
                    litem.date_of_joining = table.Rows[i]["date_of_joining"].ToString();
                    litem.HR_Position_Type = table.Rows[i]["HR_Position_Type"].ToString();
                    litem.HR_Access_Domain_Email = table.Rows[i]["HR_Access_Domain_Email"].ToString();
                    litem.HR_Proposed_Emailid = table.Rows[i]["HR_Proposed_Emailid"].ToString();
                    litem.HR_Chosse_Domain = table.Rows[i]["HR_Chosse_Domain"].ToString();
                    litem.HR_Emp_Being_Replace_Ecode = table.Rows[i]["HR_Emp_Being_Replace_Ecode"].ToString();
                    litem.HR_Replace_Name = table.Rows[i]["HR_Replace_Name"].ToString();
                    litem.HR_Replace_Designation = table.Rows[i]["HR_Replace_Designation"].ToString();
                    litem.HR_Replace_Grade = table.Rows[i]["HR_Replace_Grade"].ToString();
                    litem.HR_Replace_Email = table.Rows[i]["HR_Replace_Email"].ToString();
                    litem.ConfrimJoiningdate = table.Rows[i]["Confrim_Joining_Date"].ToString();
                    litem.smitsubdepart = table.Rows[i]["SMIT_Sub_Depart"].ToString();
                    litem.Hrremark= table.Rows[i]["Hrremark"].ToString();
                    litem.UserUniqueID = Uniqueid;
                    litem.HR_Access_Required_Group = table.Rows[i]["HR_Access_Required_Group"].ToString().Trim(); 
                    litem.HR_Employee_Type = table.Rows[i]["HR_Employee_Type"].ToString().Trim();
                    litem.HR_Till_Date = table.Rows[i]["HR_Till_Date"].ToString();
                    Empdetails.Add(litem);
                }





            }



            return Json(Empdetails);

        }
        //---------------------------------Rep Emp Details ----------------------------------
        public ActionResult Bind_Repempdetails(string empid)
        {



            List<Employeedetails> Empdetails = new List<Employeedetails>();

            string query = @"Bind_Repemp_Details";
            DataTable table = new DataTable();

            var con = Employedetails_EmailCreation.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employeeid ", empid);
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();


                    litem.fname = table.Rows[i]["Fullname"].ToString();
                    litem.company_email_id = table.Rows[i]["company_email_id"].ToString();

                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.job_level = table.Rows[i]["Designation"].ToString();

                    Empdetails.Add(litem);
                }

            }



            return Json(Empdetails);

        }


        //-------------------------------Bind Email Block Requests----------------------------------------
        public ActionResult get_email_block_email_details()
        {


            List<Employeedetails> PendingEmpdetails = new List<Employeedetails>();

            string query = @"Bind_Block_Email_Request";
            DataTable table = new DataTable();

            var con = Employedetails_EmailCreation.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string a = HttpContext.Session.GetString("Empcode");
                cmd.Parameters.AddWithValue("@EmpCode", HttpContext.Session.GetString("Empcode"));

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();

                    litem.fname = table.Rows[i]["first_name"].ToString();
                    litem.Mname = table.Rows[i]["middle_name"].ToString();
                    litem.Lname = table.Rows[i]["last_name"].ToString();
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.functional_area_dept = table.Rows[i]["functional_area_dept"].ToString();
                    litem.functional_area_subdept = table.Rows[i]["functional_area_subdept"].ToString();
                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.UserUniqueID = Encode(table.Rows[i]["UserUniqueID"].ToString());
                    litem.marital_status = table.Rows[i]["marital_status"].ToString();
                    litem.anniversary_date = table.Rows[i]["anniversary_date"].ToString();
                    litem.office_location = table.Rows[i]["office_location"].ToString();
                    litem.date_of_joining = table.Rows[i]["date_of_joining"].ToString();

                    litem.employee_status = table.Rows[i]["employee_status"].ToString();
                    litem.hrbp_name = table.Rows[i]["hrbp_name"].ToString();
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();
                    litem.CStatus = table.Rows[i]["CStatus"].ToString();

                    PendingEmpdetails.Add(litem);
                }

            }
            else { Employeedetails litem = new Employeedetails(); PendingEmpdetails.Add(litem); }



            return Json(PendingEmpdetails);



        }


        //-----------------------------------End Email Block Email Details -----------------------------------function----


        //-------------------------------End Rep Emp Details----------------------------------------
        public ActionResult get_pending_employee_emailid_creation()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            List<Employeedetails> PendingEmpdetails = new List<Employeedetails>();

            string query = @"Bind_Employee_EmailID_Creation";
            DataTable table = new DataTable();

            var con = Employedetails_EmailCreation.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string a = HttpContext.Session.GetString("Sessionusername");
                cmd.Parameters.AddWithValue("@USERNAME", HttpContext.Session.GetString("Sessionusername"));

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();

                    litem.fname = table.Rows[i]["first_name"].ToString();
                    litem.Mname = table.Rows[i]["middle_name"].ToString();
                    litem.Lname = table.Rows[i]["last_name"].ToString();
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.department_cost_center_name= table.Rows[i]["department_cost_center_name"].ToString(); 
                    litem.functional_area_dept = table.Rows[i]["functional_area_dept"].ToString();
                    litem.functional_area_subdept = table.Rows[i]["functional_area_subdept"].ToString();
                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.UserUniqueID = Encode(table.Rows[i]["UserUniqueID"].ToString());
                    litem.marital_status = table.Rows[i]["marital_status"].ToString();
                    litem.anniversary_date = table.Rows[i]["anniversary_date"].ToString();
                    litem.office_location = table.Rows[i]["office_location"].ToString();
                    litem.date_of_joining = table.Rows[i]["date_of_joining"].ToString();
                    litem.user_id = (table.Rows[i]["UserUniqueID"].ToString());
                    litem.employee_status = table.Rows[i]["employee_status"].ToString();
                    litem.hrbp_name = table.Rows[i]["hrbp_name"].ToString();
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();
                    litem.CStatus = table.Rows[i]["CStatus"].ToString();

                    PendingEmpdetails.Add(litem);
                }

            }
            else { Employeedetails litem = new Employeedetails(); PendingEmpdetails.Add(litem); }



            return Json(PendingEmpdetails);



        }


        public ActionResult get_pending_allunitstatusemployee_emailid_creation()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                return RedirectToAction("User_Login", "Inter");
            }

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(Employedetails_EmailCreation.getConnectionString()))
            using (SqlCommand cmd = new SqlCommand("Bind_Employee_EmailID_Creation_For_all_unit", con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERNAME", sessionUser);
                da.Fill(dt);
            }

            // Convert DataTable to JSON-friendly format
            var data = dt.AsEnumerable().Select(row =>
                dt.Columns.Cast<DataColumn>()
                  .ToDictionary(col => col.ColumnName, col => row[col])
            ).ToList();

            return Json(data);
        }



        //-----------------------------------Generate Email Id request -----------------------------------function----
        [HttpPost]
        public ActionResult Request_Email_Creation([FromBody] Request_Email_Data objdata)
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            Response returnresponse = new Response();
            try
            {




                string query = @"HR_Request_For_Email_Creation";
                DataTable table = new DataTable();

                var con = Employedetails_EmailCreation.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserUniqueID", Decode(objdata.UserUniqueID));
                    cmd.Parameters.AddWithValue("@HR_Position_Type", objdata.HR_Position_Type);
                    cmd.Parameters.AddWithValue("@HR_AccessDoamin_Emailidrequest", objdata.accessdomainandenmailidrequest);
                    cmd.Parameters.AddWithValue("@HR_Proposed_Emailid", objdata.HR_Proposed_Emailid);
                    cmd.Parameters.AddWithValue("@HR_Chosse_Domain", objdata.HR_Chosse_Domain);
                    cmd.Parameters.AddWithValue("@HR_Emp_Being_Replace_Ecode", objdata.HR_Emp_Being_Replace_Ecode);
                    cmd.Parameters.AddWithValue("@HR_Replace_Name", objdata.HR_Replace_Name);
                    cmd.Parameters.AddWithValue("@HR_Replace_Designation", objdata.HR_Replace_Designation);
                    cmd.Parameters.AddWithValue("@HR_Replace_Grade", objdata.HR_Replace_Grade);
                    cmd.Parameters.AddWithValue("@HR_Replace_Email", objdata.HR_Replace_Email);
                    cmd.Parameters.AddWithValue("@HR_Access_Required_Group", objdata.HR_Access_Required_Group);
                    cmd.Parameters.AddWithValue("@HR_Employee_Type", objdata.HR_Employee_Type);
                    cmd.Parameters.AddWithValue("@HR_Till_Date", objdata.HR_Till_Date);
                    cmd.Parameters.AddWithValue("@HR_JoiningConfirm_Date", objdata.HR_JoiningConfirm_Date);
                    cmd.Parameters.AddWithValue("@SMIT_Depart", objdata.SMITSubdepart);
                    cmd.Parameters.AddWithValue("@Position_ID", objdata.Position_ID);
                    cmd.Parameters.AddWithValue("@Critical_Posotion", objdata.Critical_Posotion);
                    cmd.Parameters.AddWithValue("@SilikaCEmail", objdata.SilikaCEmail);
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionby", HttpContext.Session.GetString("Sessionusername"));
                    cmd.Parameters.AddWithValue("@Actionemocode", HttpContext.Session.GetString("Empcode"));
                    da.Fill(table);


                    if (table.Rows.Count > 0)
                    {


                        returnresponse.Flag = table.Rows[0]["Flag"].ToString();






                        string mailcc = "";

                        if (returnresponse.Flag == "SUCCESS")
                        {

                            StringBuilder MailBody = new StringBuilder();
                            StringBuilder sb = new StringBuilder();

                            MailBody.Append("<br><html><table  border='1'  background-color: #dddddd; color: white; text-align: center;padding: 8px; style='border: 1px solid black; border - collapse: collapse; width: 500px;'>");
                            MailBody.Append("<tr style='border: 1px solid black; border - collapse: collapse;'><td style='border: 1px solid black; border - collapse: collapse; background - color: #dddddd;' colspan='2'><strong>Employee Request Detail</strong></td></tr>");

                         
                            MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>First Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["first_name"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Middle Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["middle_name"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Last  Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["last_name"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Proposed Email ID </td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["HR_Proposed_Emailid"].ToString() + "</td></tr>"); 
                            MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Designation </td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Designation"].ToString() + "</td></tr>"); 
                            MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Dept. Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_name"].ToString() + "</td></tr>"); 
                            MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Contact No.</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["ContactNo"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["group_company"].ToString() + "</td></tr>");
                            MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Cost Center Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_cost_center_name"].ToString() + "</td></tr>");
                            MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Group Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Groupname"].ToString() + "</td></tr>");
                            MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Address </td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["office_location"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background - color: #dddddd;'><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>RM Email ID </td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background - color: #dddddd;'><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HRBP</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background - color: #dddddd;'><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HR Access Domain Email</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["HR_Access_Domain_Email"].ToString() + "</td></tr>");

                            MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Remark</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.Remark + "</td></tr>");

                            MailBody.Append("</table></html><br/>");
                            sb.Append("Dear "+ table.Rows[0]["FullnameSentto"].ToString() + ",");
                            sb.Append("<br>");
                            sb.Append("<br>");
                            String Headermessage = "";


                            if (table.Rows[0]["Peningaction"].ToString() == "N") { 
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has approved a new employee email id creation  with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Approved New Employee Email Creation- " + table.Rows[0]["Fullname"].ToString() + "";
                            }
                            if (table.Rows[0]["Peningaction"].ToString() == "Y")
                            {
                                sb.Append("You has Extended Date of joining  with details as follows:");
                                sb.Append("</br>");
                                sb.Append("</br>");
                                sb.Append("</br>");
                                sb.Append("</br>");
                                Headermessage = "Date of Joining Extended- " + table.Rows[0]["Fullname"].ToString() + "";
                            }
                            sb.Append(MailBody.ToString());
                            sb.Append("<br>"); sb.Append("<br>");
                            sb.Append("Click on <a href='https://darwinmware.sparkminda.in/'>Link</a> to reach email id request & take necessary action.");
                            sb.Append("<br>");
                            sb.Append("<br>");
                            sb.Append("Darwin Box Portal Team");

                            sb.Append("<br>");
                            sb.Append("Regards"); sb.Append("<br>");
                            sb.Append("<br>");

                            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                            string userName = "noreply@mindacorporation.com";
                            string password = "Sos67130";
                            string frommail = "noreply@mindacorporation.com";
                            MailAddress sentfrom = new MailAddress(frommail);
                            msg.From = sentfrom;
                            msg.To.Add(table.Rows[0]["MailSentto"].ToString());


                            //msg.To.Add("rajesh.kumar1@mindacorporation.com");
                            msg.CC.Add(table.Rows[0]["manager_email_cc"].ToString());
                            msg.CC.Add(HttpContext.Session.GetString("SessionusernameEmail"));
                            msg.Bcc.Add("rajesh.kumar1@mindacorporation.com");
                            msg.Subject = Headermessage.ToString();

                            msg.Body = sb.ToString();
                            msg.IsBodyHtml = true;
                            SmtpClient SmtpClient = new SmtpClient();
                            SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                            SmtpClient.Host = "smtp.office365.com";
                            SmtpClient.Port = 587;
                            SmtpClient.EnableSsl = true;
                            SmtpClient.Send(msg);

                            returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                            returnresponse.message = table.Rows[0]["MESSAGE"].ToString();




                            //_response.header.error_message = "TSMS_Approvals Data!";






                        }
                        else
                        {
                            returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                            returnresponse.message = table.Rows[0]["MESSAGE"].ToString();



                        }


                    }
                }
                return Json(returnresponse);
            }
            catch (SystemException ex)
            {
                return Json(returnresponse);
            }

        }



        //----------------------------------End Generate Email ID Request ---------------------------------------------


        //-----------------------------------Extend_DOJ request -----------------------------------function----
        [HttpPost]
        public ActionResult Request_Email_Creation_Extend_DOJ([FromBody] Request_Email_Data objdata)
        {
            Response returnresponse = new Response();
            try
            {




                string query = @"HR_Request_For_Email_Creation_Extend_DOJ";
                DataTable table = new DataTable();

                var con = Employedetails_EmailCreation.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserUniqueID", objdata.UserUniqueID);
                
                    cmd.Parameters.AddWithValue("@HR_JoiningConfirm_Date", objdata.HR_JoiningConfirm_Date);

                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionby", HttpContext.Session.GetString("Sessionusername"));
                    cmd.Parameters.AddWithValue("@Actionemocode", HttpContext.Session.GetString("Empcode"));
                    da.Fill(table);


                    if (table.Rows.Count > 0)
                    {


                        returnresponse.Flag = table.Rows[0]["Flag"].ToString();

                        if (returnresponse.Flag == "SUCCESS")
                        {

                            StringBuilder MailBody = new StringBuilder();
                            StringBuilder sb = new StringBuilder();

                            MailBody.Append("<br><html><table  border='1'  background-color: #dddddd; color: white; text-align: center;padding: 8px; style='border: 1px solid black; border - collapse: collapse; width: 500px;'>");
                            MailBody.Append("<tr style='border: 1px solid black; border - collapse: collapse;'><td style='border: 1px solid black; border - collapse: collapse; background - color: #dddddd;' colspan='2'><strong>Employee Request Detail</strong></td></tr>");
                            MailBody.Append("<tr style='background-color: #dddddd;'><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Full Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Fullname"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background-color: #dddddd;'><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>DB DOJ</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["date_of_joining"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background-color: #dddddd;'><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HR Confirm DOJ</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Confrim_Joining_Date"].ToString() + "</td></tr>");
                            MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["group_company"].ToString() + "</td></tr>");
                            MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Cost Center Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_cost_center_name"].ToString() + "</td></tr>");


                            MailBody.Append("<tr><td  background-color:#dddddd; color:white; text-align:center;padding: 8px;>Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["office_location"].ToString() + "</td></tr>");

                            MailBody.Append("<tr style='background - color: #dddddd;'><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>RM Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + "</td></tr>");
                            MailBody.Append("<tr style='background - color: #dddddd;'><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HR BP</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");
                            MailBody.Append("<tr><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Remark</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.Remark + "</td></tr>");

                            MailBody.Append("</table></html><br/>");
                            sb.Append("Dear " + table.Rows[0]["FullnameSentto"].ToString() + ",");
                            sb.Append("<br>");
                            sb.Append("<br>");
                            String Headermessage = "";


                           
                                sb.Append("You has Extended Date of joining  with details as follows:");
                                sb.Append("</br>");
                                sb.Append("</br>");
                                sb.Append("</br>");
                                sb.Append("</br>");
                                Headermessage = "Date of Joining Extended- " + table.Rows[0]["Fullname"].ToString() + "";
                            
                            sb.Append(MailBody.ToString());
                            sb.Append("<br>"); sb.Append("<br>");
                            sb.Append("Click on <a href='https://darwinmware.sparkminda.in/'>Link</a> to reach email id request & take necessary action.");
                            sb.Append("<br>");
                            sb.Append("<br>");
                            sb.Append("Darwin Box Portal Team");

                            sb.Append("<br>");
                            sb.Append("Regards"); sb.Append("<br>");
                            sb.Append("<br>");

                            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                            string userName = "noreply@mindacorporation.com";
                            string password = "Sos67130";
                            string frommail = "noreply@mindacorporation.com";
                            MailAddress sentfrom = new MailAddress(frommail);
                            msg.From = sentfrom;
                            msg.To.Add("rajesh.kumar1@mindacorporation.com");
                            msg.To.Add("pooja.thakur @mindacorporation.com");

                            //msg.To.Add("rajesh.kumar1@mindacorporation.com");
                            //msg.CC.Add("rajesh.kumar1@mindacorporation.com");
                            msg.Bcc.Add("rajesh.kumar1@mindacorporation.com");
                            msg.Subject = Headermessage.ToString();

                            msg.Body = sb.ToString();
                            msg.IsBodyHtml = true;
                            SmtpClient SmtpClient = new SmtpClient();
                            SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                            SmtpClient.Host = "smtp.office365.com";
                            SmtpClient.Port = 587;
                            SmtpClient.EnableSsl = true;
                            SmtpClient.Send(msg);

                            returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                            returnresponse.message = table.Rows[0]["MESSAGE"].ToString();




                            //_response.header.error_message = "TSMS_Approvals Data!";






                        }
                        else
                        {
                            returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                            returnresponse.message = table.Rows[0]["MESSAGE"].ToString();



                        }


                    }
                }
                return Json(returnresponse);
            }
            catch (SystemException ex)
            {
                return Json(returnresponse);
            }

        }



        //----------------------------------End Generate Email ID Request ---------------------------------------------

        //-----------------------------------GenerateEMP Not Joined -----------------------------------function----
        [HttpPost]
        public ActionResult Request_Email_Not_Joined([FromBody] Request_Email_Data objdata)
        {
            Response returnresponse = new Response();
            try
            {




                string query = @"HR_Request_For_EMP_NOTJOINED";
                DataTable table = new DataTable();

                var con = Employedetails_EmailCreation.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserUniqueID", Decode(objdata.UserUniqueID));


                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionby", HttpContext.Session.GetString("Sessionusername"));
                    cmd.Parameters.AddWithValue("@Actionemocode", HttpContext.Session.GetString("Empcode"));
                    da.Fill(table);
                }

                if (table.Rows.Count > 0)
                {


                    returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                    returnresponse.message = table.Rows[0]["MESSAGE"].ToString();


                }



                return Json(returnresponse);

            }
            catch (SystemException ex)
            {
                return Json(returnresponse);
            }

        }



        //----------------------------------End EMP Not Joined ---------------------------------------------

        //-----------------------------------Email ID Created & Validated BY Infra Team -----------------------------------function----
        [HttpPost]
        public ActionResult Request_Validate_By_Infrateam([FromBody] Request_Email_Data objdata)
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            Response returnresponse = new Response();
            try
            {




                string query = @"Approved_InfraTeam";
                DataTable table = new DataTable();

                var con = Employedetails_EmailCreation.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uniqueid", Decode(objdata.UserUniqueID));
                    cmd.Parameters.AddWithValue("@ITEmailCreated", objdata.ITEmailCreated);
                    cmd.Parameters.AddWithValue("@Password", objdata.password);
                    cmd.Parameters.AddWithValue("@MS_LicenceAllocated", objdata.MS_LicenceAllocated);
                    cmd.Parameters.AddWithValue("@NewLicence_Prucure", objdata.NewLicence_Prucure);
                    cmd.Parameters.AddWithValue("@Status", objdata.Status);
                    cmd.Parameters.AddWithValue("@Teamstatus", objdata.Teamstatus);


                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionby", HttpContext.Session.GetString("Empcode"));
                    da.Fill(table);
                }

               



                if (table.Rows.Count > 0)
                {


                    returnresponse.Flag = table.Rows[0]["Flag"].ToString();

                    returnresponse.message = table.Rows[0]["MESSAGE"].ToString();




                    string mailcc = "";

                    if (returnresponse.Flag == "SUCCESS")
                    {

                        StringBuilder MailBody = new StringBuilder();
                        StringBuilder sb = new StringBuilder();

                        MailBody.Append("<br><html><table  border='1'  background-color: #dddddd; color: white; text-align: center;padding: 8px; style='border: 1px solid black; border - collapse: collapse; width: 500px;'>");
                        MailBody.Append("<tr style='border: 1px solid black; border - collapse: collapse;'><td style='border: 1px solid black; border - collapse: collapse; background - color: #dddddd;' colspan='2'><strong>Employee Request Detail</strong></td></tr>");


                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Full Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Fullname"].ToString() + "</td></tr>");
                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>DB DOJ</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["date_of_joining"].ToString() + "</td></tr>");
                        //MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HR Confirm DOJ</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Confrim_Joining_Date"].ToString() + "</td></tr>");
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["group_company"].ToString() + "</td></tr>");
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Cost Center Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_cost_center_name"].ToString() + "</td></tr>");
                        //MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Proposed Email</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["HR_Proposed_Emailid"].ToString() + "</td></tr>");
                        if (objdata.Status == "Approved")
                        {
                            //MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Email ID Created</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.ITEmailCreated + "</td></tr>");
                            MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>MS License Allocated</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.MS_LicenceAllocated + "</td></tr>");
                            MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>New License Procurement Request Generated</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.NewLicence_Prucure + "</td></tr>");
                        }
                      
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["office_location"].ToString() + "</td></tr>");
                   


                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HRBP</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Remark</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.Remark + "</td></tr>");
                        MailBody.Append("</table></html><br/>");
                        sb.Append("Dear " + table.Rows[0]["Fullnamesentto"].ToString() + ",");
                        sb.Append("<br>");
                        sb.Append("<br>");
                        String Headermessage = "";


                        if (objdata.Status == "Approved")
                        {
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has approved a new employee email id creation  with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Employee Email ID Created- " + table.Rows[0]["Fullname"].ToString() + "";
                        }
                        if (objdata.Status == "Reconsidered")
                        {
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has asked you to reconsider your new Email ID Creation request with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");

                            Headermessage = "Reconsidered New Email ID Creation Request- " + table.Rows[0]["Fullname"].ToString() + "";
                        }
                        if (objdata.Status == "Put On Hold")
                        {
                            sb.Append("I have Put On  Hold for Email Creation with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Put On Hold New Email ID Creation Request- " + table.Rows[0]["Fullname"].ToString() + "";
                        }
                        sb.Append(MailBody.ToString());
                        sb.Append("<br>"); sb.Append("<br>");
                        sb.Append("Click on <a href='https://darwinmware.sparkminda.in/'>Link</a> to reach email id request & take necessary action.");
                        sb.Append("<br>");
                        sb.Append("<br>");
                        sb.Append("Darwin Box Portal Team");

                        sb.Append("<br>");
                        sb.Append("Regards"); sb.Append("<br>");
                        sb.Append("<br>");

                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        string userName = "noreply@mindacorporation.com";
                        string password = "Sos67130";
                        string frommail = "noreply@mindacorporation.com";
                        MailAddress sentfrom = new MailAddress(frommail);
                        msg.From = sentfrom;
                        msg.CC.Add(table.Rows[0]["Mailsentto"].ToString());
                        //msg.To.Add("pooja.thakur @mindacorporation.com");
                        //msg.To.Add("rajesh.kumar1@mindacorporation.com");
                        //msg.CC.Add("rajesh.kumar1@mindacorporation.com");
                        if (objdata.Status == "Approved")
                        {
                           // msg.To.Add(table.Rows[0]["APP_IT_Help_Desk"].ToString());

                            if (!string.IsNullOrEmpty(Convert.ToString(table.Rows[0]["Localit"])))
                            {
                                // Add the value to msg.CC
                                msg.CC.Add(table.Rows[0]["Localit"].ToString());
                            }
                            msg.CC.Add("ithelpdesk@mindacorporation.com");

                        }

                        
                        msg.CC.Add(table.Rows[0]["manager_email_cc"].ToString());
                        msg.CC.Add(HttpContext.Session.GetString("SessionusernameEmail"));
                        msg.Bcc.Add("rajesh.kumar1@mindacorporation.com");
                        msg.Subject = Headermessage.ToString();

                        msg.Body = sb.ToString();
                        msg.IsBodyHtml = true;
                        SmtpClient SmtpClient = new SmtpClient();
                        SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                        SmtpClient.Host = "smtp.office365.com";
                        SmtpClient.Port = 587;
                        SmtpClient.EnableSsl = true;
                        SmtpClient.Send(msg);

                        returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                        returnresponse.message = table.Rows[0]["MESSAGE"].ToString();




                        //_response.header.error_message = "TSMS_Approvals Data!";






                    }
                    else
                    {
                        returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                        returnresponse.message = table.Rows[0]["MESSAGE"].ToString();



                    }


                }


                return Json(returnresponse);

            }
            catch (SystemException ex)
            {
                return Json(returnresponse);
            }

        }



        //----------------------------------End Generate Email ID Request ---------------------------------------------
        public static string Encode(string text)
        {
            // Convert to bytes
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);

            // Base64 encode
            string base64 = Convert.ToBase64String(textBytes);

            // Replace + with P and / with S
            string modifiedBase64 = base64.Replace("+", "P").Replace("/", "S");

            return modifiedBase64;
        }

        public static string Decode(string encodedText)
        {
            // Replace P with + and S with /
            string base64 = encodedText.Replace("P", "+").Replace("S", "/");

            // Decode
            byte[] decodedBytes = Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(decodedBytes);
        }

    }
}


