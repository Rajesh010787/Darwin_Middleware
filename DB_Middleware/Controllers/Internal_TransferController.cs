using DB_Middleware.DAO;
using DB_Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DB_Middleware.Controllers
{
    public class Internal_TransferController : Controller
    {
        DB_DataAccess_Layer GeteactiveEmployeedetails; 
        // GET: Internal_TransferController
        public ActionResult Index()
        {
            return View();
        }
        public Internal_TransferController(IConfiguration configuration)
        {

            GeteactiveEmployeedetails = new DB_DataAccess_Layer(configuration); 
        }

        // GET: Internal_TransferController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Validate_By_HR()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Validate_By_Infra() 
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Validate_By_Direct_Infra() 
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Bind_On_HR()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Bind_Direct_on_infra()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        
        public ActionResult Bind_ON_Infra() 
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        // GET: Internal_TransferController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Internal_TransferController/Create
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

        // GET: Internal_TransferController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Internal_TransferController/Edit/5
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

        // GET: Internal_TransferController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Internal_TransferController/Delete/5
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


        // bind active employee details 


        //-------------------------------Bind all internal transfer records ----------------------------------------
        public ActionResult getactibveemployeedetails(string  bindtype) 
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            List<Employeedetails> PendingEmpdetails = new List<Employeedetails>();

            string query = @"Active_Employee_HR";
            DataTable table = new DataTable();

            var con = GeteactiveEmployeedetails.getConnectionString(); 
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string a = HttpContext.Session.GetString("Empcode");
                cmd.Parameters.AddWithValue("@EmpCode", HttpContext.Session.GetString("Empcode"));
                cmd.Parameters.AddWithValue("@Bindtype", bindtype);
                
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();

                    litem.fname = table.Rows[i]["FullName"].ToString();

                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.department_cost_center_name = table.Rows[i]["department_cost_center_name"].ToString();
                    
                    litem.company_email_id = table.Rows[i]["company_email_id"].ToString();
                    litem.UserUniqueID = Encode(table.Rows[i]["UserUniqueID"].ToString());
                    litem.user_id = (table.Rows[i]["UserUniqueID"].ToString());
                    litem.CStatus = (table.Rows[i]["status"].ToString());
                    litem.office_location = table.Rows[i]["office_location"].ToString();
                    litem.date_of_joining = table.Rows[i]["group_date_of_joining"].ToString();
                    litem.department = table.Rows[i]["department_name"].ToString();
                    litem.employee_id = table.Rows[i]["department_name"].ToString();
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();

                    PendingEmpdetails.Add(litem);
                }

            }
            else { Employeedetails litem = new Employeedetails(); PendingEmpdetails.Add(litem); }



            return Json(PendingEmpdetails);



        }


        //-------------------------------End  internal transfer records ----------------------------------------
        public ActionResult getactibGetemployeedetails(string  Uniquesid)
        {
            string uniqueid = Decode(Uniquesid);

            List<Employeedetails> PendingEmpdetails = new List<Employeedetails>();

            string query = @"GET_EXICTINNG_NEWRECORDS";
            DataTable table = new DataTable();

            var con = GeteactiveEmployeedetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string a = HttpContext.Session.GetString("Empcode");
                cmd.Parameters.AddWithValue("@Useruniqueid", uniqueid);
           
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();

                    litem.fname = table.Rows[i]["FullName"].ToString();

                    litem.group_company = table.Rows[i]["BusinessUnit"].ToString();
                    litem.department_cost_center_name = table.Rows[i]["Division"].ToString();

                    litem.company_email_id = table.Rows[i]["EmailID"].ToString();
                    litem.UserUniqueID = Encode(table.Rows[i]["Useruniqueid"].ToString());

                    litem.office_location = table.Rows[i]["LocationDetails"].ToString();
                  
                    litem.department = table.Rows[i]["Department"].ToString();
                    litem.employee_id = table.Rows[i]["Ecode"].ToString();
             
                    litem.Requestype = table.Rows[i]["Tabletype"].ToString();
                    litem.date_of_joining = table.Rows[i]["dateofjoining"].ToString();


                    litem.HR_Access_Domain_Email = table.Rows[i]["Accessdomainemailid"].ToString();
                    litem.HR_Access_Required_Group = table.Rows[i]["Accessrequestforgroup"].ToString();

                    litem.effectivedate = table.Rows[i]["EffectiveDate"].ToString();


                    litem.office_mobile_no = table.Rows[i]["Mobilenumber"].ToString();


                    litem.direct_manager_name = table.Rows[i]["Managername"].ToString();
                    litem.direct_manager_email = table.Rows[i]["Manageremail"].ToString();
                    litem.job_level = table.Rows[i]["Joblevel"].ToString();

                    litem.remark = table.Rows[i]["Remark_Des"].ToString();

                    PendingEmpdetails.Add(litem);
                }

            }
            else { Employeedetails litem = new Employeedetails(); PendingEmpdetails.Add(litem); }



            return Json(PendingEmpdetails);



        }

        //-----------------------Direct transfer get details --------------------------

        public ActionResult getactibGetemployeedetailsfordirecttransfer(string Uniquesid)
        {
            string uniqueid = Decode(Uniquesid);

            List<Employeedetails> PendingEmpdetails = new List<Employeedetails>();

            string query = @"GET_EXICTINNG_NEWRECORDS_Direct";
            DataTable table = new DataTable();

            var con = GeteactiveEmployeedetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string a = HttpContext.Session.GetString("Empcode");
                cmd.Parameters.AddWithValue("@Useruniqueid", uniqueid);

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();

                    litem.fname = table.Rows[i]["FullName"].ToString();

                    litem.group_company = table.Rows[i]["BusinessUnit"].ToString();
                    litem.department_cost_center_name = table.Rows[i]["Division"].ToString();

                    litem.company_email_id = table.Rows[i]["EmailID"].ToString();
                    litem.UserUniqueID = Encode(table.Rows[i]["Useruniqueid"].ToString());

                    litem.office_location = table.Rows[i]["LocationDetails"].ToString();

                    litem.department = table.Rows[i]["Department"].ToString();
                    litem.employee_id = table.Rows[i]["Ecode"].ToString();

                    litem.Requestype = table.Rows[i]["Tabletype"].ToString();
                    litem.date_of_joining = table.Rows[i]["dateofjoining"].ToString();


                    litem.HR_Access_Domain_Email = table.Rows[i]["Accessdomainemailid"].ToString();
                    litem.HR_Access_Required_Group = table.Rows[i]["Accessrequestforgroup"].ToString();

                    litem.effectivedate = table.Rows[i]["EffectiveDate"].ToString();


                    litem.office_mobile_no = table.Rows[i]["Mobilenumber"].ToString();


                    litem.direct_manager_name = table.Rows[i]["Managername"].ToString();
                    litem.direct_manager_email = table.Rows[i]["Manageremail"].ToString();
                    litem.job_level = table.Rows[i]["Joblevel"].ToString();

                    litem.remark = table.Rows[i]["Remark_Des"].ToString();

                    PendingEmpdetails.Add(litem);
                }

            }
            else { Employeedetails litem = new Employeedetails(); PendingEmpdetails.Add(litem); }



            return Json(PendingEmpdetails);



        }


        //-----------------------------------Validate HR for Internal Transfer -----------------------------------function----//
        [HttpPost]
        public ActionResult  VAlidate_By_HR_Transfercase([FromBody] Request_Email_Data objdata)
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




                string query = @"Update_Record_Transfer_Case_HR";
                DataTable table = new DataTable();

                var con = GeteactiveEmployeedetails.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Uniqueid", Decode(objdata.UserUniqueID));
         
                    cmd.Parameters.AddWithValue("@Accessdoaminreq", objdata.accessdomainandenmailidrequest);
           
                    cmd.Parameters.AddWithValue("@Accessreq_group", objdata.HR_Access_Required_Group);

                    cmd.Parameters.AddWithValue("@Effectivedate",Convert.ToDateTime( objdata.Effectivedate));
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionby", HttpContext.Session.GetString("Sessionusername"));
                    cmd.Parameters.AddWithValue("@Actionemocode", HttpContext.Session.GetString("Empcode"));
                    da.Fill(table);
                }

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


                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Full Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Fullname"].ToString() + "</td></tr>");
                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>DB DOJ</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["date_of_joining"].ToString() + "</td></tr>");

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["group_company"].ToString() + "</td></tr>");
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Cost Center Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_cost_center_name"].ToString() + "</td></tr>");


                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["office_location"].ToString() + "</td></tr>");

                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>RM Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + "</td></tr>");
                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HRBP</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Remark</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.Remark + "</td></tr>");
                        //MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Mail CC</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + ',' + table.Rows[0]["MailSentCCInfra"].ToString() + ',' + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");
                        MailBody.Append("</table></html><br/>");
                        sb.Append("Dear " + table.Rows[0]["FullnameSentto"].ToString() + ",");
                        sb.Append("<br>");
                        sb.Append("<br>");
                        String Headermessage = "";



                        sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has been initiated for employee transfer request  with details as follows:");
                        sb.Append("</br>");
                        sb.Append("</br>");
                        sb.Append("</br>");
                        sb.Append("</br>");
                        Headermessage = "Employee Transfer Request- " + table.Rows[0]["Fullname"].ToString() + "";


                        sb.Append(MailBody.ToString());
                        sb.Append("<br>"); sb.Append("<br>");
                        sb.Append("Click on <a href='https://darwinmware.sparkminda.in/'>Link</a> to reach employee transfer request & take necessary action.");
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



        //----------------------------------End Generate Email ID Request ---------------------------------------------





        //-----------------------------------Validate Direct for Internal Transfer -----------------------------------function----//
        [HttpPost]
        public ActionResult VAlidate_By_Infra_DirectTransfercase([FromBody] Request_Email_Data objdata) 
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




                string query = @"Update_Record_DitrectTransfer_Case_HR";
                DataTable table = new DataTable();

                var con = GeteactiveEmployeedetails.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Uniqueid", Decode(objdata.UserUniqueID));

                  
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionby", HttpContext.Session.GetString("Sessionusername"));
                    cmd.Parameters.AddWithValue("@Actionemocode", HttpContext.Session.GetString("Empcode"));
                    da.Fill(table);
                }

                //if (table.Rows.Count > 0)
                //{


                //    returnresponse.Flag = table.Rows[0]["Flag"].ToString();






                //    //string mailcc = "";

                //    //if (returnresponse.Flag == "SUCCESS")
                //    //{

                //    //    StringBuilder MailBody = new StringBuilder();
                //    //    StringBuilder sb = new StringBuilder();

                //    //    MailBody.Append("<br><html><table  border='1'  background-color: #dddddd; color: white; text-align: center;padding: 8px; style='border: 1px solid black; border - collapse: collapse; width: 500px;'>");
                //    //    MailBody.Append("<tr style='border: 1px solid black; border - collapse: collapse;'><td style='border: 1px solid black; border - collapse: collapse; background - color: #dddddd;' colspan='2'><strong>Employee Request Detail</strong></td></tr>");


                //    //    MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Full Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Fullname"].ToString() + "</td></tr>");
                //    //    MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>DB DOJ</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["date_of_joining"].ToString() + "</td></tr>");

                //    //    MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["group_company"].ToString() + "</td></tr>");
                //    //    MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Cost Center Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_cost_center_name"].ToString() + "</td></tr>");


                //    //    MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["office_location"].ToString() + "</td></tr>");

                //    //    MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>RM Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + "</td></tr>");
                //    //    MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HRBP</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");

                //    //    MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Remark</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.Remark + "</td></tr>");
                //    //    MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Mail CC</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + ',' + table.Rows[0]["MailSentCCInfra"].ToString() + ',' + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");
                //    //    MailBody.Append("</table></html><br/>");
                //    //    sb.Append("Dear " + table.Rows[0]["FullnameSentto"].ToString() + ",");
                //    //    sb.Append("<br>");
                //    //    sb.Append("<br>");
                //    //    String Headermessage = "";



                //    //    sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has been initiated for employee transfer request  with details as follows:");
                //    //    sb.Append("</br>");
                //    //    sb.Append("</br>");
                //    //    sb.Append("</br>");
                //    //    sb.Append("</br>");
                //    //    Headermessage = "Employee Transfer Request- " + table.Rows[0]["Fullname"].ToString() + "";


                //    //    sb.Append(MailBody.ToString());
                //    //    sb.Append("<br>"); sb.Append("<br>");
                //    //    sb.Append("Click on <a href='https://darwinmware.sparkminda.in/'>Link</a> to reach employee transfer request & take necessary action.");
                //    //    sb.Append("<br>");
                //    //    sb.Append("<br>");
                //    //    sb.Append("Darwin Box Portal Team");

                //    //    sb.Append("<br>");
                //    //    sb.Append("Regards"); sb.Append("<br>");
                //    //    sb.Append("<br>");

                //    //    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                //    //    string userName = "noreply@mindacorporation.com";
                //    //    string password = "Sos67130";
                //    //    string frommail = "noreply@mindacorporation.com";
                //    //    MailAddress sentfrom = new MailAddress(frommail);
                //    //    msg.From = sentfrom;
                //    //    msg.To.Add("rajesh.kumar1@mindacorporation.com");
                //    //    msg.To.Add("pooja.thakur @mindacorporation.com");
                //    //    //msg.To.Add("rajesh.kumar1@mindacorporation.com");
                //    //    //msg.CC.Add("rajesh.kumar1@mindacorporation.com");
                //    //    msg.Bcc.Add("rajesh.kumar1@mindacorporation.com");
                //    //    msg.Subject = Headermessage.ToString();

                //    //    msg.Body = sb.ToString();
                //    //    msg.IsBodyHtml = true;
                //    //    SmtpClient SmtpClient = new SmtpClient();
                //    //    SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                //    //    SmtpClient.Host = "smtp.office365.com";
                //    //    SmtpClient.Port = 587;
                //    //    SmtpClient.EnableSsl = true;
                //    //    SmtpClient.Send(msg);

                //    //    returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                //    //    returnresponse.message = table.Rows[0]["MESSAGE"].ToString();




                //    //    //_response.header.error_message = "TSMS_Approvals Data!";






                //    //}
                //    else
                //    {
                //        returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                //        returnresponse.message = table.Rows[0]["MESSAGE"].ToString();



                //    }


                //}


                returnresponse.Flag = table.Rows[0]["FLAG"].ToString();
                   returnresponse.message = table.Rows[0]["MESSAGE"].ToString();
                return Json(returnresponse);

            }
            catch (SystemException ex)
            {
                return Json(returnresponse);
            }

        }



        //----------------------------------End Direct Direct Email ID Request ---------------------------------------------



        //-----------------------------------Validate Infra for Internal Transfer -----------------------------------function----//
        [HttpPost]
        public ActionResult VAlidate_By_Infra_Transfercase([FromBody] Request_Email_Data objdata)
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




                string query = @"Update_record_transfer_case_infra";
                DataTable table = new DataTable();

                var con = GeteactiveEmployeedetails.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Uniqueid", Decode(objdata.UserUniqueID));

                    cmd.Parameters.AddWithValue("@Email", objdata.ITEmailCreated);

                    cmd.Parameters.AddWithValue("@Newlicencepurchase", objdata.NewLicence_Prucure);

                    cmd.Parameters.AddWithValue("@MSlicence", objdata.MS_LicenceAllocated);
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@Actionby", HttpContext.Session.GetString("Sessionusername"));
                    cmd.Parameters.AddWithValue("@Actionemocode", HttpContext.Session.GetString("Empcode"));
                    cmd.Parameters.AddWithValue("@Status", objdata.Status);

                    
                    da.Fill(table);
                }

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


                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Full Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["Fullname"].ToString() + "</td></tr>");
                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>DB DOJ</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["date_of_joining"].ToString() + "</td></tr>");

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Company Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["group_company"].ToString() + "</td></tr>");
                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Cost Center Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["department_cost_center_name"].ToString() + "</td></tr>");


                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Location</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["office_location"].ToString() + "</td></tr>");

                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>RM Name</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + "</td></tr>");
                        MailBody.Append("<tr style='background - color: #dddddd;'> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>HRBP</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");

                        MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Remark</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + objdata.Remark + "</td></tr>");
                        //MailBody.Append("<tr> <td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>Mail CC</td><td  background-color: #dddddd; color: white; text-align: center;padding: 8px;>" + table.Rows[0]["manager_email_cc"].ToString() + ',' + table.Rows[0]["MailSentCCInfra"].ToString() + ',' + table.Rows[0]["hr_bp_email_cc"].ToString() + "</td></tr>");
                        MailBody.Append("</table></html><br/>");
                        sb.Append("Dear " + table.Rows[0]["FullnameSentto"].ToString() + ",");
                        sb.Append("<br>");
                        sb.Append("<br>");
                        String Headermessage = "";

                        if(objdata.Status=="Approved")
                        {
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + "has been updated for employee transfer request  with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Employee Transfer Request- " + table.Rows[0]["Fullname"].ToString() + "";
                        }
                        if (objdata.Status == "Reconsidered")
                        {
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has been Reconsidered for employee transfer request  with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Reconsidered Employee Transfer Request- " + table.Rows[0]["Fullname"].ToString() + "";
                        }
                        if (objdata.Status == "Put On Hold")
                        {
                            sb.Append("" + HttpContext.Session.GetString("Sessionfullname") + " has been Put On Hold for employee transfer request  with details as follows:");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            sb.Append("</br>");
                            Headermessage = "Put On Hold Employee Transfer Request- " + table.Rows[0]["Fullname"].ToString() + "";
                        }



                        sb.Append(MailBody.ToString());
                        sb.Append("<br>"); sb.Append("<br>");
                        sb.Append("Click on <a href='https://darwinmware.sparkminda.in/'>Link</a> to reach employee transfer request & take necessary action.");
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
                    
                        msg.CC.Add(HttpContext.Session.GetString("SessionusernameEmail"));
                        //msg.To.Add("rajesh.kumar1@mindacorporation.com");
                        msg.CC.Add(table.Rows[0]["manager_email_cc"].ToString());
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

        //-----------------------------------End Bind Email Block Requests -----------------------------------function----



    }
}
