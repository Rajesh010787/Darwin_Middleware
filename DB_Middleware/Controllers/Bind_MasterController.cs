using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DB_Middleware.DAO;
using DB_Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DB_Middleware.Controllers
{
    public class Bind_MasterController : Controller
    {
        // GET: Bind_MasterController
        public ActionResult Index()
        {
            return View();
        }
        DB_DataAccess_Layer GetMasterdadadetails;
        // GET: Email_RequestController
        public Bind_MasterController(IConfiguration configuration)
        {
            GetMasterdadadetails = new DB_DataAccess_Layer(configuration);
        }

        // GET: Bind_MasterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bind_MasterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bind_MasterController/Create
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

        // GET: Bind_MasterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bind_MasterController/Edit/5
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

        // GET: Bind_MasterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bind_MasterController/Delete/5
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
        //Get -----Domain Name in Dropdown & Group Email-------------------


        public ActionResult Bind_Domain(string Groupcompanyname,string Cost_Center_Name,string accessdomain)

        {

            List<Master_Data> BindMData = new List<Master_Data>();

            string query = @"BIND_DomainName";
            DataTable table = new DataTable();

            var con = GetMasterdadadetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Group_CompanyName", Groupcompanyname);
                cmd.Parameters.AddWithValue("@Department_Cost_Center_Name", Cost_Center_Name);
                cmd.Parameters.AddWithValue("@Bind_Domain", accessdomain);
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Master_Data litem = new Master_Data();

                    litem.domainname = table.Rows[i]["Domain"].ToString();

                    BindMData.Add(litem);
                }

            }


            return Json(BindMData);


        }

        public ActionResult Bind_SMIT_SubDepart() 

        {

            List<Master_Data> BindMData = new List<Master_Data>();

            string query = @"Bind_SMIT_Depart";
            DataTable table = new DataTable();

            var con = GetMasterdadadetails.getConnectionString();
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

                    litem.smitdepart = table.Rows[i]["Smitsubdepart"].ToString();

                    BindMData.Add(litem);
                }

            }


            return Json(BindMData);


        }


        public ActionResult Bind_MSLicense()

        {

            List<Master_Data> BindMData = new List<Master_Data>();

            string query = @"Bind_MS_License";
            DataTable table = new DataTable();

            var con = GetMasterdadadetails.getConnectionString();
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

                    litem.mslicense = table.Rows[i]["MSLicence"].ToString();

                    BindMData.Add(litem);
                }

            }


            return Json(BindMData);


        }

        public ActionResult Bind_GEmail(string Groupcompanyname, string Cost_Center_Name)

        {

            List<Master_Data> BindGroupEmail = new List<Master_Data>(); 

            string query = @"BIND_Group_Email";
            DataTable table = new DataTable();

            var con = GetMasterdadadetails.getConnectionString();
            SqlConnection connection = new SqlConnection(con);
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Group_CompanyName", Groupcompanyname);
                cmd.Parameters.AddWithValue("@Department_Cost_Center_Name", Cost_Center_Name);  
                da.Fill(table);
            }

            if (table.Rows.Count > 0)
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Master_Data litem = new Master_Data();

                    litem.groupemailids = table.Rows[i]["Email"].ToString();

                    BindGroupEmail.Add(litem);
                }

            }


            return Json(BindGroupEmail);


        }
    }
}
