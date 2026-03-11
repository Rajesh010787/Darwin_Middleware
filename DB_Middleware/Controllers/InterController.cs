using Microsoft.AspNetCore.Mvc;
using DB_Middleware.Models;
using Saml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using static DB_Middleware.Models.Menu;
using DB_Middleware;
using DB_Middleware.DAO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace  DB_Middleware.Controllers 
{
    public class InterController : Controller
    {
   
        private SqlCommand com;
        DB_DataAccess_Layer userauth;
        public const string SessionKeyName = "_Name";
        public InterController(IConfiguration configuration)
        {
            userauth = new DB_DataAccess_Layer(configuration);
        }

        // GET: Interuserlogin
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult User_Login()
        {
           // CallAzure();

            return View();
        }
        public ActionResult LTSUser_Login()
        {

         
            return View();
        }

        public ActionResult Creatorsignout()
        {
            


            //this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            //this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //this.Response.Cache.SetNoStore();
            //Session["Email"] = null;  
            //Session["username"] = null;
            //Session["Fullnname"] = null;
            //Session["USERID"] = null;
            //Session["Initiated"] = null;

            //Session.Abandon();

            //  return RedirectToAction("userlogin", "Inter");
            //return RedirectToAction("Login", "Account");
            var samlEndpoint = AzureUrlData.samlLogout;
            dynamic request = samlDyanmics.AzureAuthRequest();
            //redirect the user to the SAML provider
            //Response.Redirect(request.GetRedirectUrl(samlEndpoint));
            return Redirect(request.GetRedirectUrl(samlEndpoint));



        }
      
        public void CallAzure()
        {
            var samlEndpoint = AzureUrlData.samlLogin;

            //var request = new AuthRequest(
            //    "http://localhost:44381/", //TODO: put your app's "entity ID" here
            //    "http://localhost:44381/Inter/AzureLogin" //TODO: put Assertion Consumer URL (where the provider should redirect users after authenticating)
            //    );

            var request = new AuthRequest(
               "https://darwinmware.sparkminda.in/", //TODO: put your app's "entity ID" here
               "https://darwinmware.sparkminda.in/Inter/AzureLogin" //TODO: put Assertion Consumer URL (where the provider should redirect users after authenticating)
               );

            //redirect the user to the SAML provider
            Response.Redirect(request.GetRedirectUrl(samlEndpoint));
        }
        public JsonResult Authentation(string Username, string Password)
        {

           // AzureLogin();

            string msg = "";
            string constr = userauth.getConnectionString();




            SqlDataReader reader;

            Login_Detail message = new Login_Detail();

            try
            {
                
                    using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_Check_Login"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email_Id", Username);
                        cmd.Connection = con;
                        con.Open();
                        reader = cmd.ExecuteReader();

                        reader.Read();
                        message.username = Username;
                        //Session["username"] = Username;
                        //Session["Fullnname"] = reader["FULLNAME"].ToString();
                        //message.userid = reader["USERID"].ToString();
                        //Session["USERID"] = reader["USERID"].ToString();
                        //message.Emailid = reader["EMAILID"].ToString();
                        //Session["Email"] = reader["EMAILID"].ToString();
                        //Session["ROLES"] = reader["ROLES"].ToString();
                        message.Rolename = reader["ROLES"].ToString();
                        HttpContext.Session.SetString("Sessionfullname", reader["FULLNAME"].ToString());
                        HttpContext.Session.SetString("Sessionusername", Username);
                        HttpContext.Session.SetString("SessionusernameEmail", Username);
                        HttpContext.Session.SetString("Empcode", reader["Empcode"].ToString());
                        HttpContext.Session.SetString("Depart", reader["Depart"].ToString());
                        HttpContext.Session.SetString("DOJ", reader["DOJ"].ToString());

                        HttpContext.Session.SetString("BV", reader["BV"].ToString());
                        HttpContext.Session.SetString("Plant", reader["Plant"].ToString());
                        HttpContext.Session.SetString("Division", reader["Division"].ToString());
                        message.Activestatus = reader["Activestatus"].ToString();
                        if (message.Activestatus == "True")
                        { 
                            
                            message.Message = "login Successfully";
                            message.URL = reader["URL"].ToString();
                            message.Flag = "SUCCESS"; 
                        
                        }

                        else { message.Message = "Please check your userid and password"; message.Flag = "error"; message.URL = reader["URL"].ToString(); }
                        con.Close();


                        }
                    }
              



                //}

            }
            catch (Exception Ex)
            {
                message.Message = "Inter server Error please check";
                message.Flag = "error";
                return Json(message);
                //LogCounter(globalCounter, ConfigurationManager.AppSettings["Enviorement"].ToString(), CounterAppCode, "Service-ItimeApprovals", JsonConvert.SerializeObject(value), null, Ex.GetBaseException().ToString(), "Error");
            }

            return Json(message);



        }


        //azure login

        [HttpPost]
        public ActionResult AzureLogin()
        {
            if (Request.Form["SAMLResponse"].ToString()!= null)
            {
                try
                {
                    string samlCertificate = samlDyanmics.Certificatedata();
                    Saml.Response1 samlResponse = new Response1(samlCertificate, Request.Form["SAMLResponse"]);

                    if (samlResponse.IsValid())
                    {
                        string username = samlResponse.GetNameID();
                        string email = samlResponse.GetEmail();
                        string firstname = samlResponse.GetFirstName();
                        string lastname = samlResponse.GetLastName();

                        // Use a more robust approach to handle SQL connections and commands
                        string constr = userauth.getConnectionString();
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand("USP_Check_Login", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Email_Id", email);

                                con.Open();
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                   
                                    if (reader.Read())
                                    {
                                        HttpContext.Session.SetString("Sessionfullname", reader["FULLNAME"].ToString());
                                        HttpContext.Session.SetString("Sessionusername", username);
                                        HttpContext.Session.SetString("SessionusernameEmail", email);
                                        HttpContext.Session.SetString("Empcode", reader["Empcode"].ToString());
                                        HttpContext.Session.SetString("Depart", reader["Depart"].ToString());

                                        HttpContext.Session.SetString("DOJ", reader["DOJ"].ToString());

                                        HttpContext.Session.SetString("BV", reader["BV"].ToString());
                                        HttpContext.Session.SetString("Plant", reader["Plant"].ToString());
                                        HttpContext.Session.SetString("Division", reader["Division"].ToString());

                                        HttpContext.Session.SetString("DOJ", reader["DOJ"].ToString());
                                        var message = new Login_Detail
                                        {
                                            username = email,
                                            userid = reader["USERID"].ToString(),
                                            Emailid = reader["EMAILID"].ToString(),
                                            Rolename = reader["ROLES"].ToString(),
                                            Activestatus = reader["Activestatus"].ToString(),

                                        };

                                        if (message.Activestatus == "True")
                                        {
                                            message.Message = "Login Successfully";
                                            message.URL = reader["URL"].ToString();
                                            message.Flag = "SUCCESS";
                                            return RedirectToAction(message.URL, reader["Controller"].ToString()); 
                                        }
                                        else
                                        {
                                            message.Message = "Please check your userid and password";
                                            message.Flag = "error";
                                            message.URL = reader["URL"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        // Handle the case where no records are returned
                                        return RedirectToAction("userlogin", "Inter");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Handle invalid SAML response
                        return RedirectToAction("userlogin", "Inter");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (logging should be implemented in a real scenario)
                    Console.WriteLine($"Error: {ex.Message}");
                    // Redirect to the login page or show an error message
                    return RedirectToAction("userlogin", "Inter");
                }
            }

            // If SAMLResponse is null, return the view
            return View();
        }



    }
}