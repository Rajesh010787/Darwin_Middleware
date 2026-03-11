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
    public class Emp_profile_completation_Controller : Controller
    {
        // GET: Emp_profile_completation_Controller
     
        DB_DataAccess_Layer Dashbaorddata;
        public const string SessionKeyName = "_Name";

        public Emp_profile_completation_Controller(IConfiguration configuration) 
        {
            Dashbaorddata = new DB_DataAccess_Layer(configuration);
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Emp_profile_completation_Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Emp_profile_completation_Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emp_profile_completation_Controller/Create
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

        // GET: Emp_profile_completation_Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Emp_profile_completation_Controller/Edit/5
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

        // GET: Emp_profile_completation_Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Emp_profile_completation_Controller/Delete/5
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

        public ActionResult   get_emp_completion_report()
        {
            return View();

        }
        [HttpGet("GetPlantWiseDetailsExport")]
        public IActionResult GetPlantWiseDetailsExport(string plant)
        {
            try
            {
                List<object> employees = new List<object>();

                using (SqlConnection con = new SqlConnection(Dashbaorddata.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetEmployeeProfileCompletion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Department_cost_center_name", plant);
                        cmd.Parameters.AddWithValue("@username", HttpContext.Session.GetString("Sessionusername"));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow r in dt.Rows)
                        {
                            employees.Add(new
                            {
                                // BASIC DETAILS
                                EmployeeId = r["Employee Id"].ToString(),
                                EmployeeName = r["Full Name"].ToString(),
                                //EmploymentStatus = r["Employment Status"].ToString(),
                                Gender = r["gender"].ToString(),
                                getentry = r["gentry_route"].ToString(),
                                GroupCompany = r["group_company"].ToString(),
                                BusinessUnit = r["business_unit"].ToString(),
                                DepartmentCostCenterName = r["department_cost_center_name"].ToString(),
                                FunctionalAreaDept = r["functional_area_dept"].ToString(),
                                FunctionalAreaSubDept = r["functional_area_subdept"].ToString(),
                                DepartmentName = r["department_name"].ToString(),

                                JobLevel = r["job_level"].ToString(),
                                LevelGrade = r["Level_Grade"].ToString(),
                                EmployeeType = r["employee_type"].ToString(),

                                DirectManagerId = r["direct_manager_employee_id"].ToString(),
                                DirectManagerName = r["direct_manager_name"].ToString(),

                                HODId = r["hod_employee_id"].ToString(),
                                HODName = r["hod"].ToString(),

                                FatherName = r["father_name"].ToString(),
                                MaritalStatus = r["marital_status"].ToString(),

                                // CONTACT
                                CompanyEmail = r["company_email_id"].ToString(),
                                OfficeMobile = r["office_mobile_no"].ToString(),
                                PersonalEmail = r["personal_email_id"].ToString(),
                                PersonalMobile = r["personal_mobile_no"].ToString(),
                                EmergencyContact = r["emergency_contact_number"].ToString(),
                                EmergencyPerson = r["emergency_contact_person"].ToString(),

                                // ADDRESS
                                PermanentAddress = r["permanent_address"].ToString(),
                                CurrentAddress = r["current_address"].ToString(),

                                // BANK + PF
                                PFNumber = r["pf_number"].ToString(),
                                UANNumber = r["uan_number"].ToString(),
                                BloodGroup = r["blood_group"].ToString(),
                                BankName = r["bank_name"].ToString(),
                                BankBranch = r["bank_branch"].ToString(),
                                BankIFSC = r["bank_ifsc"].ToString(),
                                BankAccount = r["bank_account"].ToString(),

                                // DATES
                                DateOfJoining = r["date_of_joining"].ToString(),
                                GroupDateOfJoining = r["group_date_of_joining"].ToString(),

                                // EDUCATION
                                InstitutionName = GetValue(r, "institution_name"),
                                LevelOfStudy = GetValue(r, "level_of_study"),
                                FieldOfStudy = GetValue(r, "field_of_study"),
                                University = GetValue(r, "university"),
                                GPAPercentage = GetValue(r, "gpa_percentage"),

                                // WORK EXPERIENCE
                                WorkCompany = GetValue(r, "work_company"),
                                WorkTitle = GetValue(r, "work_title"),
                                WorkLocation = GetValue(r, "work_location"),
                                WorkFromDate = GetValue(r, "work_from_date"),
                                WorkToDate = GetValue(r, "work_to_date"),

                                // COMPLETION
                                BasicPercent = Convert.ToDecimal(r["Basic_Info_Completion_%"]),
                                EducationPercent = Convert.ToDecimal(r["Education_Completion_%"]),
                                WorkPercent = Convert.ToDecimal(r["Work_Completion_%"]),
                                Overall = Convert.ToDecimal(r["Overall_Profile_Completion_%"])
                            });
                        }
                    }
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        private string GetValue(DataRow r, string column)
        {
            return r.Table.Columns.Contains(column) ? r[column]?.ToString() : "";
        }
        [HttpGet("Bind_Plant")]
        public IActionResult Bind_Plant()
        {
           
            List<Dropdown_Model<string>> Plantlist = new List<Dropdown_Model<string>>();

            string query = @"Bind_Plant_Master";
            DataTable table = new DataTable();

            var con = Dashbaorddata.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
   
                cmd.Parameters.AddWithValue("@useremail", HttpContext.Session.GetString("Sessionusername"));

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Dropdown_Model<string> litem = new Dropdown_Model<string>();

                    litem.value = table.Rows[i]["Plant"].ToString();   // ✔ correct (lowercase)
                    litem.name = table.Rows[i]["Plant"].ToString();  // ✔ correct (lowercase)

                    Plantlist.Add(litem);
                }

            }


            return Json(Plantlist);

        }
        

        [HttpGet("GetPlantWiseDetails")]
        public IActionResult GetPlantWiseDetails(string plant)
        {
            try
            {
                List<object> employees = new List<object>();

                using (SqlConnection con = new SqlConnection(Dashbaorddata.getConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetEmployeeProfileCompletion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Department_cost_center_name", plant);
                        cmd.Parameters.AddWithValue("@username", HttpContext.Session.GetString("Sessionusername"));
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        con.Open();
                        da.Fill(dt);
                        con.Close();

                        foreach (DataRow r in dt.Rows)
                        {
                            employees.Add(new
                            {
                                // BASIC DETAILS
                                EmployeeId = r["Employee Id"].ToString(),
                                EmployeeName = r["Full Name"].ToString(),
                                //EmploymentStatus = r["Employment Status"].ToString(),
                                Gender = r["gender"].ToString(),

                                GroupCompany = r["group_company"].ToString(),
                                BusinessUnit = r["business_unit"].ToString(),
                                DepartmentCostCenterName = r["department_cost_center_name"].ToString(),
                                FunctionalAreaDept = r["functional_area_dept"].ToString(),
                                FunctionalAreaSubDept = r["functional_area_subdept"].ToString(),
                                DepartmentName = r["department_name"].ToString(),

                                JobLevel = r["job_level"].ToString(),
                                LevelGrade = r["Level_Grade"].ToString(),
                                EmployeeType = r["employee_type"].ToString(),

                                DirectManagerId = r["direct_manager_employee_id"].ToString(),
                                DirectManagerName = r["direct_manager_name"].ToString(),

                                HODId = r["hod_employee_id"].ToString(),
                                HODName = r["hod"].ToString(),

                                FatherName = r["father_name"].ToString(),
                                MaritalStatus = r["marital_status"].ToString(),

                                CompanyEmail = r["company_email_id"].ToString(),
                                OfficeMobile = r["office_mobile_no"].ToString(),
                                PersonalEmail = r["personal_email_id"].ToString(),
                                PersonalMobile = r["personal_mobile_no"].ToString(),
                                EmergencyContact = r["emergency_contact_number"].ToString(),
                                EmergencyPerson = r["emergency_contact_person"].ToString(),

                                PermanentAddress = r["permanent_address"].ToString(),
                                CurrentAddress = r["current_address"].ToString(),

                                PFNumber = r["pf_number"].ToString(),
                                UANNumber = r["uan_number"].ToString(),
                                BloodGroup = r["blood_group"].ToString(),

                                BankName = r["bank_name"].ToString(),
                                BankBranch = r["bank_branch"].ToString(),
                                BankIFSC = r["bank_ifsc"].ToString(),
                                BankAccount = r["bank_account"].ToString(),

                                DateOfJoining = r["date_of_joining"].ToString(),
                                GroupDateOfJoining = r["group_date_of_joining"].ToString(),

                                // COMPLETION %
                                BasicPercent = Convert.ToDecimal(r["Basic_Info_Completion_%"]),
                                EducationPercent = Convert.ToDecimal(r["Education_Completion_%"]),
                                WorkPercent = Convert.ToDecimal(r["Work_Completion_%"]),
                                Overall = Convert.ToDecimal(r["Overall_Profile_Completion_%"]),


                                  // ===============================
                                  // EDUCATION DETAILS
                                  // ===============================
                                InstitutionName = r["institution_name"].ToString(),
                                LevelOfStudy = r["level_of_study"].ToString(),
                                FieldOfStudy = r["field_of_study"].ToString(),
                                EducationCategory = r["education_category"].ToString(),
                                GPAPercentage = r["gpa_percentage"].ToString(),
                                University = dt.Columns.Contains("university") ? r["university"].ToString() : "",

                                // ===============================
                                // WORK EXPERIENCE DETAILS
                                // ===============================
                                WorkCompany = r["work_company"].ToString(),
                                WorkTitle = r["work_title"].ToString(),
                                WorkLocation = r["work_location"].ToString(),
                                WorkFromDate = r["work_from_date"].ToString(),
                                WorkToDate = r["work_to_date"].ToString()
                            });
                        }
                    }
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Optional: Log error here (Serilog, NLog, Console, etc.)
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while fetching plant-wise employee details.",
                    error = ex.Message
                });
            }
        }


        [HttpGet]
        public IActionResult GetChartDataPlant(string plants)
        {
            try
            {
                var sessionUser = HttpContext.Session.GetString("Sessionusername");
                if (string.IsNullOrEmpty(sessionUser))
                    return RedirectToAction("User_Login", "Inter");

                var plantData = new List<object>();
                decimal grandBasic = 0, grandEducation = 0, grandWork = 0, grandOverall = 0;
                int deptCount = 0;
                int Totalemployee=0;
                using (var connection = new SqlConnection(Dashbaorddata.getConnectionString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand("sp_GetEmployeeProfileCompletion_Group", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PlantList", plants);
                        command.Parameters.AddWithValue("@Username", sessionUser);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var dept = reader["department_cost_center_name"]?.ToString() ?? "";
                               
                                decimal basic = 0, edu = 0, work = 0,  overall = 0;
                                decimal.TryParse(reader["Avg_Basic"]?.ToString(), out basic);
                                decimal.TryParse(reader["Avg_Education"]?.ToString(), out edu);
                                decimal.TryParse(reader["Avg_Work"]?.ToString(), out work);
                                decimal.TryParse(reader["Overall_Avg_Profile_Completion_Percent"]?.ToString(), out overall);

                                // Accumulate for grand averages
                                Totalemployee =Convert.ToInt32( reader["Grand_Total_Employees"]);
                                grandBasic += basic;
                                grandEducation += edu;
                                grandWork += work;
                                grandOverall += overall;
                                deptCount++;

                                // Dynamic color based on overall completion
                                var color = overall >= 100 ? "#28A745" : "#D32F2F";

                                // Tooltip summary
                                var summary = reader["Completion_Summary"]?.ToString() ?? "";

                                plantData.Add(new
                                {
                                    name = dept,
                                    y = overall,
                                    color = color,
                                    summary = summary
                                });
                            }
                        }
                    }
                }

                // Calculate Grand Averages
                grandBasic = deptCount > 0 ? Math.Round(grandBasic / deptCount, 2) : 0;
                grandEducation = deptCount > 0 ? Math.Round(grandEducation / deptCount, 2) : 0;
                grandWork = deptCount > 0 ? Math.Round(grandWork / deptCount, 2) : 0;
                grandOverall = deptCount > 0 ? Math.Round(grandOverall / deptCount, 2) : 0;
               
                var result = new
                {
                    series = new[]
                    {
                new
                {
                    name = "Overall Profile Completion (%)",
                    data = plantData
                }
            },
                    Grand_Basic = grandBasic,
                    Grand_Education = grandEducation,
                    Grand_Work = grandWork,
                    Grand_Overall = grandOverall,
                    Totalemp = Totalemployee,
            };

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine("Error in GetChartDataPlant: " + ex.Message);
                return StatusCode(500, "Internal server error while fetching chart data.");
            }
        }




    }
}


