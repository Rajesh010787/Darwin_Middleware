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
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;

namespace DB_Middleware.Controllers
{
    public class LoanController : Controller
    {
        // GET: LoanController

        public ActionResult Index()
        {

            return View();
        }
        DB_DataAccess_Layer Getloan;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //   private readonly SalaryProtector _salaryProtector;
        // GET: Email_RequestController
        public LoanController(IConfiguration configuration, IWebHostEnvironment env)
        {
            Getloan = new DB_DataAccess_Layer(configuration);
            _webHostEnvironment = env;
            //_salaryProtector = salaryProtector;
        }

        //public string PrintLoan(string id)
        //{
        //    DataTable dt = GetLoanPrintDetails(id);

        //    if (dt.Rows.Count == 0)
        //        return "";

        //    return GetLoanApplicationHtml(dt.Rows[0]);
        //}
        public DataTable GetLoanPrintDetails(string id)
        {
            DataTable dt = new DataTable();
            string conStr = Getloan.getConnectionString();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Loan_Print_Details", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public IActionResult PrintLoan(string loanId)
        {
            DataTable dt = GetLoanPrintDetails(loanId);

            if (dt.Rows.Count == 0)
            {
                return Content("No Data Found");
            }

            string html = Emailtext.GetLoanApplicationHtml(dt.Rows[0]);

            return Content(html, "text/html");
        }

        public ActionResult Initiated_Loan()
        {
            if (HttpContext.Session.GetString("Loanstatus") == "Completed 6 Months")
            {

                
            }
            else
            {
                return RedirectToAction("Notification", "Loan");

            }
            return View();
        }
        public ActionResult Notification()
        {
            return View();
        }
        
        public ActionResult Validate_By_HR_PADepart()
        {
            return View();
        }
        public ActionResult Validate_By_Deviation()
        {
            return View();
        }
        public ActionResult Approved_Loan_Request()
        {
            return View();
        }
        
        public ActionResult Validate_Final_Approval()
        {
            return View();
        }
        public ActionResult Approval_Pending()
        {
            return View();
        }
        public ActionResult Validate_By_GCO_HR()
        {
            return View();
        }
        
        public ActionResult Ack_Loan()
        {
            return View();
        }
        public ActionResult Finance_Release()
        {
            return View();
        }
        public ActionResult Load_Request_Details()
        {
            return View();
        }
        public ActionResult Validate_By_Approver()
        {
            return View();
        }

        // GET: LoanController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public IActionResult DownloadPdf(string loanId)
        {
            var pdfBytes = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("APPLICATION FOR LOAN / ADVANCE")
                        .SemiBold().FontSize(16).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Column(col =>
                        {
                            col.Spacing(5);

                            col.Item().Text($"Loan ID: {loanId}").FontSize(14).Bold();
                            col.Item().Text($"Employee Name: rajesh).FontSize(12)");

                            col.Item().Text("Acknowledgment: I acknowledge the receipt of loan.").FontSize(12);

                            col.Item().Text("Signature: __________________________").FontSize(12);
                            col.Item().Text($"Date: {System.DateTime.Now:dd-MM-yyyy}").FontSize(12);
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text("CSG-HR-324 REV.00 DATE:01-04-2010")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken1);
                });
            })
             .GeneratePdf(); // ✅ returns byte[] of PDF

            // 2️⃣ Return PDF file to browser
            return File(pdfBytes, "application/pdf", $"Loan_{loanId}.pdf");

        }
        // GET: LoanController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanController/Create
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

        // GET: LoanController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoanController/Edit/5
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

        // GET: LoanController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoanController/Delete/5
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


        public ActionResult Bind_Garanted_Person(string Ecode)

        {

            List<Master_Data> BindGarantedData = new List<Master_Data>();

            string query = @"Bind_Garanted_Person";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ecode", Ecode);

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Master_Data litem = new Master_Data();

                    litem.gpersonname = table.Rows[i]["Fullname"].ToString();
                    litem.gperson_Ecode = table.Rows[i]["employee_id"].ToString();

                    BindGarantedData.Add(litem);
                }

            }


            return Json(BindGarantedData);


        }
        public ActionResult Bind_Purpose()

        {

            List<Master_Data> BindPurposeData = new List<Master_Data>(); 

            string query = @"Bind_Purpose";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();
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
                    Master_Data litem = new Master_Data();

                    litem.purpose = table.Rows[i]["purpose_name"].ToString();


                    BindPurposeData.Add(litem);
                }

            }


            return Json(BindPurposeData);


        }

        [HttpPost]
        public ActionResult Validate_Loan_Request([FromBody] loandata objdata)
        {
            Response returnresponse = new Response();

            try
            {
                string query = @"Validate_Loan";
                DataTable table = new DataTable();

                var con = Getloan.getConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objdata.id);
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@ActionStatus", objdata.CStatus);
                    cmd.Parameters.AddWithValue("@ActionBy", HttpContext.Session.GetString("Empcode"));

                    da.Fill(table);
                }

                // If SP returns data (success case)
                // mail function call
                string mailBody = string.Empty;
                string subject = string.Empty;

                string currentStatus = table.Rows[0]["CurrentStatus"].ToString();
                string loanId = table.Rows[0]["Loanid"].ToString();
                string createdBy = table.Rows[0]["Createdby"].ToString();
                string sessionFullName = HttpContext.Session.GetString("Sessionfullname");

                // Approved
                if (objdata.CStatus == "Approved" &&
                    currentStatus != "Acknowledged and closed" &&
                    currentStatus != "Amount Release By Finance")
                {


                    subject = "Loan Request Approved";
                    if (currentStatus == "Approved By Guaranteed 2")
                    {
                        mailBody = Emailtext.whileapprovedbyG2(table.Rows[0]["Pending_Full_Name"].ToString(),






                      table.Rows[0]["Createdby"].ToString(),

                      table.Rows[0]["Pendingemail"].ToString(),



                       table.Rows[0]["Guaranteed1"].ToString(),

                      table.Rows[0]["Guaranteed2"].ToString()
                      );

                    }

                    else
                    {
                        mailBody = Emailtext.MailApprovedbyG1(table.Rows[0]["Pending_Full_Name"].ToString(),





                        table.Rows[0]["Createdby"].ToString(),

                        table.Rows[0]["Pendingemail"].ToString()
                    );
                    }
                }
                // Sent Back
                else if (objdata.CStatus == "SentBack" &&
                         currentStatus != "Acknowledged and closed" &&
                         currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Sent Back";
                    mailBody = Emailtext.GetLoanSentBackMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
                // Rejected
                else if (objdata.CStatus == "Rejected")
                {
                    subject = "Loan Request Rejected";
                    mailBody = Emailtext.GetLoanRejectedMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
              
                // Amount released
                else if (currentStatus == "Amount Release By Finance")
                {
                    subject = "Loan Amount Released";
                    mailBody = Emailtext.GetAmountReleasedMail(
                        loanId,
                        createdBy
                    );
                }

                string sentemail = Emailtext.SendMail(table.Rows[0]["Toemail"].ToString(), subject, mailBody, HttpContext.Session.GetString("Sessionusername"), table.Rows[0]["Bccemail"].ToString());


                returnresponse.Flag = "1";
                returnresponse.message = "Action completed successfully";



                return Json(returnresponse);
            }
            catch (SqlException ex)   // 🔴 IMPORTANT
            {
                returnresponse.Flag = "0";
                returnresponse.message = ex.Message;   // shows THROW message
                return Json(returnresponse);
            }
            catch (Exception ex)
            {
                returnresponse.Flag = "0";
                returnresponse.message = "Unexpected error occurred";
                return Json(returnresponse);
            }
        }

        public ActionResult Bind_MasterDroupdown(string typeid, string Business_Unit, string CostCenter, string SapCostCenter, string Profit_Center)
        {
            List<Dropdown_Model<string>> Bomlistdetails = new List<Dropdown_Model<string>>();

            string query = @"Bind_Cost_Center_Name";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Input", typeid);
                cmd.Parameters.AddWithValue("@Business_Unit", Business_Unit);
                cmd.Parameters.AddWithValue("@CostCenter", CostCenter);
                cmd.Parameters.AddWithValue("@SapCostCenter", SapCostCenter);
                cmd.Parameters.AddWithValue("@Profit_Center", Profit_Center);
         
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Dropdown_Model<string> litem = new Dropdown_Model<string>();
                    litem.value = Convert.ToString(table.Rows[i]["GetValue"].ToString());
                    litem.name = table.Rows[i]["GText"].ToString();

                    Bomlistdetails.Add(litem);
                }

            }


            return Json(Bomlistdetails);

        }

        [HttpPost]
        public ActionResult Validate_Finance_Release_Request([FromBody] loandata objdata)
        {
            Response returnresponse = new Response();

            try
            {
                string query = @"Release_By_Finance";
                DataTable table = new DataTable();

                var con = Getloan.getConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objdata.id);
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@ActionStatus", objdata.CStatus);
                    cmd.Parameters.AddWithValue("@ActionBy", HttpContext.Session.GetString("Empcode"));

                    da.Fill(table);
                }

                // If SP returns data (success case)
                // mail function call
                string mailBody = string.Empty;
                string subject = string.Empty;

               




                string currentStatus = table.Rows[0]["CurrentStatus"].ToString();
                string loanId = table.Rows[0]["Loanid"].ToString();
                string createdBy = table.Rows[0]["Createdby"].ToString();
                string Plant_Vertical = table.Rows[0]["CostCenter"].ToString();
                string CostCenter = table.Rows[0]["GCompany"].ToString();
                string DisbursementDate = table.Rows[0]["Disbursement_Date"].ToString();
                string Pending_Full_Name = table.Rows[0]["Pending_Full_Name"].ToString();
                string Toemail = table.Rows[0]["Pendingemail"].ToString();
                string Amountrelease = table.Rows[0]["Finalamount"].ToString();
                string sessionFullName = HttpContext.Session.GetString("Sessionfullname");
                string ccemail = HttpContext.Session.GetString("SessionusernameEmail");
                
                // Approved


                // Rejected
                if (objdata.CStatus == "Rejected")
                {
                    subject = "Loan Request Rejected";
                    mailBody = Emailtext.GetLoanRejectedMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
               
                // Amount released
                else if (currentStatus == "Amount Release By Finance")
                {
                    subject = "Loan Amount Successfully Disbursed by Finance Team";
                    mailBody = Emailtext.GetLoanreleaseamountMail(
                       loanId,
                       createdBy,
                       Plant_Vertical,
                       CostCenter,
                       DisbursementDate,
                       Toemail,
                       Pending_Full_Name,
                       Amountrelease,
                       ccemail

                   );
                }

                string sentemail = Emailtext.SendMail(table.Rows[0]["Toemail"].ToString(), subject, mailBody, HttpContext.Session.GetString("Sessionusername"), table.Rows[0]["Bccemail"].ToString());


                returnresponse.Flag = "1";
                returnresponse.message = "Action completed successfully";



                return Json(returnresponse);
            }
            catch (SqlException ex)   // 🔴 IMPORTANT
            {
                returnresponse.Flag = "0";
                returnresponse.message = ex.Message;   // shows THROW message
                return Json(returnresponse);
            }
            catch (Exception ex)
            {
                returnresponse.Flag = "0";
                returnresponse.message = "Unexpected error occurred";
                return Json(returnresponse);
            }
        }




        [HttpPost]
        public ActionResult Validate_Loan_Request_GCO_HR([FromBody] loandata objdata)
        {
            Response returnresponse = new Response();
            string gcofinalamount = EncryptionHelper.Encrypt(objdata.gcofinalamount);
            try
            {
                string query = @"Validate_GCO_Loan";
                DataTable table = new DataTable();

                var con = Getloan.getConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objdata.id);
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@ActionStatus", objdata.CStatus);
                    cmd.Parameters.AddWithValue("@bankname", objdata.bankname);
                    cmd.Parameters.AddWithValue("@ifsccode", objdata.ifsccode);
                    cmd.Parameters.AddWithValue("@bankaccountnumber", objdata.accountnumber);
                    cmd.Parameters.AddWithValue("@finalamount", gcofinalamount);
                    cmd.Parameters.AddWithValue("@instalment", objdata.gcoinstalment);
                    cmd.Parameters.AddWithValue("@pan", objdata.pan);
                    
                    cmd.Parameters.AddWithValue("@costcenter", objdata.costcenter);
                    cmd.Parameters.AddWithValue("@sapcostcenter", objdata.sapcostcenter);
                    cmd.Parameters.AddWithValue("@prfofitcenter", objdata.prfofitcenter);
                    cmd.Parameters.AddWithValue("@newcostcenter", objdata.newcostcenter);

                    cmd.Parameters.AddWithValue("@ActionBy", HttpContext.Session.GetString("Empcode"));

                    da.Fill(table);
                }

                // If SP returns data (success case)
                // mail function call
                string mailBody = string.Empty;
                string subject = string.Empty;

                string currentStatus = table.Rows[0]["CurrentStatus"].ToString();
                string loanId = table.Rows[0]["Loanid"].ToString();
                string createdBy = table.Rows[0]["Createdby"].ToString();
                string sessionFullName = HttpContext.Session.GetString("Sessionfullname");
                string Pendingemail = table.Rows[0]["Pendingemail"].ToString();
                string ActionBy = table.Rows[0]["ActionBy"].ToString();
                string Pending_Full_Name = table.Rows[0]["Pending_Full_Name"].ToString();
              

                // Approved
                if (objdata.CStatus == "Approved" &&
                    currentStatus != "Acknowledged and closed" &&
                    currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Approved";
                    mailBody = Emailtext.whileapprovedcentralpayrollteam(
                    createdBy,
                        Pending_Full_Name,
                        Pendingemail
                       
                    );
                }
                // Sent Back
                else if (objdata.CStatus == "SentBack" &&
                         currentStatus != "Acknowledged and closed" &&
                         currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Sent Back";
                    mailBody = Emailtext.GetLoanSentBackMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
                // Rejected
                else if (objdata.CStatus == "Rejected")
                {
                    subject = "Loan Request Rejected";
                    mailBody = Emailtext.GetLoanRejectedMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
           
                // Amount released
                else if (currentStatus == "Amount Release By Finance")
                {
                    subject = "Loan Amount Released";
                    mailBody = Emailtext.GetAmountReleasedMail(
                        loanId,
                        createdBy
                    );
                }

                string sentemail = Emailtext.SendMail(table.Rows[0]["Toemail"].ToString(), subject, mailBody, HttpContext.Session.GetString("Sessionusername"), table.Rows[0]["Bccemail"].ToString());


                returnresponse.Flag = "1";
                returnresponse.message = "Action completed successfully";



                return Json(returnresponse);
            }
            catch (SqlException ex)   // 🔴 IMPORTANT
            {
                returnresponse.Flag = "0";
                returnresponse.message = ex.Message;   // shows THROW message
                return Json(returnresponse);
            }
            catch (Exception ex)
            {
                returnresponse.Flag = "0";
                returnresponse.message = "Unexpected error occurred";
                return Json(returnresponse);
            }
        }


        [HttpPost]
        public ActionResult Validate_Loan_HR_PA_Depart([FromBody] loandata objdata)
        {

            Response returnresponse = new Response();
            string getsalarybasicvdahra = EncryptionHelper.Encrypt(objdata.salarybasicvdahra);
            string geteligibilityamount = EncryptionHelper.Encrypt(objdata.eligibilityamount);

            try
            {
                string query = @"Validate_Loan_HR_PA_Depart";
                DataTable table = new DataTable();

                var con = Getloan.getConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objdata.id);
                    cmd.Parameters.AddWithValue("@Salarybasicvdahra", getsalarybasicvdahra);
                    cmd.Parameters.AddWithValue("@Whetherconfirmedornot", objdata.whetherconfirmedornot);
                    cmd.Parameters.AddWithValue("@Eligibilityamount", geteligibilityamount);
                    cmd.Parameters.AddWithValue("@Noofinstallmentforreppayment", objdata.noofinstallmentforreppayment);
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@ActionStatus", objdata.CStatus);
                    cmd.Parameters.AddWithValue("@loanstatus", objdata.loanstatus);
                    cmd.Parameters.AddWithValue("@closeddate", objdata.closeddate);
                    cmd.Parameters.AddWithValue("@sapecode", objdata.sapecode);
                    cmd.Parameters.AddWithValue("@ActionBy", HttpContext.Session.GetString("Empcode"));

                    da.Fill(table);
                }

                // If SP returns data (success case)
                // mail function call
                string mailBody = string.Empty;
                string subject = string.Empty;

                string currentStatus = table.Rows[0]["CurrentStatus"].ToString();
                string loanId = table.Rows[0]["Loanid"].ToString();
                string createdBy = table.Rows[0]["Createdby"].ToString();
                string Plant_Vertical = table.Rows[0]["CostCenter"].ToString();
                string CostCenter = table.Rows[0]["GCompany"].ToString();
                string DisbursementDate = table.Rows[0]["Disbursement_Date"].ToString();
                string Pending_Full_Name = table.Rows[0]["Pending_Full_Name"].ToString();
                string Toemail = table.Rows[0]["Toemail"].ToString();
                string G1 = table.Rows[0]["Guaranteed1"].ToString();
                string G2 = table.Rows[0]["Guaranteed2"].ToString();
                string ActionBy = table.Rows[0]["ActionBy"].ToString(); 
                string sessionFullName = HttpContext.Session.GetString("Sessionfullname");
                string ccemail = HttpContext.Session.GetString("SessionusernameEmail");
                
                // Approved
                if (objdata.CStatus == "Approved" &&
                    currentStatus != "Acknowledged and closed" &&
                    currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Approved";
                    mailBody = Emailtext.whileapprovedbyspochr(
                     Toemail,  createdBy, Pending_Full_Name,G1,G2, ActionBy
                    );
                }
                // Sent Back
                else if (objdata.CStatus == "SentBack" &&
                         currentStatus != "Acknowledged and closed" &&
                         currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Sent Back";
                    mailBody = Emailtext.GetLoanSentBackMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
                // Rejected
                else if (objdata.CStatus == "Rejected")
                {
                    subject = "Loan Request Rejected";
                    mailBody = Emailtext.GetLoanRejectedMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
              

                string sentemail = Emailtext.SendMail(table.Rows[0]["Toemail"].ToString(), subject, mailBody, HttpContext.Session.GetString("Sessionusername"), table.Rows[0]["Bccemail"].ToString());


                returnresponse.Flag = "1";
                returnresponse.message = "Action completed successfully";



                return Json(returnresponse);
            }
            catch (SqlException ex)   // 🔴 IMPORTANT
            {
                returnresponse.Flag = "0";
                returnresponse.message = ex.Message;   // shows THROW message
                return Json(returnresponse);
            }
            catch (Exception ex)
            {
                returnresponse.Flag = "0";
                returnresponse.message = "Unexpected error occurred";
                return Json(returnresponse);
            }
        }


        
        [HttpPost]
        public ActionResult Validate_Loan_deviationsan([FromBody] loandata objdata)
        {

            Response returnresponse = new Response();
            string getdeviationsanctionedamount = EncryptionHelper.Encrypt(objdata.deviationsanctionedamount);
            string deviationfinalamount = EncryptionHelper.Encrypt(objdata.deviationfinalamount);

            try
            {
                string query = @"Validate_Loan_Deviation";
                DataTable table = new DataTable();

                var con = Getloan.getConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objdata.id);
                    cmd.Parameters.AddWithValue("@deviationsanctionedamount", getdeviationsanctionedamount);
                    cmd.Parameters.AddWithValue("@deviationnoofinstallmentforreppayment", objdata.deviationnoofinstallmentforreppayment);
                    
                           cmd.Parameters.AddWithValue("@deviationfinalamount", deviationfinalamount);
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@ActionStatus", objdata.CStatus);
                    cmd.Parameters.AddWithValue("@ActionBy", HttpContext.Session.GetString("Empcode"));

                    da.Fill(table);
                }

                // If SP returns data (success case)
                // mail function call
                string mailBody = string.Empty;
                string subject = string.Empty;

                string currentStatus = table.Rows[0]["CurrentStatus"].ToString();
                string loanId = table.Rows[0]["Loanid"].ToString();
                string createdBy = table.Rows[0]["Createdby"].ToString();
                string sessionFullName = HttpContext.Session.GetString("Sessionfullname");
                string Pendingemail = table.Rows[0]["Pendingemail"].ToString();
                string ActionBy = table.Rows[0]["ActionBy"].ToString();
                string Pending_Full_Name = table.Rows[0]["Pending_Full_Name"].ToString();


                // Approved
                if (objdata.CStatus == "Approved" &&
                    currentStatus != "Acknowledged and closed" &&
                    currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Approved";
                    mailBody = Emailtext.whileapprovedfinalaction(
                        createdBy,
                        Pending_Full_Name,
                        Pendingemail
                    );
                }
                // Sent Back
                else if (objdata.CStatus == "SentBack" &&
                         currentStatus != "Acknowledged and closed" &&
                         currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Sent Back";
                    mailBody = Emailtext.GetLoanSentBackMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
                // Rejected
                else if (objdata.CStatus == "Rejected")
                {
                    subject = "Loan Request Rejected";
                    mailBody = Emailtext.GetLoanRejectedMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
              


                string sentemail = Emailtext.SendMail(table.Rows[0]["Toemail"].ToString(), subject, mailBody, HttpContext.Session.GetString("Sessionusername"), table.Rows[0]["Bccemail"].ToString());

                returnresponse.Flag = "1";
                returnresponse.message = "Action completed successfully";



                return Json(returnresponse);
            }
            catch (SqlException ex)   // 🔴 IMPORTANT
            {
                returnresponse.Flag = "0";
                returnresponse.message = ex.Message;   // shows THROW message
                return Json(returnresponse);
            }
            catch (Exception ex)
            {
                returnresponse.Flag = "0";
                returnresponse.message = "Unexpected error occurred";
                return Json(returnresponse);
            }
        }



        [HttpPost]
        public ActionResult Validate_Loan_Validate_Loan_Final([FromBody] loandata objdata)
        {

            Response returnresponse = new Response();
            string getfinalsanctionedamount = EncryptionHelper.Encrypt(objdata.finalsanctionedamount);


            try
            {
                string query = @"Validate_Loan_Final";
                DataTable table = new DataTable();

                var con = Getloan.getConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objdata.id);
                    cmd.Parameters.AddWithValue("@getfinalsanctionedamount", getfinalsanctionedamount);
                    cmd.Parameters.AddWithValue("@finalnoofinstallmentforreppayment", objdata.finalnoofinstallmentforreppayment);
                    cmd.Parameters.AddWithValue("@finalamount", objdata.finalamount);
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@ActionStatus", objdata.CStatus);
                    cmd.Parameters.AddWithValue("@ActionBy", HttpContext.Session.GetString("Empcode"));

                    da.Fill(table);
                }

                // If SP returns data (success case)
                // mail function call
                string mailBody = string.Empty;
                string subject = string.Empty;

                string currentStatus = table.Rows[0]["CurrentStatus"].ToString();
                string loanId = table.Rows[0]["Loanid"].ToString();
                string createdBy = table.Rows[0]["Createdby"].ToString();
                string sessionFullName = HttpContext.Session.GetString("Sessionfullname");

             
                string Pending_Full_Name = table.Rows[0]["Pending_Full_Name"].ToString();
                string Toemail = table.Rows[0]["Toemail"].ToString();
                string G1 = table.Rows[0]["Guaranteed1"].ToString();
                string G2 = table.Rows[0]["Guaranteed2"].ToString();
                string hrtspoc = table.Rows[0]["HRSPOC"].ToString();
                string ActionBy = table.Rows[0]["ActionBy"].ToString();
                string Deviationcase = table.Rows[0]["Deviationcase"].ToString(); 
                
                // Approved
                if (objdata.CStatus == "Approved" &&
                    currentStatus != "Acknowledged and closed" &&
                    currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Approved";
                    mailBody = Emailtext.whileapprovedbyplanthrhead(
                        Toemail, createdBy, Pending_Full_Name, G1, G2, ActionBy, hrtspoc, Deviationcase
                    );
                }
                // Sent Back
                else if (objdata.CStatus == "SentBack" &&
                         currentStatus != "Acknowledged and closed" &&
                         currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Sent Back";
                    mailBody = Emailtext.GetLoanSentBackMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
                // Rejected
                else if (objdata.CStatus == "Rejected")
                {
                    subject = "Loan Request Rejected";
                    mailBody = Emailtext.GetLoanRejectedMail(
                        loanId,
                        createdBy,
                        sessionFullName,
                        objdata.Remark
                    );
                }
              

               
                string sentemail = Emailtext.SendMail(table.Rows[0]["Toemail"].ToString(), subject, mailBody, HttpContext.Session.GetString("Sessionusername"), table.Rows[0]["Bccemail"].ToString());


                returnresponse.Flag = "1";
                returnresponse.message = "Action completed successfully";



                return Json(returnresponse);
            }
            catch (SqlException ex)   // 🔴 IMPORTANT
            {
                returnresponse.Flag = "0";
                returnresponse.message = ex.Message;   // shows THROW message
                return Json(returnresponse);
            }
            catch (Exception ex)
            {
                returnresponse.Flag = "0";
                returnresponse.message = "Unexpected error occurred";
                return Json(returnresponse);
            }
        }



        [HttpPost]
        public ActionResult Validate_AKG([FromBody] loandata objdata)
        {

            Response returnresponse = new Response();
            string getreceipt_rs = EncryptionHelper.Encrypt(objdata.receipt_rs);


            try
            {
                string query = @"Validate_AkG";
                DataTable table = new DataTable();

                var con = Getloan.getConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objdata.id);
                    cmd.Parameters.AddWithValue("@getreceipt_rs", getreceipt_rs);
                    cmd.Parameters.AddWithValue("@cash_cheque_no", objdata.cash_cheque_no);
                    cmd.Parameters.Add("@amount_received_date", SqlDbType.Date, 20)
          .Value = string.IsNullOrWhiteSpace(objdata.amount_received_date)
               ? (object)DBNull.Value
               : objdata.amount_received_date;
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);
                    cmd.Parameters.AddWithValue("@ActionStatus", objdata.CStatus);
                    cmd.Parameters.AddWithValue("@ActionBy", HttpContext.Session.GetString("Empcode"));

                    da.Fill(table);
                }

                // If SP returns data (success case)
                // mail function call
                string mailBody = string.Empty;
                string subject = string.Empty;

                string currentStatus = table.Rows[0]["CurrentStatus"].ToString();
                string loanId = table.Rows[0]["Loanid"].ToString();
         
                string finalamount = table.Rows[0]["finalamount"].ToString();
                string Createdby = table.Rows[0]["Createdby"].ToString();
                string Pending_Full_Name = table.Rows[0]["Pending_Full_Name"].ToString();
                string Pending_Email = table.Rows[0]["Pending_Email"].ToString();
                string ActionBy = table.Rows[0]["ActionBy"].ToString();
                string sessionFullName = HttpContext.Session.GetString("Sessionfullname");
                string ccemail = HttpContext.Session.GetString("SessionusernameEmail");
                string amountreceiveddate = table.Rows[0]["amountreceiveddate"].ToString();
                
                // Approved
                if (objdata.CStatus == "Approved" &&
                    currentStatus != "Acknowledged and closed" &&
                    currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Approved";
                    mailBody = Emailtext.GetLoanClosedMail(
                        amountreceiveddate,
                        Pending_Full_Name,
                        ActionBy,
                        currentStatus,
                        Pending_Email,
                        finalamount,
                        ccemail
                    );
                }
                // Sent Back
                else if (objdata.CStatus == "SentBack" &&
                         currentStatus != "Acknowledged and closed" &&
                         currentStatus != "Amount Release By Finance")
                {
                    subject = "Loan Request Sent Back";
                    mailBody = Emailtext.GetLoanSentBackMail(
                        loanId,
                        Createdby,
                        sessionFullName,
                        objdata.Remark
                    );
                }
                // Rejected
                else if (objdata.CStatus == "Rejected")
                {
                    subject = "Loan Request Rejected";
                    mailBody = Emailtext.GetLoanRejectedMail(
                        loanId,
                        Createdby,
                        sessionFullName,
                        objdata.Remark
                    );
                }
                // Acknowledged and closed
                else if (currentStatus == "Acknowledged and closed")
                {
                    subject = "Loan Request Approved";
                    mailBody = Emailtext.GetLoanClosedMail(
                         amountreceiveddate,
                         Pending_Full_Name,
                         ActionBy,
                         currentStatus,
                         Pending_Email,
                         finalamount,
                            ccemail
                     );
                }
               

                 string sentemail = Emailtext.SendMail(table.Rows[0]["Toemail"].ToString(), subject, mailBody, table.Rows[0]["CCEmail"].ToString(), table.Rows[0]["Bccemail"].ToString() );
                

                returnresponse.Flag = "1";
                returnresponse.message = "Action completed successfully";



                return Json(returnresponse);
            }
            catch (SqlException ex)   // 🔴 IMPORTANTs
            {
                returnresponse.Flag = "0";
                returnresponse.message = ex.Message;   // shows THROW message
                return Json(returnresponse);
            }
            catch (Exception ex)
            {
                returnresponse.Flag = "0";
                returnresponse.message = "Unexpected error occurred";
                return Json(returnresponse);
            }
        }
        public ActionResult get_tracking_loan_records_history(string uniqueid)
        {


            List<Tracking_History> trackdetails = new List<Tracking_History>();

            string query = @"Action_Loan_Details";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Uniqueid", uniqueid);
                cmd.Parameters.AddWithValue("@Userecode", HttpContext.Session.GetString("Empcode"));
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Tracking_History litem = new Tracking_History();

                    litem.Actionid = table.Rows[i]["Requestid"].ToString();
                    //   litem.fullname = table.Rows[i]["Full Name"].ToString();
                    litem.actionby = table.Rows[i]["Actionby"].ToString();
                    litem.peningon = table.Rows[i]["Pendingon"].ToString();
                    litem.Cstatus = table.Rows[i]["Actionstatus"].ToString();
                    //litem.requestfor = table.Rows[i]["Requestfor"].ToString();
                    litem.actiondate = table.Rows[i]["Actiondate"].ToString();


                    litem.remark = table.Rows[i]["Remark"].ToString();


                    trackdetails.Add(litem);
                }

            }



            return Json(trackdetails);



        }




        public ActionResult get_loandetails_details_for_validate(string uniqueid)
        {


            List<loandata> loandeytailss = new List<loandata>();

            string query = @"Bind_Loan_Details_For_Validation";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", uniqueid);

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    loandata litem = new loandata();



                    litem.ecode = table.Rows[i]["ecode"].ToString();
                    litem.department = table.Rows[i]["department"].ToString();
                    litem.DOJ = table.Rows[i]["doj"].ToString();
                    litem.deviation = table.Rows[i]["deviation"].ToString();



                    litem.salarydown = EncryptionHelper.Decrypt(table.Rows[i]["salarydown"].ToString());
                    litem.rupess = EncryptionHelper.Decrypt(table.Rows[i]["rupess"].ToString());

                    
                           litem.attcahement = table.Rows[i]["attcahement"].ToString();
                    litem.salarybasicvdahra = EncryptionHelper.Decrypt(table.Rows[i]["Salarybasicvdahra"].ToString());
                    litem.eligibilityamount = EncryptionHelper.Decrypt(table.Rows[i]["Eligibilityamount"].ToString());
                    litem.noofinstallmentforreppayment = table.Rows[i]["Noofinstallmentforreppayment"].ToString();
                    litem.whetherconfirmedornot = table.Rows[i]["Whetherconfirmedorno"].ToString();

                    litem.deviationnoofinstallmentforreppayment = table.Rows[i]["deviationnoofinstallment"].ToString();
                    litem.deviationsanctionedamount = EncryptionHelper.Decrypt(table.Rows[i]["deviationsanctionedamount"].ToString());
                    litem.deviationfinalamount = EncryptionHelper.Decrypt(table.Rows[i]["deviationfinalamount"].ToString());

                    litem.finalnoofinstallmentforreppayment = table.Rows[i]["finalnoofinstallmentforreppayment"].ToString();
                    litem.finalsanctionedamount = EncryptionHelper.Decrypt(table.Rows[i]["getfinalsanctionedamount"].ToString());



                    litem.installments = table.Rows[i]["number_of_repayment"].ToString();
                    litem.Garantees1 = table.Rows[i]["Guaranteed1"].ToString();
                    litem.Garantees2 = table.Rows[i]["Guaranteed2"].ToString();
                    litem.fullname = table.Rows[i]["Fullname"].ToString();

                    litem.Garanteesecode1 = table.Rows[i]["Garanteesecode1"].ToString();
                    litem.Garanteesecode2 = table.Rows[i]["Garanteesecode2"].ToString();

                    litem.purpose = table.Rows[i]["purpose"].ToString();


                    litem.loanstatus = table.Rows[i]["loanstatus"].ToString();
                    litem.closeddate = table.Rows[i]["closeddate"].ToString();
                    litem.finalamount = table.Rows[i]["FinalAmount"].ToString();
                    
                    litem.sapecode = table.Rows[i]["sapecode"].ToString();


                    litem.bankname = table.Rows[i]["Banckname"].ToString();
                    litem.accountnumber = table.Rows[i]["Accountnumber"].ToString();
                    litem.gcofinalamount = table.Rows[i]["Central_Hr_Amount"].ToString();
                    litem.ifsccode = table.Rows[i]["Ifsccode"].ToString();
                    litem.gcoinstalment = table.Rows[i]["GCO_Installment"].ToString();


                    litem.businessvertical = table.Rows[i]["Business Vertical"].ToString();
                    litem.division = table.Rows[i]["Division"].ToString();
                    litem.plant = table.Rows[i]["Plant"].ToString();
                    litem.costcenter= table.Rows[i]["Cost_Center"].ToString();
                    litem.sapcostcenter = table.Rows[i]["SAP_Cost_Center"].ToString();
                    litem.prfofitcenter = table.Rows[i]["Profit_Center"].ToString();
                    litem.newcostcenter = table.Rows[i]["New_Cost_Center"].ToString();

                    litem.purposedesc= table.Rows[i]["purposedesc"].ToString(); 
                    litem.pan = table.Rows[i]["Pan"].ToString(); 
                    loandeytailss.Add(litem);
                }

            }



            return Json(loandeytailss);



        }

        //-----------------------------------Generate Email Id request -----------------------------------function----
        [HttpPost]
        public async Task<IActionResult> Request_Loan_Initiated(List<IFormFile> files, [FromForm] loandata objdata,string hiddenimaegurl)
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            string sessionFullName = HttpContext.Session.GetString("Sessionfullname");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }
            var uniqueFileName = "";
            foreach (var file in files)
            {
                if (file.Length > 0) // Ensure the file is not empty
                {
                    string fileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(file.FileName);
                    uniqueFileName = $"{DateTime.Now.Ticks}{fileExtension}";
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "Loan_" + uniqueFileName);

                    // Create the directory if it does not exist
                    var directory = Path.GetDirectoryName(uploadPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Save the file to the server asynchronously
                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream); // Ensure the file is copied properly
                    }

                    HttpContext.Session.SetString("imageurl", $"{"Loan_" + uniqueFileName}");
                }
                else
                {
                    HttpContext.Session.SetString("imageurl", hiddenimaegurl);
                }
            }
            Response returnresponse = new Response();
            try
            {


                string getsalarydown = EncryptionHelper.Encrypt(objdata.salarydown);
                string getrupess = EncryptionHelper.Encrypt(objdata.rupess);

                string query = @"sp_InsertLoanInitiated";
                DataTable table = new DataTable();

                var con = Getloan.getConnectionString();
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ecode", HttpContext.Session.GetString("Empcode"));

                    cmd.Parameters.AddWithValue("@deviation", objdata.deviation);
                    cmd.Parameters.AddWithValue("@ID", objdata.id);
                    cmd.Parameters.AddWithValue("@Actiontype", objdata.Actiontype);
                    cmd.Parameters.AddWithValue("@number_of_repayment", objdata.installments);
                    cmd.Parameters.AddWithValue("@salarydown", getsalarydown);
                    cmd.Parameters.AddWithValue("@rupess", getrupess);
                    cmd.Parameters.AddWithValue("@purpose", objdata.purpose);
                    cmd.Parameters.AddWithValue("@purposedesc", objdata.purposedesc);
                    cmd.Parameters.AddWithValue("@garantees1", objdata.Garantees1);
                    cmd.Parameters.AddWithValue("@garantees2", objdata.Garantees2);
                    cmd.Parameters.AddWithValue("@Attachment", HttpContext.Session.GetString("imageurl"));
                    cmd.Parameters.AddWithValue("@Remark", objdata.Remark);

                    da.Fill(table);


                    if (table.Rows.Count > 0)
                    {


                        returnresponse.Flag = table.Rows[0]["Success"].ToString();



                        string MailBody = "";
               

                        string mailcc = "";

                        if (returnresponse.Flag == "1")
                        {

                           
                         

                            
                                MailBody = Emailtext.GetLoanInitiatedMail(
                                table.Rows[0]["Success"].ToString(), table.Rows[0]["CreatedBy"].ToString(),
                                sessionFullName, table.Rows[0]["Pending_Full_Name"].ToString(), table.Rows[0]["MESSAGE"].ToString(), table.Rows[0]["Pendingemail"].ToString());

                            

                            Emailtext.SendMail(table.Rows[0]["Pendingemail"].ToString(), "Approval Required: Loan Request – "+ table.Rows[0]["CreatedBy"].ToString() + "", MailBody, sessionUser, table.Rows[0]["Pending_Full_Name"].ToString() );

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
                }
                return Json(returnresponse);
            }
            catch (SystemException ex)
            {
                return Json(returnresponse);
            }

        }
        public IActionResult DownloadFile(string ImageName)
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login pag
                return RedirectToAction("User_Login", "Inter");
            }

            // Ensure that imageName is provided and it's not null or empty
            if (string.IsNullOrEmpty(ImageName))
            {
                return BadRequest("File name is required.");
            }

            // Define the full file path inside the wwwroot/uploadedfile folder
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", ImageName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 if file does not exist
            }

            // Open the file as a stream and return it to the client
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, "application/octet-stream")
            {
                FileDownloadName = ImageName // The name the user will see when downloading the file
            };

        }

        //-----------------------------------------bind loan details 


        public ActionResult get_loandetails()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            List<loandata> loandeytailss = new List<loandata>();

            string query = @"Get_Loan_Request_Details";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string a = HttpContext.Session.GetString("Sessionusername");
                cmd.Parameters.AddWithValue("@Ecode", HttpContext.Session.GetString("Empcode"));

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    loandata litem = new loandata();


                    litem.id = table.Rows[i]["id"].ToString();
                    litem.Gid = table.Rows[i]["GID"].ToString();
                    litem.deviation = table.Rows[i]["deviation"].ToString();
                    litem.salarydown = EncryptionHelper.Decrypt(table.Rows[i]["salarydrown"].ToString()); 
                    litem.rupess = EncryptionHelper.Decrypt(table.Rows[i]["rupess"].ToString());
                    litem.installments = table.Rows[i]["Number_Repayment"].ToString();

                    litem.CStatus = table.Rows[i]["CStatus"].ToString();
                    litem.purpose = table.Rows[i]["purpose"].ToString();
                    litem.pending = table.Rows[i]["Pendingon"].ToString();
                    loandeytailss.Add(litem);
                }

            }
            else { loandata litem = new loandata(); loandeytailss.Add(litem); }



            return Json(loandeytailss);



        }

        public ActionResult Get_Loan_Pending_Approval()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            List<loandata> loandeytailss = new List<loandata>();

            string query = @"Get_Loan_Pending_Approval";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ecode", HttpContext.Session.GetString("Empcode"));
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    loandata litem = new loandata();
                    
                          litem.Gid = table.Rows[i]["GID"].ToString();
                    litem.id = table.Rows[i]["id"].ToString();
                    litem.deviation = table.Rows[i]["deviation"].ToString();
                    litem.salarydown = EncryptionHelper.Decrypt(table.Rows[i]["salarydrown"].ToString());
                    litem.rupess = EncryptionHelper.Decrypt(table.Rows[i]["rupess"].ToString());
                    litem.installments = table.Rows[i]["Number_Repayment"].ToString();
                    litem.CStatus = table.Rows[i]["cStatus"].ToString();
                    litem.purpose = table.Rows[i]["purpose"].ToString();
                    litem.pending = table.Rows[i]["pendingon"].ToString();
                    litem.Remark = table.Rows[i]["remark"].ToString();
                    litem.fullname = table.Rows[i]["createdBy"].ToString();

                    loandeytailss.Add(litem);
                }
            }

            // ✅ If no rows, return empty list
            return Json(loandeytailss);
        }

        public ActionResult Get_Loan_Approved()
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                // Session has expired, redirect to login page
                return RedirectToAction("User_Login", "Inter");
            }

            List<loandata> loandeytailss = new List<loandata>();

            string query = @"Get_Loan_Approved_Details";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ecode", HttpContext.Session.GetString("Empcode"));
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    loandata litem = new loandata();

                    litem.Gid = table.Rows[i]["GID"].ToString();
                    litem.id = table.Rows[i]["id"].ToString();
                    litem.deviation = table.Rows[i]["deviation"].ToString();
                    litem.salarydown = EncryptionHelper.Decrypt(table.Rows[i]["salarydrown"].ToString());
                    litem.rupess = EncryptionHelper.Decrypt(table.Rows[i]["rupess"].ToString());
                    litem.installments = table.Rows[i]["Number_Repayment"].ToString();
                    litem.CStatus = table.Rows[i]["cStatus"].ToString();
                    litem.purpose = table.Rows[i]["purpose"].ToString();
                    litem.pending = table.Rows[i]["pendingon"].ToString();
                    litem.Remark = table.Rows[i]["remark"].ToString();
                    litem.fullname = table.Rows[i]["createdBy"].ToString();

                    loandeytailss.Add(litem);
                }
            }

            // ✅ If no rows, return empty list
            return Json(loandeytailss);
        }

        public ActionResult Get_Loan_Details_Export_InExcel(string id)
        {
            var sessionUser = HttpContext.Session.GetString("Sessionusername");
            if (string.IsNullOrEmpty(sessionUser))
            {
                return RedirectToAction("User_Login", "Inter");
            }

            List<loandata> loandeytailss = new List<loandata>();

            string query = @"Get_Loan_Report_Details";
            DataTable table = new DataTable();

            var con = Getloan.getConnectionString();

            using (SqlConnection connection = new SqlConnection(con))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    loandata litem = new loandata();

                    litem.ecode = table.Rows[i]["Employeecode"].ToString();
                    litem.fullname = table.Rows[i]["Empname"].ToString();
                    litem.costcenter = table.Rows[i]["CostCenter"].ToString();
                    litem.sapcostcenter = table.Rows[i]["Sap_Cost_Center"].ToString();
                    litem.prfofitcenter = table.Rows[i]["Profit_Center"].ToString();
                    litem.plant = table.Rows[i]["PlantCode"].ToString();
                    litem.newcostcenter = table.Rows[i]["Newcostcenter"].ToString();
                    litem.pan = table.Rows[i]["Pan"].ToString();
                    litem.bankname = table.Rows[i]["BankName"].ToString();
                    litem.ifsccode = table.Rows[i]["IFSC_Code"].ToString();
                    litem.accountnumber = table.Rows[i]["Bank_Account"].ToString();
                    litem.finalamount = table.Rows[i]["Amount"].ToString();
                    litem.sapecode = table.Rows[i]["SAP_Vendor_Code"].ToString();

                    loandeytailss.Add(litem);
                }
            }

            return Json(new { data = loandeytailss });
        }

    }
}
