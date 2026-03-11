using DB_Middleware.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Middleware.Controllers
{
    public class HR_Head_ReportController : Controller
    {
        DB_DataAccess_Layer GetHrREPORTMasterdadadetails;
        // GET: Email_RequestController
        public HR_Head_ReportController(IConfiguration configuration)
        {
            GetHrREPORTMasterdadadetails = new DB_DataAccess_Layer(configuration);
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: HR_Head_ReportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HR_Head_ReportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HR_Head_ReportController/Create
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

        // GET: HR_Head_ReportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HR_Head_ReportController/Edit/5
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

        // GET: HR_Head_ReportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        public ActionResult HR_Head_Report()
        {
            return View();
        }
        
        // POST: HR_Head_ReportController/Delete/5
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
        //[HttpGet]
        //public IActionResult ExportAjaySirReportToExcel()
        //{
        //    var con = GetHrREPORTMasterdadadetails.getConnectionString();
        //    using SqlConnection connection = new SqlConnection(con);
        //    using SqlCommand cmd = new SqlCommand("Get_Ajay_Sir_Report", connection)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };

        //    using SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);  // Fill DataTable directly from SP

        //    using var wb = new XLWorkbook();
        //    wb.Worksheets.Add(dt, "HR Report");

        //    using var stream = new MemoryStream();
        //    wb.SaveAs(stream);
        //    var content = stream.ToArray();

        //    return File(content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "HR_Head_Report.xlsx");
        //}

        public class getfilterparam
        {

            public int Draw { get; set; }
            public int Start { get; set; }
            public int Length { get; set; } 
            public string SearchText { get; set; }  // <-- Add this
            public string SortColumn { get; set; }  // Optional
            public string SortDirection { get; set; } // Optional

        }

        [HttpPost]
        public IActionResult GetAjaySirReport(getfilterparam model)
        {
            try
            {
                var con = GetHrREPORTMasterdadadetails.getConnectionString();

                using (SqlConnection connection = new SqlConnection(con))
                using (SqlCommand cmd = new SqlCommand("Get_Ajay_Sir_Report_1", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PageNumber", model.Start);
                    cmd.Parameters.AddWithValue("@PageSize", model.Length);
                    cmd.Parameters.AddWithValue("@SearchText", model.SearchText ?? string.Empty);
                    cmd.Parameters.AddWithValue("@SortColumn", model.SortColumn ?? string.Empty);
                    cmd.Parameters.AddWithValue("@SortDirection", model.SortDirection ?? string.Empty);

                    var totalRecordsParam = new SqlParameter("@TotalRecords", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(totalRecordsParam);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int totalRecords = totalRecordsParam.Value != DBNull.Value ? (int)totalRecordsParam.Value : 0;

                    // Map columns from DataTable to simple keys:
                    var data = dt.AsEnumerable().Select(row => new
                    {
                        RequisitionId = row.Field<object>("Requisition Id"),
                        GroupCompany = row.Field<object>("Group Company"),
                        DepartmentName = row.Field<object>("Department Name"),
                        BusinessUnitName = row.Field<object>("Business Unit Name"),
                        RequisitionRaisedByEmployeeId = row.Field<object>("Requisition Raised By - Employee Id"),
                        RequisitionRaisedByEmployeeName = row.Field<object>("Requisition Raised By - Employee Name"),
                        RequisitionStatus = row.Field<object>("Requisition Status"),
                        RequisitionArchivedBy = row.Field<object>("Requisition Archived By"),
                        RequisitionArchivedOn = row.Field<object>("Requisition Archived On"),
                        RequisitionInitiatedOn = row.Field<object>("Requisition Initiated On"),
                        RequisitionActivatedOn = row.Field<object>("Requisition Activated On"),
                        RequisitionActivatedBy = row.Field<object>("Requisition Activated By"),
                        RequisitionHiringManager = row.Field<object>("Requisition Hiring Manager"),
                        Level1Assignees = row.Field<object>("Level 1 Assignees"),
                        Level1Status = row.Field<object>("Level 1 Status"),
                        Level1ActionTakenBy = row.Field<object>("Level 1 Action Taken By"),
                        Level1ActionTakenOn = row.Field<object>("Level 1 Action Taken On"),
                        Level2Assignees = row.Field<object>("Level 2 Assignees"),
                        Level2Status = row.Field<object>("Level 2 Status"),
                        Level2ActionTakenBy = row.Field<object>("Level 2 Action Taken By"),
                        Level2ActionTakenOn = row.Field<object>("Level 2 Action Taken On"),
                        Level3Assignees = row.Field<object>("Level 3 Assignees"),
                        Level3Status = row.Field<object>("Level 3 Status"),
                        Level3ActionTakenBy = row.Field<object>("Level 3 Action Taken By"),
                        Level3ActionTakenOn = row.Field<object>("Level 3 Action Taken On"),
                        Level4Assignees = row.Field<object>("Level 4 Assignees"),
                        Level4Status = row.Field<object>("Level 4 Status"),
                        Level4ActionTakenBy = row.Field<object>("Level 4 Action Taken By"),
                        Level4ActionTakenOn = row.Field<object>("Level 4 Action Taken On"),
                        Level5Assignees = row.Field<object>("Level 5 Assignees"),
                        Level5Status = row.Field<object>("Level 5 Status"),
                        Level5ActionTakenBy = row.Field<object>("Level 5 Action Taken By"),
                        Level5ActionTakenOn = row.Field<object>("Level 5 Action Taken On"),
                        Level6Assignees = row.Field<object>("Level 6 Assignees"),
                        Level6Status = row.Field<object>("Level 6 Status"),
                        Level6ActionTakenBy = row.Field<object>("Level 6 Action Taken By"),
                        Level6ActionTakenOn = row.Field<object>("Level 6 Action Taken On"),
                        RequisitionLastApprovedRejectedOn = row.Field<object>("Requisition Last Approved/Rejected On"),
                        RequisitionLastApprovedRejectedBy = row.Field<object>("Requisition Last Approved/Rejected By"),
                        ReplacementEmployees = row.Field<object>("Replacement Employees"),
                        BudgetedNonBudgeted = row.Field<object>("Budgeted/Non Budgeted"),
                        VacancyDueTo = row.Field<object>("Vacancy Due To"),
                        InternalTalent = row.Field<object>("Internal Talent Can Anyone From Existing Team Take Up This Role If Yes Then Who"),
                        HeadcountOfDepartmentBudget = row.Field<object>("Headcount Of Department Budget"),
                        HeadcountOfDepartmentActual = row.Field<object>("Headcount Of Department Actual"),
                        IfUnBudgetedReason = row.Field<object>("If Un Budgeted Reason"),
                        LevelAndDesignation = row.Field<object>("Level And Designation"),
                        ExperienceRange = row.Field<object>("Experience Range From To"),
                        JustificationForHiring = row.Field<object>("Justification For Hiring Why Not Internally Associate To Staff Or Clubbing Of Roles"),
                        OfferSection = "--",
                        CandidateName = row.Field<object>("Candidate Name"),
                        DateOfApplication = row.Field<object>("Date Of Application"),
                        CandidateSourceId = row.Field<object>("Candidate Source Id"),
                        OfferEmployeeType = row.Field<object>("Offer Employee Type"),
                        GeneratedBy = row.Field<object>("Generated By"),
                        GeneratedOn = row.Field<object>("Generated On"),
                        OfferSalaryStructureName = row.Field<object>("Offer Salary Structure Name"),
                        DateOfJoiningAsPerOffer = row.Field<object>("Date Of Joining As Per Offer"),
                        DateOfJoiningAsPerProfile = row.Field<object>("Date Of Joining As Per Profile"),
                        EmployeeId = row.Field<object>("Employee Id"),
                        OfferSentOn = row.Field<object>("Offer Sent On"),
                        OfferSentBy = row.Field<object>("Offer Sent By"),
                        OfferAcceptedOn = row.Field<object>("Offer Accepted On"),
                        OfferAcceptedBy = row.Field<object>("Offer Accepted By"),
                        CandidateCommentsInOfferLetter = row.Field<object>("Candidate Comments In Offer Letter"),
                        OfferRejectedOn = row.Field<object>("Offer Rejected On"),
                        OfferRejectedBy = row.Field<object>("Offer Rejected By"),
                        OfferStatus = row.Field<object>("Offer Status"),
                        OfferWithdrawnOn = row.Field<object>("Offer Withdrawn On"),
                        OfferWithdrawnBy = row.Field<object>("Offer Withdrawn By"),
                        ExistingLevelAndDesignation = row.Field<object>("Existing Level And Designation"),
                        RevisedLevelAndDesignation = row.Field<object>("Revised Level And Designation"),
                        ExistingCostCentre = row.Field<object>("Existing Cost Centre"),
                        RevisedCostCentre = row.Field<object>("Revised Cost Centre"),
                        Level1AssigneesRepeat = row.Field<object>("Level 1 Assignees"),
                        Level1StatusRepeat = row.Field<object>("Level 1 Status"),
                        Level1ActionTakenByRepeat = row.Field<object>("Level 1 Action Taken By"),
                        Level1ActionTakenOnRepeat = row.Field<object>("Level 1 Action Taken On"),
                        Level2AssigneesRepeat = row.Field<object>("Level 2 Assignees"),
                        Level2StatusRepeat = row.Field<object>("Level 2 Status"),
                        Level2ActionTakenByRepeat = row.Field<object>("Level 2 Action Taken By"),
                        Level2ActionTakenOnRepeat = row.Field<object>("Level 2 Action Taken On")
                    }).ToList();

                    var result = new
                    {
                        draw = model.Draw,
                        recordsTotal = totalRecords,
                        recordsFiltered = totalRecords,
                        data = data
                    };

                    var json = JsonConvert.SerializeObject(result);
                    return Content(json, "application/json");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }



    }
}
