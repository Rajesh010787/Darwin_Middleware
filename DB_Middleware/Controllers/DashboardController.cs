using DB_Middleware.DAO;
using DB_Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace DB_Middleware.Controllers
{
    public class DashboardController : Controller
    {
         DB_DataAccess_Layer Dashbaorddata;
        public const string SessionKeyName = "_Name";
      
        public DashboardController(IConfiguration configuration)
        {
            Dashbaorddata = new DB_DataAccess_Layer(configuration);
        }
      
        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Common_Dashboard() 
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            var dashboardData = GetDashboardAsync();

            // Pass the data to the view
            return View(dashboardData);
        }
        public Dashboarddata GetDashboardAsync()
        {
            
            Dashboarddata litem = new Dashboarddata();

            try
            {


                string query = @"Common_Dashboard";
                DataTable table = new DataTable();

                var con = Dashbaorddata.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ecode = HttpContext.Session.GetString("Empcode");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ecode", ecode);
                    da.Fill(table);
                }

                if (table.Rows.Count > 0)
                {

                   


                        litem.PendingEmailCreation = table.Rows[0]["PendingEmailCreation"].ToString();
                        litem.PendingEmailBlock = table.Rows[0]["PendingEmailBlock"].ToString();
                        litem.PendingEmailDelete = table.Rows[0]["PendingEmailDelete"].ToString();
                        litem.TotalAction = table.Rows[0]["TotalAction"].ToString();
                        litem.Pendingtransfer = table.Rows[0]["Pendingtransfer"].ToString();

                    litem.EmailCreationpageurl = table.Rows[0]["EmailCreationpageurl"].ToString();
                    litem.EmailBlockpageurl = table.Rows[0]["EmailBlockpageurl"].ToString();
                    litem.EmailRemovepageurl = table.Rows[0]["EmailRemovepageurl"].ToString();
                    litem.Intertranferpegurl = table.Rows[0]["Intertranferpegurl"].ToString();




                }




            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error fetching menu items: {ex.Message}");
                // Depending on your needs, you might return an empty list or rethrow the exception
            }
            return litem;
        }
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
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


        public ActionResult get_dasbhaord_emp_records()
        {

            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            string query = @"Bind_Dashboard_Data";
            DataTable table = new DataTable();

            var con = Dashbaorddata.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
          
                da.Fill(table);
            }
            Dashboarddata litem = new Dashboarddata();
            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    

                    litem.AllEmployee = table.Rows[i]["TotalAllemployee"].ToString();
                    litem.TotalPrejoining = table.Rows[i]["TotalPrejoining"].ToString();
                    litem.TotalResigned = table.Rows[i]["TotalResigned"].ToString();
                    litem.TotalActive = table.Rows[i]["TotalActive"].ToString();


                }

            }



            return Json(litem);



        }

        public ActionResult get_dasbhaord_emp_records_ongraph()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            List<Dashboarddata> dashlist = new List<Dashboarddata>();
            string query = @"Dashboard_table_details";
            DataTable table = new DataTable();

            var con = Dashbaorddata.getConnectionString();
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
                    Dashboarddata litem = new Dashboarddata();

                    litem.PendingEmailCreation = table.Rows[i]["Createemail"].ToString();
                    litem.PendingEmailBlock = table.Rows[i]["BlockEmail"].ToString();
                    litem.PendingEmailDelete = table.Rows[i]["Removeemail"].ToString();
                    litem.Months = table.Rows[i]["Months"].ToString();
                    litem.Pendingtransfer = table.Rows[i]["transfer"].ToString();
                    litem.TotalRequest = table.Rows[i]["Totalrequest"].ToString();
                    dashlist.Add(litem);
                }
            

            }



            return Json(dashlist);



        }

        // Controllers/DashboardController.cs

      
    

    public ActionResult get_dasbhaord_common()
        {

             var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            string query = @"Common_Dashboard";
            DataTable table = new DataTable();

            var con = Dashbaorddata.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ecode", HttpContext.Session.GetString("Empcode"));
                da.Fill(table);
            }
            Dashboarddata litem = new Dashboarddata();
            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {


                    litem.PendingEmailCreation = table.Rows[i]["PendingEmailCreation"].ToString() ;
                    litem.PendingEmailBlock = table.Rows[i]["PendingEmailBlock"].ToString();
                    litem.PendingEmailDelete = table.Rows[i]["PendingEmailDelete"].ToString();
                  
                    litem.TotalAction = table.Rows[i]["TotalAction"].ToString();


                }

            }

            return View(litem);

            



        }
    }
}
