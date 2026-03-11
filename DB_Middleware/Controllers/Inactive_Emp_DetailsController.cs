using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DB_Middleware.DAO;
using DB_Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DB_Middleware.Controllers
{
    public class Inactive_Emp_DetailsController : Controller
    { 
        DB_DataAccess_Layer Inactive_EMP_Details;
        // GET: Inactive_Emp_DetailsController
        public ActionResult Index()
        {
            return View();
        }
        public Inactive_Emp_DetailsController(IConfiguration configuration)
        {
            Inactive_EMP_Details = new DB_DataAccess_Layer(configuration); 
        }
        // GET: Inactive_Emp_DetailsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Bind_Inactive_Emp()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Bind_Inactive_Emp_oninfra()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Bind_Inactive_Emp_Local_IT()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        
        // GET: Inactive_Emp_DetailsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inactive_Emp_DetailsController/Create
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

        // GET: Inactive_Emp_DetailsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inactive_Emp_DetailsController/Edit/5
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

        // GET: Inactive_Emp_DetailsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inactive_Emp_DetailsController/Delete/5
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

        public ActionResult get_active_empldetial_for_remove(string UniqueID) 
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            string filterid = Decode(UniqueID);
            List<Employeedetails> Empdetails = new List<Employeedetails>();

            string query = @"Bind_Employee_Details_IDFILTER";
            DataTable table = new DataTable();

            var con = Inactive_EMP_Details.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UniqueID", filterid);
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();

                    litem.fullname = table.Rows[i]["fullname"].ToString();
                    litem.fname = table.Rows[i]["first_name"].ToString();
                    litem.Mname = table.Rows[i]["middle_name"].ToString();
                    litem.Lname = table.Rows[i]["last_name"].ToString();
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.functional_area_dept = table.Rows[i]["functional_area_dept"].ToString();
                    litem.functional_area_subdept = table.Rows[i]["functional_area_subdept"].ToString();
                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.UserUniqueID = table.Rows[i]["UserUniqueID"].ToString();
                    litem.marital_status = table.Rows[i]["marital_status"].ToString();
                    litem.anniversary_date = table.Rows[i]["anniversary_date"].ToString();
                    litem.office_location = table.Rows[i]["office_location"].ToString();
                    litem.office_mobile_no = table.Rows[i]["office_mobile_no"].ToString();
                    litem.employee_status = table.Rows[i]["employee_status"].ToString();
                    litem.hrbp_name = table.Rows[i]["hrbp_name"].ToString();
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();
                    litem.company_email_id = table.Rows[i]["company_email_id"].ToString();
                    litem.employee_id = table.Rows[i]["employee_id"].ToString();
                    litem.Deisgnation = table.Rows[i]["employee_id"].ToString();
                    litem.job_level = table.Rows[i]["job_level"].ToString();
                    litem.date_of_exit = table.Rows[i]["date_of_exit"].ToString();
                    
                            litem.Accessblockdate = table.Rows[i]["Block_Email_date"].ToString();
                    litem.Fwdr_Email_Status = table.Rows[i]["FWDR_Email"].ToString();
                    litem.Fwdr_Email_Description = table.Rows[i]["FWDR_Email_Des"].ToString();
                    litem.GivenEmail_Onedrive = table.Rows[i]["WhomEmail_Onedrive"].ToString();
                    litem.GivenEmail_Onedrive_des = table.Rows[i]["WhomEmail_Onedrive_Des"].ToString();

                    litem.pststatus= table.Rows[i]["PSTStaus"].ToString();
                    litem.pstlocation = table.Rows[i]["PSTLocation"].ToString();




                    Empdetails.Add(litem);
                }

            }



            return Json(Empdetails);



        }
        public ActionResult get_inactive_empdetails()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            List<Employeedetails> PendingEmpdetails = new List<Employeedetails>();

            string query = @"Bind_Remove_Email_Request";
            DataTable table = new DataTable();

            var con = Inactive_EMP_Details.getConnectionString();
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

                    litem.fname = table.Rows[i]["Fullname"].ToString();
          
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.department_cost_center_name = table.Rows[i]["department_cost_center_name"].ToString();
                    litem.functional_area_dept = table.Rows[i]["functional_area_dept"].ToString();
                    litem.functional_area_subdept = table.Rows[i]["functional_area_subdept"].ToString();
                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.UserUniqueID = Encode(table.Rows[i]["UserUniqueID"].ToString());
                    litem.user_id = (table.Rows[i]["UserUniqueID"].ToString());
                    litem.office_location = table.Rows[i]["office_location"].ToString();
                    litem.date_of_joining = table.Rows[i]["group_date_of_joining"].ToString();

                    litem.employee_status = table.Rows[i]["employee_status"].ToString();
                   
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();
  

                    PendingEmpdetails.Add(litem);
                }

            }
            else { Employeedetails litem = new Employeedetails(); PendingEmpdetails.Add(litem); }



            return Json(PendingEmpdetails);



        }


        //-----------------------------------Email ID Remove Request By RM -----------------------------------function----
        [HttpPost]
        public ActionResult Validate_RM_Remove_Email([FromBody] Remove_EmailID objdata)
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




                string query = @"Validate_RM__Email";
                DataTable table = new DataTable();

                var con = Inactive_EMP_Details.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
          
                    cmd.Parameters.AddWithValue("@FWDREMAIL_des", objdata.Fwdr_Email_Description);
                    cmd.Parameters.AddWithValue("@GivenEmail_Onedrive_des", objdata.GivenEmail_Onedrive_des);
                    cmd.Parameters.AddWithValue("@FWDREMAIL", objdata.Fwdr_Email_Status);
                    cmd.Parameters.AddWithValue("@GivenEmail_Onedrive", objdata.GivenEmail_Onedrive);
                  

                    cmd.Parameters.AddWithValue("@Actionstatus", "Inactive Email ID");
                    cmd.Parameters.AddWithValue("@UniquieID", objdata.UniquieID);

                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionby", HttpContext.Session.GetString("Empcode"));
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



        //----------------------------------End Email ID Remove Request By RM ---------------------------------------------


        //-----------------------------------Email ID Remove Request By Infra -----------------------------------function----
        [HttpPost]
        public ActionResult Validate_Infra_Remove_Email([FromBody] Remove_EmailID objdata)
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


                

                string query = @"Validate_InfraTeam";
                DataTable table = new DataTable();

                var con = Inactive_EMP_Details.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PSTStatus", objdata.pststatus);
                    cmd.Parameters.AddWithValue("@PSTLocation", objdata.pstlocation);
                    cmd.Parameters.AddWithValue("@Status", objdata. status);
                   


                    cmd.Parameters.AddWithValue("@uniqueid", objdata.UniquieID);


                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionstatus", "Inactive Email ID");
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

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["group_company"].ToString() + "</td></tr>");
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Cost Center Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_cost_center_name"].ToString() + "</td></tr>");



                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["office_location"].ToString() + "</td></tr>");

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>PST Status</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.pststatus + "</td></tr>");



                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>PST Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.pstlocation + "</td></tr>");


                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>RM Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + "</td></tr>");
                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HRBP</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Remark</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.Remark + "</td></tr>");

                        //MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Mail CC</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + ',' + table.Rows[0]["MailSentCCInfra"].ToString() + ',' + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");


                        MailBody.Append("</table></html><br/>");
                        sb.Append("Dear " + table.Rows[0]["Fullnamesentto"].ToString() + ",");
                        sb.Append("<br>");
                        sb.Append("<br>");
                        String Headermessage = "";

                        if(objdata.status== "Approved")
                        {
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has been Inactived from System with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Employee ID Inactive- " + table.Rows[0]["Fullname"].ToString() + "";

                        }
                        if (objdata.status == "Reconsidered")
                        {
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has been Reconsidered  with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Reconsidered for Employee ID Inactive- " + table.Rows[0]["Fullname"].ToString() + "";
                        }
                        if (objdata.status == "Put On Hold")
                        {
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has been Put On Hold  with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Put On Hold for Employee ID Inactive- " + table.Rows[0]["Fullname"].ToString() + "";
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
                        msg.To.Add(table.Rows[0]["EmailSentTo"].ToString());
                        //msg.To.Add("pooja.thakur @mindacorporation.com");
                        
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



                return Json(returnresponse);

            }
            catch (SystemException ex)
            {
                return Json(returnresponse);
            }

        }



        //----------------------------------End Email ID Remove Request By Infra ---------------------------------------------

        //-----------------------------------Email ID Remove Request By Local IT -----------------------------------function----
        [HttpPost]
        public ActionResult Validate_LocalIT_Remove_Email([FromBody] Remove_EmailID objdata)
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




                string query = @"Validate_LocalITTeam"; 
                DataTable table = new DataTable();

                var con = Inactive_EMP_Details.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PSTStatus", objdata.pststatus);
                    cmd.Parameters.AddWithValue("@PSTLocation", objdata.pstlocation);
                    cmd.Parameters.AddWithValue("@Status", "Approved By Local IT");



                    cmd.Parameters.AddWithValue("@UniquieID", objdata.UniquieID);


                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionstatus", "Inactive Email ID");
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
                       
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["group_company"].ToString() + "</td></tr>");
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Cost Center Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_cost_center_name"].ToString() + "</td></tr>");
               
                       

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["office_location"].ToString() + "</td></tr>");

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>PST Status</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.pststatus + "</td></tr>");



                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>PST Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.pstlocation + "</td></tr>");


                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>RM Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + "</td></tr>");
                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HR BP</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Remark</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.Remark + "</td></tr>");

                        //MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Mail CC</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + ',' + table.Rows[0]["MailSentCCInfra"].ToString() +','+ table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");
                      

                        MailBody.Append("</table></html><br/>");
                        sb.Append("Dear " + table.Rows[0]["Fullnamesentto"].ToString() + ",");
                        sb.Append("<br>");
                        sb.Append("<br>");
                        String Headermessage = "";


                      
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has been updated PSD Location with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Employee ID Inactive- " + table.Rows[0]["Fullname"].ToString() + "";
                        
                       
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
                        msg.To.Add(table.Rows[0]["EmailSentTo"].ToString());
    
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



                return Json(returnresponse);

            }
            catch (SystemException ex)
            {
                return Json(returnresponse);
            }

        }



        //----------------------------------End Email ID Remove Request By Infra ---------------------------------------------


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
