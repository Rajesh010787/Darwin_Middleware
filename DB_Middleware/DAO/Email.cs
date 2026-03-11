using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DB_Middleware.DAO
{
    public class Emailtext

    {
        public static string SendMail(string toEmail,
        string subject,
        string bodyHtml,
        string ccEmail,
        string bccEmail)
        {

            try
            {


                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                string userName = "noreply@mindacorporation.com";
                string password = "Sos67130";
                string frommail = "noreply@mindacorporation.com";
                MailAddress sentfrom = new MailAddress(frommail);
                msg.From = sentfrom;
                msg.To.Add("durgesh.kumar@mindacorporation.com");
                msg.Bcc.Add("rajesh.kumar1@mindacorporation.com");
                

                msg.Subject = subject.ToString();

                msg.Body = bodyHtml;
                msg.IsBodyHtml = true;
                SmtpClient SmtpClient = new SmtpClient();
                SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                SmtpClient.Host = "smtp.office365.com";
                SmtpClient.Port = 587;
                SmtpClient.EnableSsl = true;
                SmtpClient.Send(msg);





                return "1";

            }
            catch (Exception ex)
            {
                // Log exception (DB / file / logger)
                return "0";
            }





        }
        public static string GetLoanInitiatedMail(
    string loanId,
    string employeeName,
 
    string initiatedBy,
        string GName,
    string currentStatus,
    string pendingWith)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");
            sb.AppendLine("<p>Dear "+ GName + ",</p>");
            sb.AppendLine("<p>Greetings!</p>");
            sb.AppendLine($"<p>This is to inform you that  " +
                          $"<strong>{employeeName}</strong> has applied for a loan request in the system. The same is submitted for your review and approval.</p> ");

            sb.AppendLine($"<p>Kindly review the request at your end and provide your approval/comments to enable us to process it further</p>");

            
            //sb.AppendLine("<table width='100%' cellpadding='8' style='background:#f9fafb;border:1px solid #e0e0e0;'>");
            //sb.AppendLine($"<tr><td><strong>Initiated By</strong></td><td>{initiatedBy}</td></tr>");
            //sb.AppendLine($"<tr><td><strong>Current Status</strong></td><td>{currentStatus}</td></tr>");
            //sb.AppendLine($"<tr><td><strong>Pending With</strong></td><td>{pendingWith}</td></tr>");
            //sb.AppendLine("</table>");

            sb.AppendLine("<p style='margin-top:20px;'>" +
              $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");

   
            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }


        public static string MailApprovedbyG1(

  string pendingfullname, string createdfullname, string toemail
)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");
            sb.AppendLine("<p>Dear " + pendingfullname + ",</p>");
            sb.AppendLine("<p>Greetings!</p>");
            sb.AppendLine($"<p>This is to inform you that  " +
                          $"<strong>{createdfullname}</strong> has applied for a loan request in the system. The same is submitted for your review and approval.</p> ");

            sb.AppendLine($"<p>Kindly review the request at your end and provide your approval/comments to enable us to process it further</p>");


            //sb.AppendLine("<table width='100%' cellpadding='8' style='background:#f9fafb;border:1px solid #e0e0e0;'>");
            //sb.AppendLine($"<tr><td><strong>Initiated By</strong></td><td>{initiatedBy}</td></tr>");
            //sb.AppendLine($"<tr><td><strong>Current Status</strong></td><td>{currentStatus}</td></tr>");
            //sb.AppendLine($"<tr><td><strong>Pending With</strong></td><td>{pendingWith}</td></tr>");
            //sb.AppendLine("</table>");

            sb.AppendLine("<p style='margin-top:20px;'>" +
              $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }

        public static string whileapprovedbyG2(
            string Pending_Full_Name,
string employeeName,
string pendingemail, 
string G1,
string G2
)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");

            sb.AppendLine("<p>Dear <strong> " + Pending_Full_Name + " </strong>,</p>");
            sb.AppendLine("<p>Greetings!</p>");

            sb.AppendLine($"<p>This is to inform you that an employee<strong> " + employeeName + " </strong>has applied for a loan request, and the same has been duly approved by <strong>" + G1 + "</strong> and <strong>" + G2 + "</strong> as per the applicable loan policy.</p>");

            sb.AppendLine("<p>As the next step, the HR team is requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing.</p>");

            sb.AppendLine("<p style='margin-top:20px;'>" +
             $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }
        public static string GetLoanApprovedbyhrspocMail(
string loanId,
string employeeName,
string hrspocname,
string G1,
string G2,
string actionBy,
string currentStatus,
string pendingWith)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");
       
            sb.AppendLine("<p>Dear "+ hrspocname + ",</p>");
            sb.AppendLine("<p>Greetings!</p>");

            sb.AppendLine($"<p>This is to inform you that an employee "+ employeeName +" has applied for a loan request, and the same has been duly approved by "+ G1 + " and "+ G2 +" as per the applicable loan policy.</p>");

            sb.AppendLine("<p>As the next step, the HR team is requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing.</p>");

            sb.AppendLine("<p style='margin-top:20px;'>" +
             $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }


        public static string whileapprovedbyspochr(
string Toemail,
string employeeName,
string pendingfullname,
string G1,
string G2,
string hrspocname
)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");

            sb.AppendLine("<p>Dear <strong> " + pendingfullname + "</strong>,</p>");
            sb.AppendLine("<p>Greetings!</p>");

            sb.AppendLine($"<p>This is to inform you that an employee <strong>" +employeeName + " </strong>has applied for a loan request, and the same has been duly approved by <strong>" + G1 + "</strong> and <strong>" + G2 + "</strong> and reviewed by your HR Team Member <strong>" + hrspocname+ "</strong> .</p>");

            sb.AppendLine("<p>As the next step, you requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing.</p>");
            sb.AppendLine("<p style='margin-top:20px;'>" +
                         $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }

        public static string whileapprovedbyplanthrhead(
string Toemail,
string employeeName,
string pendingfullname,
string G1,
string G2,
string ActionBy,
string hrspocname,
string deviationcase
)
        {
            StringBuilder sb = new StringBuilder();

            if (deviationcase == "Yes")
            {
                sb.AppendLine("<table width='100%'><tr><td align='left'>");
                sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

                sb.AppendLine("<tr><td>");

                sb.AppendLine("<p>Dear <strong> " + pendingfullname + "</strong>,</p>");
                sb.AppendLine("<p>Greetings!</p>");

                sb.AppendLine($"<p>This is to notify that an employee <strong>"+ employeeName + "</strong> has applied for a loan request, which was processed as a deviation case. The request has been duly <strong> validated and approved </strong> by the respective <strong>" + hrspocname + "</strong> and <strong>" + ActionBy + "</strong>, in line with the defined approval process and policies.</p>");

                sb.AppendLine("<p>As the next step, you requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing.</p>");
                sb.AppendLine("<p style='margin-top:20px;'>" +
                             $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


                sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

                sb.AppendLine("</td></tr></table></td></tr></table>");


            } else
            {

                sb.AppendLine("<table width='100%'><tr><td align='left'>");
                sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

                sb.AppendLine("<tr><td>");

                sb.AppendLine("<p>Dear <strong> " + pendingfullname + "</strong>,</p>");
                sb.AppendLine("<p>Greetings!</p>");

                sb.AppendLine($"<p>This is to inform you that an employee <strong>" + employeeName + "</strong> has applied for a loan request, and the same has been approved at all respective approval authority.</p>");

                sb.AppendLine($"<p>You are requested to fill all the necessary detail in the form for further process to submit with Finance Team.</p>");

                sb.AppendLine("<p>As the next step, you requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing.</p>");
                sb.AppendLine("<p style='margin-top:20px;'>" +
                             $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


                sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

                sb.AppendLine("</td></tr></table></td></tr></table>");

            }



            return sb.ToString();
        }

        public static string whileapprovedfinalaction(
string  createdby,
            string Pending_Full_Name,
                   string Pendingemail)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");

            sb.AppendLine("<p>Dear <strong> " + Pending_Full_Name + "</strong>,</p>");
            sb.AppendLine("<p>Greetings!</p>");

       
            sb.AppendLine($"<p>This is to inform you that an employee <strong> " + createdby + " </strong> has applied for a loan request, and the same has been approved at all respective approval authority.</p>");


            sb.AppendLine("<p>You are requested to fill all the necessary detail in the form for further process to submit with Finance Team.</p>");

            sb.AppendLine("<p>As the next step, you requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing.</p>");

            sb.AppendLine("<p style='margin-top:20px;'>" +
               $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }


        public static string whileapprovedcentralpayrollteam(
string createdby,
           string Pending_Full_Name,
                  string Pendingemail)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");

            sb.AppendLine("<p>Dear <strong> " + Pending_Full_Name + "</strong>,</p>");
            sb.AppendLine("<p>Greetings!</p>");


            sb.AppendLine($"<p>This is to inform you that an employee <strong> " + createdby + " </strong> has applied for a loan request, and the same has been approved at all respective approval authority.</p>");


            sb.AppendLine("<p>The Loan Request has been validate by Central Payroll Team details are given in the form.</p>");

            sb.AppendLine("<p>As the next step, you requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing the Laon amount into the employee account.</p>");

            sb.AppendLine("<p style='margin-top:20px;'>" +
               $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }

        public static string GetLoanApprovedbycentralhrpayrollMail(
string loanId,
string employeeName,
string hrspocname,
string G1,
string G2,
string actionBy,
string currentStatus,
string pendingWith)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");

            sb.AppendLine("<p>Dear " + hrspocname + ",</p>");
            sb.AppendLine("<p>Greetings!</p>");

            sb.AppendLine($"<p>This is to inform you that an employee "+ employeeName +" has applied for a loan request, and the same has been approved at all respective approval authority.</p>");

            sb.AppendLine($"<p>You are requested to fill all the necessary detail in the form for further process to submit with Finance Team.</p>");


            sb.AppendLine("<p>As the next step, you requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing.</p>");

            sb.AppendLine("<p style='margin-top:20px;'>" +
               $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }
        public static string GetLoanrvalidatebycentralspochrMail(

string employeeName,

string pendingWith)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");

            sb.AppendLine("<p>Dear " + pendingWith + ",</p>");
            sb.AppendLine("<p>Greetings!</p>");

            sb.AppendLine($"<p>This is to inform you that an employee "+ employeeName + " has applied for a loan request, and the same has been approved at all respective approval authority.</p>");

            sb.AppendLine($"<p>The Loan Request has been validate by Central Payroll Team details are given in the form.</p>");


            sb.AppendLine("<p>As the next step, you requested to review the loan request in line with the policy guidelines and take necessary action at your end for further processing the Laon amount into the employee account.</p>");

            sb.AppendLine("<p style='margin-top:20px;'>" +
               $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }
        public static string GetLoanreleaseamountMail(
      string loanId,
                       string createdBy,
                      string Plant_Vertical,
                      string CostCenter,
                      string DisbursementDate,
                      string Toemail,
                      string Pending_Full_Name,
                           string Loanamount, 
                       string ccemail)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<table width='100%' style='font-family:Arial, sans-serif;'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");
            sb.AppendLine("<tr><td>");

            sb.AppendLine($"<p>Dear {Pending_Full_Name},</p>");
            sb.AppendLine("<p>Greetings!</p>");

            sb.AppendLine("<p>This is to notify that the employee loan amount has been successfully disbursed by the Finance team into the employee’s registered bank account.</p>");

            sb.AppendLine("<p>We request you to take the necessary action at your end for payroll processing and recovery, in line with the applicable loan terms and policy.</p>");

            sb.AppendLine("<p><strong>Employee Details:</strong></p>");
            sb.AppendLine("<ul style='padding-left:20px;'>");
            sb.AppendLine($"<li><strong>Employee Name:</strong> {createdBy}</li>");

            sb.AppendLine($"<li><strong>Plant / Vertical:</strong> {Plant_Vertical+'/'+ CostCenter}</li>");

            sb.AppendLine($"<li><strong>Loan Amount:</strong> {Loanamount}</li>");
            sb.AppendLine($"<li><strong>Disbursement Date:</strong> {DisbursementDate}</li>");
            sb.AppendLine("</ul>");

            sb.AppendLine("<p style='margin-top:20px;'>");
            sb.AppendLine("<a href='https://darwinmware.sparkminda.in/' style='background-color:#007bff;color:#ffffff;padding:8px 12px;text-decoration:none;border-radius:4px;'>Click here to view details</a>");
            sb.AppendLine("</p>");

            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }

        public static string GetLoanApprovedMail(
    string loanId,
    string employeeName,
    string actionBy,
    string currentStatus,
    string pendingWith)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");
            sb.AppendLine("<h3 style='color:#2c3e50;'>Loan Request Approved</h3>");
            sb.AppendLine("<p>Dear Team,</p>");
            sb.AppendLine($"<p>The loan request <strong>{loanId}</strong> raised by " +
                          $"<strong>{employeeName}</strong> has been <strong>approved</strong> by " +
                          $"<strong>{actionBy}</strong>.</p>");

            sb.AppendLine("<table width='100%' cellpadding='8' style='background:#f9fafb;border:1px solid #e0e0e0;'>");
            sb.AppendLine($"<tr><td><strong>Current Status</strong></td><td>{currentStatus}</td></tr>");
            sb.AppendLine($"<tr><td><strong>Pending With</strong></td><td>{pendingWith}</td></tr>");
            sb.AppendLine("</table>");

            sb.AppendLine("<p style='margin-top:20px;'>Please review and take the necessary action: " +
$"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");
            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");


            return sb.ToString();
        }

        public static string GetLoanSentBackMail(
    string loanId,
    string employeeName,
    string actionBy,
    string remarks)
        {
            StringBuilder sb = new StringBuilder();

            //      sb.AppendLine("<!DOCTYPE html>");
            //      sb.AppendLine("<html><head><meta charset='UTF-8'></head>");
            //      sb.AppendLine("<body style='font-family:Arial;background-color:#f4f6f8;padding:20px;'>");
            //      sb.AppendLine("<table width='100%'><tr><td align='left'>");
            //      sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");
              
            //      sb.AppendLine("<tr><td>");
            //      sb.AppendLine("<h3 style='color:#e67e22;'>Loan Request Sent Back</h3>");
            //      sb.AppendLine($"<p>Dear {employeeName},</p>");
            //      sb.AppendLine($"<p>Your loan request <strong>{loanId}</strong> has been sent back by " +
            //                    $"<strong>{actionBy}</strong>.</p>");

            //      sb.AppendLine("<p><strong>Remarks:</strong></p>");
            //      sb.AppendLine($"<div style='background:#fff3cd;padding:10px;border:1px solid #ffeeba;'>{remarks}</div>");

            //      sb.AppendLine("<p style='margin-top:20px;'>Please review and take the necessary action: " +
            //$"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");
            //      sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            //      sb.AppendLine("</td></tr></table></td></tr></table>");
            //      sb.AppendLine("</body></html>");


            sb.AppendLine("<table width='100%' style='font-family:Arial, sans-serif;'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");
            sb.AppendLine("<tr><td>");

            sb.AppendLine($"<p>Dear {employeeName},</p>");
            sb.AppendLine("<p>Greetings!</p>");

            sb.AppendLine("<p>his is to inform you that your Loan Request Form has been reviewed by the approver <strong>"+ actionBy + "</strong> and has been sent back for correction / clarification. You are requested to please review the remarks mentioned in the system and resubmit the form after making the necessary corrections at the earliest.</p>");




         

            sb.AppendLine("<p style='margin-top:20px;'>");
            sb.AppendLine("<a href='https://darwinmware.sparkminda.in/' style='background-color:#007bff;color:#ffffff;padding:8px 12px;text-decoration:none;border-radius:4px;'>Click here to view details</a>");
            sb.AppendLine("</p>");

            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();




         
        }


        public static string GetLoanRejectedMail(
    string loanId,
    string employeeName,
    string actionBy,
    string remarks)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html><head><meta charset='UTF-8'></head>");
            sb.AppendLine("<body style='font-family:Arial;background-color:#f4f6f8;padding:20px;'>");
            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");
            sb.AppendLine("<h3 style='color:#c0392b;'>Loan Request Rejected</h3>");
            sb.AppendLine($"<p>Dear {employeeName},</p>");
            sb.AppendLine($"<p>Your loan request <strong>{loanId}</strong> has been rejected by " +
                          $"<strong>{actionBy}</strong>.</p>");

            sb.AppendLine("<p><strong>Remarks:</strong></p>");
            sb.AppendLine($"<div style='background:#f8d7da;padding:10px;border:1px solid #f5c6cb;'>{remarks}</div>");

            sb.AppendLine("<p style='margin-top:20px;'>Please review: " +
  $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");

            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
        public static string GetAmountReleasedMail( 
    string loanId,
    string employeeName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html><head><meta charset='UTF-8'></head>");
            sb.AppendLine("<body style='font-family:Arial;background-color:#f4f6f8;padding:20px;'>");
            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");
            sb.AppendLine("<h3 style='color:#27ae60;'>Loan Amount Released</h3>");
            sb.AppendLine($"<p>Dear {employeeName},</p>");
            sb.AppendLine($"<p>The loan amount for request <strong>{loanId}</strong> has been successfully released.</p>");

   

            sb.AppendLine("<p style='margin-top:20px;'>Please acknowledge the receipt in the system: " +
  $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");
            sb.AppendLine("<p>Regards,<br><strong>Finance Team</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }

        public static string GetLoanClosedMail(
            string Amountreceiveddate,
      string    Pending_Full_Name,
             string ActionBy,
              string currentStatus,
               string Pending_Email,
                 string finalamount,
                  string ccemail
                 )
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<table width='100%'><tr><td align='left'>");
            sb.AppendLine("<table width='600' style='background:#ffffff;border-radius:6px;' cellpadding='20'>");

            sb.AppendLine("<tr><td>");

            sb.AppendLine("<p>Dear " + Pending_Full_Name + ",</p>");
            sb.AppendLine("<p>Greetings!</p>");

            sb.AppendLine($"<p>This is to acknowledge that I have received the loan amount of "+ finalamount + " credited to my bank account on "+ Amountreceiveddate + " .</p>");

            sb.AppendLine($"<p>TI confirm that the loan has been received successfully and I agree to the applicable terms and conditions, including repayment and recovery as per company policy.</p>");



            sb.AppendLine("<p style='margin-top:20px;'>" +
               $"<a href='https://darwinmware.sparkminda.in/'>Click here</a>.</p>");


            sb.AppendLine("<p>Regards,<br><strong>HR Operations</strong></p>");

            sb.AppendLine("</td></tr></table></td></tr></table>");

            return sb.ToString();
        }


        public static string GetLoanApplicationHtml(string  isDeviation)
        {
            isDeviation = "Y";
            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "mindalogo.png");

            string logoBase64 = Convert.ToBase64String(File.ReadAllBytes(logoPath));
            string logoSrc = $"data:image/png;base64,{logoBase64}";

            StringBuilder sb = new StringBuilder();
        
            sb.Append($@"
<!DOCTYPE html>
<html>
<head>
<meta charset='UTF-8'>
<title>Application for Loan / Advance</title>

<style>
body {{
    font-family: Arial, sans-serif;
    font-size: 12px;
    margin: 0;
}}
.container {{
    width: 800px;
    margin: auto;
    padding: 20px;
}}
.header {{
    display: flex;
    align-items: center;
    justify-content: space-between;
}}
.header-title {{
    flex: 1;
    text-align: center;
}}
.logo {{
    width: 80px;
    height: auto;
}}
.row {{
    margin: 8px 0;
}}
.line {{
    display: inline-block;
    border-bottom: 1px solid #000;
    min-width: 150px;
    height: 14px;
    vertical-align: bottom;
}}
.long {{
    min-width: 300px;
}}
.section {{
    margin-top: 18px;
    font-weight: bold;
    text-decoration: underline;
}}
.signature {{
    margin-top: 15px;
}}
.footer {{
    font-size: 10px;
    margin-top: 20px;
}}
@media print {{
    @page {{
        size: A4;
        margin: 20mm;
    }}
    body {{
        -webkit-print-color-adjust: exact;
        print-color-adjust: exact;
    }}
}}
</style>
</head>

<body onload='window.print()'>
<div class='container'>

<!-- Header with logo -->
<div class='header'>
    <div class='header-title'>
        <h2>APPLICATION FOR LOAN / ADVANCE</h2>
        <h3>Annexure - 1</h3>
    </div>
    <img src='{logoSrc}' class='logo' />
</div>

<!-- Applicant Details -->
<div class='row'>
Name <span class='line long'></span>
Employee Code <span class='line'></span>
Deptt <span class='line'></span>
</div>

<div class='row'>
Salary Drawn <span class='line'></span>
D.O.J <span class='line'></span>
Amount applied for Rs <span class='line'></span>
</div>

<div class='row'>
(Rupees <span class='line long'></span>)
Purpose <span class='line long'></span>
</div>

<div class='row'>
Number of installments for Repayment <span class='line'></span>
</div>

<div class='row signature'>
Recommended by <span class='line long'></span>
Signature of Applicant <span class='line long'></span>
<br/>(H.O.D.)
</div>

<!-- Guarantee Section -->
<div class='section'>GUARANTEE</div>
<div class='row'>
We, (1) <span class='line'></span> (2) <span class='line'></span>
employees of <span class='line long'></span> working in
</div>
<div class='row'>
deptt. (1) <span class='line'></span> (2) <span class='line'></span>
employee code No. (1) <span class='line'></span> (2) <span class='line'></span>
</div>
<div class='row'>
Since (1) <span class='line'></span> (2) <span class='line'></span> respectively do hereby
stand guarantee severally and jointly for the above amount and authorise the company
to recover the amount from our salary or any other amount due to us from the company
in the event said amount or any part thereof being not paid by the above borrower.
</div>
<div class='row signature'>
Signatures of the Guarantors No. (1) <span class='line long'></span><br/>
(2) <span class='line long'></span>
</div>
<div class='row'>
<b>Note:</b> The guarantor must have minimum 2 years service in the company
</div>

<!-- FOR OFFICE USE -->
<div class='section'>FOR OFFICE USE (HR/P&A Deptt.)</div>
<div class='row'>
Salary or Basic+VDA+HRA <span class='line'></span>
Whether confirmed or not <span class='line'></span>
</div>
<div class='row'>
Status of last loan repayment <span class='line'></span>
Eligibility amount as per Policy <span class='line'></span>
</div>
<div class='row'>
No. of Installment for Repayment as per policy <span class='line'></span>
</div>
<div class='row signature'>
Verified by P & A <span class='line long'></span>
</div>

<!-- Final Approval -->
<div class='section'>FINAL APPROVAL</div>
<div class='row'>
Sanctioned Amount Rs <span class='line'></span>
</div>
<div class='row'>
No. of Installments for Repayment <span class='line'></span>
</div>
<div class='row signature'>
Signature of Approving Authority <span class='line long'></span>
</div>");

            // Conditional Managing Director signature
            if (isDeviation=="Y")
            {
                sb.Append(@"
<div class='row signature'>
Signature of Managing Director <span class='line long'></span>
</div>");
            }

            sb.Append(@"
<div class='row'>
Date <span class='line'></span>
</div>

<!-- Acknowledgment -->
<div class='section'>ACKNOWLEDGMENT</div>
<div class='row'>
I acknowledge the receipt of Rs <span class='line'></span>
by cash / cheque no <span class='line'></span> dated <span class='line'></span>
</div>
<div class='row'>
This amount may be deducted from my monthly Salary or any other amount due to me from the company.
</div>
<div class='row signature'>
Signature of the Employee <span class='line long'></span>
</div>
<div class='row'>
Date <span class='line'></span>
</div>

<div class='footer'>
CSG-HR-324 REV.00 DATE:01-04-2010<br/>
Note: Xerox copy of the application for loan after final approval has to be kept in the individual personnel file
</div>

</div>
</body>
</html>
");

            return sb.ToString();
        }





    }
}
