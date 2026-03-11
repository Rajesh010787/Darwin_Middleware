using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static DB_Middleware.Models.Menu;

namespace DB_Middleware.DAO
{
   
    public interface IMenuService
    {
       
        List<MenuListdefaultPage> GetMenuItemsAsync();
       
    }
   

    public class MenuService : IMenuService
    {
        private readonly IConfiguration _configuration;
   
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        public List<MenuListdefaultPage> GetMenuItemsAsync()
        {
            var con = _configuration.GetConnectionString("DefaultConnection");

            var menuItems = new List<MenuListdefaultPage>();

            try
            {
                string query = @"Menu_List_Default_Page";
                DataSet  ds = new DataSet();

        
                SqlConnection connection = new SqlConnection(con);
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    var userId = _httpContextAccessor.HttpContext.Session.GetString("Sessionusername");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_ID", userId);
                    da.Fill(ds);
                }

                // Get the data table
                DataTable dt = ds.Tables[0];
              

                // Use LINQ to filter and transform data
                var parentItems = dt.AsEnumerable()
                    .Where(row => row.Field<int>("ParentID") == 0)
                    .Select(row => new MenuListdefaultPage
                    {
                        ID = row.Field<int>("ID"),
                        ModuleName = row.Field<string>("InnerHtml"),
                        LCModuleName = row.Field<string>("InnerHtml").ToLower(),
                        PageUrl = row.Field<string>("Url"),
                        Icon = row.Field<string>("IMAGE_URL"),
                        MenuList = dt.AsEnumerable()
                            .Where(subRow => subRow.Field<int>("ParentID") == row.Field<int>("ID"))
                            .Select(subRow => new MenulistBulkDetail
                            {
                                ID = subRow.Field<int>("ID"),
                                ModuleName = subRow.Field<string>("InnerHtml"),
                                ListLOCModuleName = subRow.Field<string>("InnerHtml").ToLower(),
                                PageUrl = subRow.Field<string>("Url")
                         
                            }).ToList()
                    }).ToList();

                menuItems.AddRange(parentItems);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error fetching menu items: {ex.Message}");
                // Depending on your needs, you might return an empty list or rethrow the exception
            }
            return menuItems;
        }
    }


}

