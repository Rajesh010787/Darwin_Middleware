using DB_Middleware.DAO;
using DB_Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace DB_Middleware.Controllers 
{

    public class AuthController : Controller
    {
        DB_DataAccess_Layer userauth;
        // GET: Auth

        public AuthController(IConfiguration configuration)
        {
            userauth = new DB_DataAccess_Layer(configuration);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult userlogin()
        {
            return View();
        }


  

        // GET: Auth/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Auth/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Auth/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auth/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Auth/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public static string GenerateOTP(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Alphanumeric characters
            Random random = new Random();
            char[] otp = new char[length];
            for (int i = 0; i < length; i++)
            {
                otp[i] = chars[random.Next(chars.Length)];
            }
            return new string(otp);
        }
        public void SendOtpEmail(string toEmail, string otpCode)
        {
            // SMTP server configuration
            var smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("noreply@mindacorporation.com", "Sos67130"),
                EnableSsl = true,
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@mindacorporation.com"),
                Subject = "Your One-Time Password (OTP)",
                Body = GenerateOtpEmailBody(otpCode),
                IsBodyHtml = true, // Use HTML formatting if needed
            };

            mailMessage.To.Add(toEmail);

            // Send the email
            smtpClient.Send(mailMessage);
        }
       
        public ActionResult Authentation_byLIT(string Username, string OTP) 
        {

            // AzureLogin();

            string msg = "";
            string constr = userauth.getConnectionString();




            SqlDataReader reader;

            Login_Detail getmessage = new Login_Detail();

            try
            {

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_Check_Login_For_LNT"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email_Id", Username);
                        cmd.Parameters.AddWithValue("@OTP", OTP);
                        cmd.Connection = con;
                        con.Open();
                        reader = cmd.ExecuteReader();

                        reader.Read(); 
                        if (reader["message"].ToString()!= "OTP has been expiryed")
                        {
                            getmessage.username = Username;
                            //Session["username"] = Username;
                            //Session["Fullnname"] = reader["FULLNAME"].ToString();
                            //message.userid = reader["USERID"].ToString();
                            //Session["USERID"] = reader["USERID"].ToString();
                            //message.Emailid = reader["EMAILID"].ToString();
                            //Session["Email"] = reader["EMAILID"].ToString();
                            //Session["ROLES"] = reader["ROLES"].ToString();
                            getmessage.Rolename = reader["ROLES"].ToString();
                        HttpContext.Session.SetString("Sessionfullname", reader["FULLNAME"].ToString());
                        HttpContext.Session.SetString("Sessionusername", Username);
                        HttpContext.Session.SetString("Empcode", reader["Empcode"].ToString());
                            getmessage.Activestatus = reader["Activestatus"].ToString();
                        if (getmessage.Activestatus == "True")
                        {

                                getmessage.Message = "login Successfully";
                                getmessage.URL = reader["URL"].ToString();
                                getmessage.Flag = "SUCCESS";

                        }

                        else { getmessage.Message = "Please check your userid and password"; getmessage.Flag = "error"; getmessage.URL = reader["URL"].ToString(); }
                        con.Close();
                        }
                        else { getmessage.Message = reader["message"].ToString(); getmessage.Flag = "error"; getmessage.URL = ""; }


                    }
                }




                //}

            }
            catch (Exception Ex)
            {
                getmessage.Message = "Inter server Error please check";
                getmessage.Flag = "error";
                return Json(getmessage);
                //LogCounter(globalCounter, ConfigurationManager.AppSettings["Enviorement"].ToString(), CounterAppCode, "Service-ItimeApprovals", JsonConvert.SerializeObject(value), null, Ex.GetBaseException().ToString(), "Error");
            }

            return Json(getmessage);



        }
        private string GenerateOtpEmailBody(string otpCode)
        {
            return $@"
            <html>
            <body>
                <h2>Your One-Time Password (OTP)</h2>
                <p>Dear User,</p>
                <p>We have received a request to verify your identity with a One-Time Password (OTP).</p>
                <p><strong>Your OTP is:</strong> <span style='font-size:20px; color:#007BFF;'>{otpCode}</span></p>
                <p>This OTP is valid for the next 5 minutes and can only be used once.</p>
                <p>If you did not request this, please ignore this email.</p>
                <br/>
                <p>Regards, <br/>Darwin Box Portal Team</p>
            </body>
            </html>";
        }
    
    [HttpPost]
        public ActionResult Generate_Send_OTP([FromBody]  LIU_Login model)
        {
            Response R = new Response();
            try
            {
                if (string.IsNullOrEmpty(model.Email) || !IsValidEmail(model.Email))
                {
                  
                    R.message = "Invalid email address";
                }
                else { 
                string otp = GenerateOTP();
                DataSet ds = new DataSet();
                string query = @"Add_OTP";
                var con = userauth.getConnectionString();
                using (var connection = new SqlConnection(con))
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("employee_emailid", model.Email);
                    cmd.Parameters.AddWithValue("opt", otp);
                        cmd.Parameters.AddWithValue("status", 0);
                        

                    da.Fill(ds);
                    connection.Close();

                    if (ds.Tables[0].Rows[0]["FLAG"].ToString() == "SUCCESS")
                    {

                        R.Flag = ds.Tables[0].Rows[0]["FLAG"].ToString();
                        R.message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();

                            SendOtpEmail("rajesh.kumar1@mindacorporation.com", otp);
                    }
                    else
                    {
                        R.Flag = "Error";
                        R.message = "No data returned from the stored procedure.";
                    }
                   
                }
                }
                return Json(R);
            }
            catch (Exception e)
            {
                R.Flag = "Error";
                R.message = $"Exception: {e.Message}";
                return Json(R);
            }
        }


        private bool IsValidEmail(string email)
        {
            // C# Regex pattern for email validation
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$";
            return Regex.IsMatch(email, emailPattern);
        }
        public string internalsignout()
        {
            //Session["Email"] = null;
            //Session["username"] = null;
            //Session["Fullnname"] = null;
            //Session["USERID"] = null;


            //Session.Abandon();
            return "success";
        }
        public string Creatorsignout()
        {
            //Session["Email"] = null;
            //Session["username"] = null;
            //Session["Fullnname"] = null;
            //Session["USERID"] = null;
            //Session["Initiated"] = null;

            //Session.Abandon();
            return "success";
        }
   
  


//public ActionResult validate_user(string mail, string pass)
//{
//    List<valid_user> loginhrd = new List<valid_user>();

//    string query = @"Exim_snk_forwader_login";
//    DataTable table = new DataTable();
//    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
//    using (var cmd = new SqlCommand(query, con))
//    using (var da = new SqlDataAdapter(cmd))
//    {
//        cmd.CommandType = CommandType.StoredProcedure;
//        cmd.Parameters.AddWithValue("email", mail);
//        cmd.Parameters.AddWithValue("pass", pass);

//        da.Fill(table);
//    }

//    if (table.Rows.Count > 0)
//    {

//        for (int i = 0; i < table.Rows.Count; i++)
//        {
//            valid_user loginline = new valid_user();
//            loginline.id = Convert.ToInt32(table.Rows[i]["id"].ToString());

//            loginline.email = table.Rows[i]["emailid"].ToString();
//            loginline.contactno = table.Rows[i]["contactnumber"].ToString();
//            Session["USERID"] = "0";
//            loginline.usertype = "";





//            loginhrd.Add(loginline);
//        }

//    }

//    return Json(loginhrd, JsonRequestBehavior.AllowGet);

//}
    }
}

