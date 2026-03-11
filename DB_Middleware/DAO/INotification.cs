using DB_Middleware.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static DB_Middleware.Models.Notification;

namespace DB_Middleware.DAO
{
  public  interface INotification 
    {
       Notification_Main GetNotoficaltionmAsync();
    }
    public class  NotificationService:INotification
    {

        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        { 
            _configuration = configuration;
        }


        public Notification_Main GetNotoficaltionmAsync() 
        {
            var con = _configuration.GetConnectionString("DefaultConnection");
            var listmodel = new Notification_Main();

            try
            {
                string query = @"Get_Notification";
                DataSet ds = new DataSet();


                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_ID", "1");
                    da.Fill(ds);
                }

                // Get the data table
                DataTable dt = ds.Tables[0];



               
                List<ListNotification> model1 = new List<ListNotification>();

                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ListNotification Listnotifaction = new ListNotification();


                        Listnotifaction.ID = ds.Tables[0].Rows[i]["RequestID"].ToString();
                        Listnotifaction.CustomerName = ds.Tables[0].Rows[i]["Customer"].ToString();


                        model1.Add(Listnotifaction);

                        ////////////end considration




                    }
                    listmodel.CountNotification = ds.Tables[0].Rows[0]["CountNoti"].ToString();
                    listmodel.Notodetails = model1;

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error fetching menu items: {ex.Message}");
                // Depending on your needs, you might return an empty list or rethrow the exception
            }
            return listmodel;
        }

    }
}
