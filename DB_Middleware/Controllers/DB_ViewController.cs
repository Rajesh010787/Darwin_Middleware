using DB_Middleware.DAO;
using DB_Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nancy.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DB_Middleware.Controllers
{
    public class DB_ViewController : Controller
    {
        DB_DataAccess_Layer Employedetails;
        public const string SessionKeyName = "_Name";
        public DB_ViewController(IConfiguration configuration)
        {
            Employedetails = new DB_DataAccess_Layer(configuration);
        }


        // GET: DB_ViewController
        public ActionResult Index()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Email_Update()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult ActiveEmployee_Details()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Pre_Joining_Empdetails()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Push_Darwinbox()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult BindTracking_Details()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }


        public ActionResult Employee_Details()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            return View();
        }
        public ActionResult Tracking_Prejoining()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            return View();
        }

        // GET: DB_ViewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DB_ViewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DB_ViewController/Create
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

        // GET: DB_ViewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DB_ViewController/Edit/5
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

        // GET: DB_ViewController/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: DB_ViewController/Delete/5
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
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public ActionResult get_pending_emp_records()
        {


            List<Employeedetails> PendingEmpdetails = new List<Employeedetails>();

            string query = @"Bind_Pending_Employee_Details";
            DataTable table = new DataTable();

            var con = Employedetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

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
                    litem.UserUniqueID = table.Rows[i]["UserUniqueID"].ToString();
                    litem.marital_status = table.Rows[i]["marital_status"].ToString();
                    litem.anniversary_date = table.Rows[i]["anniversary_date"].ToString();
                    litem.office_location = table.Rows[i]["office_location"].ToString();

                    litem.employee_status = table.Rows[i]["employee_status"].ToString();
                    litem.hrbp_name = table.Rows[i]["hrbp_name"].ToString();
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();


                    PendingEmpdetails.Add(litem);
                }

            }



            return Json(PendingEmpdetails);



        }
        //--------------------------Get Tracking Emp details ---------------------------------------------

        public ActionResult get_tracking_emp_records(string filter)
        {


            List<Employeedetails> Empdetails = new List<Employeedetails>();

            string query = @"Bind_Action_Details";
            DataTable table = new DataTable();

            var con = Employedetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Userecode", HttpContext.Session.GetString("Empcode"));
                cmd.Parameters.AddWithValue("@filtertype", filter);
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Employeedetails litem = new Employeedetails();

                    litem.fullname = table.Rows[i]["fullname"].ToString();
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.department_cost_center_name = table.Rows[i]["department_cost_center_name"].ToString();
                    litem.functional_area_dept = table.Rows[i]["functional_area_dept"].ToString();
                    litem.functional_area_subdept = table.Rows[i]["functional_area_subdept"].ToString();
                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.UserUniqueID = table.Rows[i]["UserUniqueID"].ToString();
                    litem.marital_status = table.Rows[i]["marital_status"].ToString();
                    litem.anniversary_date = table.Rows[i]["anniversary_date"].ToString();
                    litem.office_location = table.Rows[i]["office_location"].ToString();
                    litem.office_mobile_no = table.Rows[i]["office_mobile_no"].ToString();
                    litem.employee_status = table.Rows[i]["employee_status"].ToString();
                    litem.CStatus = table.Rows[i]["CStatus"].ToString();
                    litem.hrbp_name = table.Rows[i]["hrbp_name"].ToString();
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();
                    litem.company_email_id = table.Rows[i]["company_email_id"].ToString();
                    litem.employee_id = table.Rows[i]["employee_id"].ToString();

                    Empdetails.Add(litem);
                }

            }



            return Json(Empdetails);



        }
        //------------------------------------End tracking details --------------------------------------------------



        //--------------------------Get Tracking Emp history ---------------------------------------------

        public ActionResult get_tracking_emp_records_history(string UniqueID,string Requesttype)
        {
            string filterid = (UniqueID);

            List<Tracking_History> trackdetails = new List<Tracking_History>();

            string query = @"GET_ACTION_HISTORY";
            DataTable table = new DataTable();

            var con = Employedetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Uniqueid", filterid);
                cmd.Parameters.AddWithValue("@Requesttype", Requesttype);
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Tracking_History litem = new Tracking_History();
                    litem.id = filterid;
                    litem.Actionid = table.Rows[i]["Actionid"].ToString();
                    litem.fullname = table.Rows[i]["Full Name"].ToString();
                    litem.actionby = table.Rows[i]["Action by"].ToString();
                    litem.peningon = table.Rows[i]["Pending Name"].ToString();
                    litem.Cstatus = table.Rows[i]["Actionstatus"].ToString();
                    litem.requestfor = table.Rows[i]["Requestfor"].ToString();
                    litem.actiondate = table.Rows[i]["Actiondate"].ToString();

                    litem.department = table.Rows[i]["Department"].ToString();
                    litem.remark = table.Rows[i]["Remark"].ToString();


                    trackdetails.Add(litem);
                }

            }



            return Json(trackdetails);



        }


        public ActionResult get_tracking_preemp_records_history(string UniqueID)
        {
            string filterid = (UniqueID);

            List<Tracking_History> trackdetails = new List<Tracking_History>();

            string query = @"GET_ACTION_HISTORY_PREJOIN";
            DataTable table = new DataTable();

            var con = Employedetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Uniqueid", filterid);

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Tracking_History litem = new Tracking_History();
                    litem.id = filterid;
                    litem.Actionid = table.Rows[i]["Actionid"].ToString();
                    litem.fullname = table.Rows[i]["Full Name"].ToString();
                    litem.actionby = table.Rows[i]["Action by"].ToString();
                    litem.peningon = table.Rows[i]["Pending Name"].ToString();
                    litem.Cstatus = table.Rows[i]["ActionStatus"].ToString();
                    litem.requestfor = table.Rows[i]["Requestfor"].ToString();
                    litem.actiondate = table.Rows[i]["Actiondate"].ToString();

                    litem.department = table.Rows[i]["Department"].ToString();
                    litem.remark = table.Rows[i]["Remark"].ToString();


                    trackdetails.Add(litem);
                }

            }



            return Json(trackdetails);



        }
        //------------------------------------Get Tracking Emp history --------------------------------------------------

        public ActionResult get_active_emp_recordsbyID(string UniqueID)
        {


            List<Employeedetails> Empdetails = new List<Employeedetails>();

            string query = @"Bind_Employee_Details";
            DataTable table = new DataTable();

            var con = Employedetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UniqueID", UniqueID);
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
                    litem.fullname = table.Rows[i]["fullname"].ToString();
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.functional_area_dept = table.Rows[i]["functional_area_dept"].ToString();
                    litem.functional_area_subdept = table.Rows[i]["functional_area_subdept"].ToString();
                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.job_level = table.Rows[i]["job_level"].ToString();
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
                    litem.itemailcreated = table.Rows[i]["ITEmail_Created"].ToString();
                    litem.itemailpassword = table.Rows[i]["ITPassword"].ToString();
                    Empdetails.Add(litem);
                }

            }



            return Json(Empdetails);



        }
        public ActionResult get_active_emp_records()
        {
            //string filterid = Decrypt(UniqueID);

            List<Employeedetails> Empdetails = new List<Employeedetails>();

            string query = @"Bind_Employee_Details_For_Email_Update";
            DataTable table = new DataTable();

            var con = Employedetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Ecode", HttpContext.Session.GetString("Empcode"));
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
                    litem.department_cost_center_name = table.Rows[i]["department_cost_center_name"].ToString();
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

                    litem.itemailcreated = table.Rows[i]["ITEmail_Created"].ToString();
                    litem.itemailpassword = table.Rows[i]["ITPassword"].ToString();

                    Empdetails.Add(litem);
                }
             

            }
            else { Employeedetails litem1 = new Employeedetails();  Empdetails.Add(litem1); }



            return Json(Empdetails);



        }
        [HttpPost]
        public async Task<ActionResult> Update_EmailONDB([FromBody] EmailUpdateModel objdata)
        {

 
            Response returnresponse = new Response();
            try
            {
                var jsonResponse = "";
                var emp = new EmailRequestBoady
                {
                    api_key = "0073935288f56d45ae8e2fb61a340ff3cde16c874fad46fdc61f8142d3bd3ba087c9512ff18e59436f2ff8af80de7d30cbbc186da03a33383e1455722406e931",
                    email_id = objdata.EmailId.Trim(),
                    user_id = objdata.UniqueID.Trim(),
                    self_service = 1,
                    activate = 1
                };
                var apiUser = "mcl_kuldeep";
                var apiKey = "Minda@4321";
                var url = "https://sparkminda-hris.darwinbox.in/UpdateEmployeeDetails/update";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://sparkminda-hris.darwinbox.in/UpdateEmployeeDetails/update");

                var authToken = Encoding.ASCII.GetBytes($"{apiUser}:{apiKey}");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                var json = JsonConvert.SerializeObject(emp);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, httpContent);

                jsonResponse = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Root>(jsonResponse);

                returnresponse.self_service_message = result.self_service_message;
                if (returnresponse.self_service_message == "Email ID Updated successfully")
                {
                    Update_EmailID(objdata.UniqueID.Trim(), objdata.EmailId.Trim(), objdata.password.Trim(), objdata.Fullname);



                    returnresponse.Flag = "SUCCESS"; returnresponse.self_service_message = "Email ID Updated successfully in Darwin Box. Please verify in Darwin Box";


                }
                else { returnresponse.Flag = "error"; returnresponse.self_service_message = returnresponse.self_service_message; }


                return Json(returnresponse);
            }
            catch (Exception ex)
            {

                return Json(returnresponse);
            }
        }



        [HttpPost]
        public async Task<ActionResult> Updateempnotjoinded([FromBody] Employeedetails objdata)
        {


            Response R = new Response();
            try
            {

                DataSet ds = new DataSet();
                string query = @"HR_Request_For_EMP_NOTJOINED";
                var con = Employedetails.getConnectionString();
                using (var connection = new SqlConnection(con))
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Remark", objdata.remark);
                    cmd.Parameters.AddWithValue("UserUniqueID", objdata.UserUniqueID);
                    cmd.Parameters.AddWithValue("Actionemocode", HttpContext.Session.GetString("Empcode")); // Use the session value
                    cmd.Parameters.AddWithValue("Actionby", HttpContext.Session.GetString("Sessionusername")); // Use the session value
                    da.Fill(ds);
                    connection.Close();

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        R.Flag = ds.Tables[0].Rows[0]["FLAG"].ToString();
                        R.message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                    }
                    else
                    {
                        R.Flag = "Error";
                        R.message = "No data returned from the stored procedure.";
                    }
                    return Json(R);
                }
            }
            catch (Exception e)
            {
                R.Flag = "Error";
                R.message = $"Exception: {e.Message}";
                return Json(R);
            }
        }

        public ActionResult Welcome_Latter_Email_Sent(string fullname,string userid,string getpassword,string HREmail,string RMEmail)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Dear <b>" + fullname +", </b><br/><br/>");
            string subject = "Welcome to Spark Minda!";

            sb.Append("Congratulations and welcome onboard! We are thrilled to have you join the Spark Minda family.<br/><br/>");
            sb.Append("To help you get started on your journey with us, we are sharing the details of your email setup below:<br><br>");
            sb.Append("<b>User ID:</b> " + userid + "<br><br>");
            sb.Append("<b>Password:</b> " + getpassword + "<br><br>");
            sb.Append("Please take a moment to review the information. If you have any questions or concerns, feel free to reach out to your hiring manager.<br><br>");
            sb.Append("We are excited to embark on this journey together and look forward to your contributions!<br><br>");


            sb.Append("Best regards");
            sb.Append("<br>");
        
            sb.Append("Darwin Box Portal Team");

          
            sb.Append("<br>");

            Response R = new Response();
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            string userName = "noreply@mindacorporation.com";
            string password = "Sos67130";
            string frommail = "noreply@mindacorporation.com";
            MailAddress sentfrom = new MailAddress(frommail);
            msg.From = sentfrom;
            msg.To.Add(HREmail);
            msg.CC.Add(RMEmail);
            msg.Bcc.Add("rajesh.kumar1@mindacorporation.com");
            msg.Bcc.Add("sumati@mindacorporation.com");
            msg.Subject = subject.ToString();

            msg.Body = sb.ToString();
            msg.IsBodyHtml = true;
            SmtpClient SmtpClient = new SmtpClient();
            SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
            SmtpClient.Host = "smtp.office365.com";
            SmtpClient.Port = 587;
            SmtpClient.EnableSsl = true;
            SmtpClient.Send(msg);
            return Json(R);
        }



   

    public ActionResult Update_EmailID(string Uniqueid, string emailid, string password,string fullname)
    {

        Response R = new Response();
        try
        {
            // Check if the session value exists
            var sessionValue = HttpContext.Session.GetString("Empcode");
            if (sessionValue == null)
            {
                R.Flag = "Error";
                R.message = "Session value not found.";
                return Json(R);
            }

            DataSet ds = new DataSet();
            string query = @"UPDATE_EMAILID";
            var con = Employedetails.getConnectionString();
            using (var connection = new SqlConnection(con))
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("employee_emailid", emailid);
                cmd.Parameters.AddWithValue("Password", password);
                cmd.Parameters.AddWithValue("user_unique_id", Uniqueid);
                cmd.Parameters.AddWithValue("Username", sessionValue); // Use the session value
                da.Fill(ds);
                connection.Close();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    R.Flag = ds.Tables[0].Rows[0]["FLAG"].ToString();
                    R.message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();

                        //-----------------------email sent 

                        Welcome_Latter_Email_Sent(fullname, ds.Tables[0].Rows[0]["ITEmail"].ToString(), ds.Tables[0].Rows[0]["Passworddetails"].ToString(), ds.Tables[0].Rows[0]["HREmail"].ToString(),ds.Tables[0].Rows[0]["RMEMAIL"].ToString());
                    }
                else
                {
                    R.Flag = "Error";
                    R.message = "No data returned from the stored procedure.";
                }
                return Json(R);
            }
        }
        catch (Exception e)
        {
            R.Flag = "Error";
            R.message = $"Exception: {e.Message}";
            return Json(R);
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
