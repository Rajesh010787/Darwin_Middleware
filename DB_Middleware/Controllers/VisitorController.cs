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
using System.Threading.Tasks;

namespace DB_Middleware.Controllers
{
    public class VisitorController : Controller
    {
        DB_DataAccess_Layer Active_Employee_Visitors;
        public IActionResult Index()
        {
            return View();
        }
        public VisitorController(IConfiguration configuration)
        {
            Active_Employee_Visitors = new DB_DataAccess_Layer(configuration);
        }

        public ActionResult Bind_Visitor_Details()
        {

            return View();
        }
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
        [HttpPost]
        public IActionResult SaveEmployeeStatus([FromBody] EmployeeStatusModel model)
        {
            string filterid = Decode(model.UserUniqueID.ToString());
            // Your logic to save the status using model.UserUniqueID and model.IsChecked
            // Return an appropriate response

            string query = @"Update_Visitor_Status";
            DataTable table = new DataTable();

            var con = Active_Employee_Visitors.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string a = HttpContext.Session.GetString("Sessionusername");
                cmd.Parameters.AddWithValue("@Updateby", HttpContext.Session.GetString("Empcode"));
                cmd.Parameters.AddWithValue("@Uniqueid", filterid);
                cmd.Parameters.AddWithValue("@Status", model.IsChecked);
                da.Fill(table);
            }
            Response R = new Response();
            if (table.Rows.Count > 0)
            {
                R.message = table.Rows[0]["Message"].ToString();
            }

            return Json(new { message = R.message });
        }


        //-------------------------------End Rep Emp Details----------------------------------------
        public ActionResult get_pending_employee_emailid_creation(string UniqueID)
        {


            List<Employeedetails> PendingEmpdetails = new List<Employeedetails>();

            string query = @"Bind_Employee_For_Visitors";
            DataTable table = new DataTable();

            var con = Active_Employee_Visitors.getConnectionString();
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
                    litem.functional_area_dept = table.Rows[i]["functional_area_dept"].ToString();
                    litem.functional_area_subdept = table.Rows[i]["functional_area_subdept"].ToString();
                    litem.Level_Grade = table.Rows[i]["Level_Grade"].ToString();
                    litem.UserUniqueID = Encode(table.Rows[i]["UserUniqueID"].ToString());
                    litem.marital_status = table.Rows[i]["marital_status"].ToString();
                    litem.anniversary_date = table.Rows[i]["anniversary_date"].ToString();
                    litem.office_location = table.Rows[i]["office_location"].ToString();
                    litem.date_of_joining = table.Rows[i]["date_of_joining"].ToString();
                    litem.Visitor_Status = table.Rows[i]["VStatus"].ToString();
                    
                    litem.employee_status = table.Rows[i]["employee_status"].ToString();
                    litem.hrbp_name = table.Rows[i]["hrbp_name"].ToString();
                    litem.direct_manager_name = table.Rows[i]["direct_manager_name"].ToString();


                    PendingEmpdetails.Add(litem);
                }

            }
            else { Employeedetails litem = new Employeedetails(); PendingEmpdetails.Add(litem); }



            return Json(PendingEmpdetails);



        }


        //-----------------------------------Generate Email Id request -----------------------------------function----

        //----------------------------------End Generate Email ID Request ---------------------------------------------

    }
}
