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
    public class VAAW_Job_PostingController : Controller
    {
        DB_DataAccess_Layer JobPosting_Connection; 
        public IActionResult Index()
        {
            return View();
        }
        public VAAW_Job_PostingController(IConfiguration configuration)
        {
            JobPosting_Connection = new DB_DataAccess_Layer(configuration);
        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobController/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Bind_Jobposting_On_VAAVE() 
        {
            return View();
        }
        
        // POST: JobController/Create
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

        // GET: JobController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobController/Edit/5
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

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobController/Delete/5
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


        [HttpPost]
        public IActionResult Saveandchangestatus([FromBody] JobStatusModel model)
        {
           
            // Your logic to save the status using model.UserUniqueID and model.IsChecked
            // Return an appropriate response

            string query = @"Update_Job_Status";
            DataTable table = new DataTable();

            var con = JobPosting_Connection.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string a = HttpContext.Session.GetString("Sessionusername");
                cmd.Parameters.AddWithValue("@Updateby", HttpContext.Session.GetString("Empcode"));
                cmd.Parameters.AddWithValue("@Uniqueid", model.UserUniqueID.ToString());
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
        public ActionResult get_job_details()
        {


            List<jobdetails> PendingEmpdetails = new List<jobdetails>();

            string query = @"Get_Job_Details";
            DataTable table = new DataTable();

            var con = JobPosting_Connection.getConnectionString();
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
                    jobdetails litem = new jobdetails();

                    litem.business_unit = table.Rows[i]["business_unit"].ToString();
                    litem.department = table.Rows[i]["department"].ToString();
                    litem.experience_from = table.Rows[i]["experience_from"].ToString();
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.experience_to = table.Rows[i]["experience_to"].ToString();
                    litem.group_company = table.Rows[i]["group_company"].ToString();
                    litem.Joblocation = table.Rows[i]["Joblocation"].ToString();
                    litem.job_title = table.Rows[i]["job_title"].ToString();
                    litem.job_id = table.Rows[i]["job_id"].ToString();
                    litem.job_created_timestamp = table.Rows[i]["job_created_timestamp"].ToString();
                    litem.Status =Convert.ToInt32(table.Rows[i]["Status"].ToString());
                    
                    litem.Url = table.Rows[i]["URL"].ToString();
                  


                    PendingEmpdetails.Add(litem);
                }

            }
            else { jobdetails litem = new jobdetails(); PendingEmpdetails.Add(litem); }



            return Json(PendingEmpdetails);



        }


        //-----------------------------------Generate Email Id request -----------------------------------function----
    }
}
