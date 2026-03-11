
using DB_Middleware.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace DB_Middleware.DAO
{
    public class DB_DataAccess_Layer
    {
        public IConfiguration Configuration { get; }
        private string _connectionString = string.Empty;
        private SqlConnection _DbCon;
        public SqlConnection DbCon
        {
            get
            {
                return _DbCon;
            }
        }

        public DB_DataAccess_Layer(IConfiguration configuration)
        {

            Configuration = configuration;
        }
        public string getConnectionString()
        {

            return Configuration["ConnectionStrings:DefaultConnection"];
        }

        //public string getConnectionString()
        //{
        //    // Read the encrypted string from appsettings.json
        //    var encryptedConnection = Configuration.GetConnectionString("DefaultConnection");

        //    // Decrypt the Base64 string
        //    var connectionString = EncryptionHelper.DBDecrypt(encryptedConnection);

        //    return connectionString;
        //}
        ////public string getConnectionString()
        ////{
        ////    //var plainConnection = "server=103.205.67.42,2499;  database=Darwinbox_MDB; user=Darwinbox_MDB; password=Darwinbox@123$; Persist Security Info=False; Connect Timeout=300";
        ////    //var encrypted = EncryptionHelper.DBEncrypt(plainConnection);
        ////    //EncryptionHelper.DBDecrypt(encryptedConnection);

        ////    var encryptedConnection = Configuration.GetConnectionString("DefaultConnection");
        ////    var connectionString = EncryptionHelper.DBDecrypt(encryptedConnection);
        ////    return Configuration[connectionString];


        ////    return Configuration["ConnectionStrings:DefaultConnection"];
        ////}
        public DataSet returnDataSet(CommandType cmdType, string CommandName, string[] Parameters, string[] DbTypes, string[] ParameterTypes, string[] Values, string[] Lengths)
        {
            DataSet ds = new DataSet();

            try
            {

                string ParameterName = "";
                SqlDbType ParameterSqlDbType = SqlDbType.NVarChar;
                int ParameterLength = 8000;


                SqlCommand cmd = new SqlCommand(CommandName, DbCon);
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = 1000;

                for (int i = 0; i < Parameters.Length; i++)
                {
                    // ParameterTypes

                    ParameterName = Parameters.GetValue(i).ToString();
                    ParameterSqlDbType = getSqlDbType(DbTypes.GetValue(i).ToString());
                    ParameterLength = int.Parse(Lengths.GetValue(i).ToString());

                    addSqlParameter(cmd, ParameterName, ParameterSqlDbType, ParameterLength, Values.GetValue(i).ToString(), ParameterTypes.GetValue(i).ToString());

                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                DataTable table = new DataTable();
                table.Columns.Add("TYPE", typeof(string));
                table.Columns.Add("ERROR", typeof(string));

                table.Rows.Add("ERROR", ex.Message);
                table.Rows.Add("0", "ERROR : " + ex.Message);

                ds.Tables.Add(table);

            }
            finally
            {
                if (DbCon.State == ConnectionState.Open)
                    DbCon.Close();
            }
            return ds;
        }

        public string executeNonQuerry(CommandType cmdType, string CommandName, string[] Parameters, string[] DbTypes, string[] ParameterTypes, string[] Values, string[] Lengths)
        {
            string str = "";

            try
            {
                string ParameterName = "";
                SqlDbType ParameterSqlDbType = SqlDbType.NVarChar;
                int ParameterLength = 8000;

                SqlCommand cmd = new SqlCommand(CommandName, DbCon);
                cmd.CommandType = cmdType;

                for (int i = 0; i < Parameters.Length; i++)
                {
                    // ParameterTypes

                    ParameterName = Parameters.GetValue(i).ToString();
                    ParameterSqlDbType = getSqlDbType(DbTypes.GetValue(i).ToString());
                    ParameterLength = int.Parse(Lengths.GetValue(i).ToString());

                    addSqlParameter(cmd, ParameterName, ParameterSqlDbType, ParameterLength, Values.GetValue(i).ToString(), ParameterTypes.GetValue(i).ToString());

                }


                if (DbCon.State != ConnectionState.Open)
                    DbCon.Open();

                cmd.ExecuteNonQuery();

                str = "1";

                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                        str = str + "~" + cmd.Parameters[i].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                str = "0~" + ex.Message;
            }
            finally
            {
                if (DbCon.State != ConnectionState.Closed)
                    DbCon.Close();
            }

            return str;

        }

        private SqlDbType getSqlDbType(string ParameterDbType)
        {
            SqlDbType dbType;
            switch (ParameterDbType.ToLower())
            {
                case "int":
                    dbType = SqlDbType.Int;
                    break;

                case "varchar":
                    dbType = SqlDbType.VarChar;
                    break;

                case "nvarchar":
                    dbType = SqlDbType.NVarChar;
                    break;

                case "decimal":
                    dbType = SqlDbType.Decimal;
                    break;

                case "binary":
                    dbType = SqlDbType.Binary;
                    break;

                case "float":
                    dbType = SqlDbType.Float;
                    break;

                case "datetime":
                    dbType = SqlDbType.DateTime;
                    break;

                case "image":
                    dbType = SqlDbType.Image;
                    break;

                case "xml":
                    dbType = SqlDbType.Xml;
                    break;

                default:
                    dbType = SqlDbType.NVarChar;
                    break;

            }
            return dbType;
        }

        private void addSqlParameter(SqlCommand cmd, string Name, SqlDbType dbType, int Length, string Value, string ParameterType)
        {
            SqlParameter par = new SqlParameter(Name, dbType);

            switch (dbType)
            {
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                    par.Size = Length;
                    break;

                case SqlDbType.Decimal:
                    par.Precision = 18;
                    par.Scale = 2;
                    break;

            }
            if (ParameterType == "output")
            {
                par.Direction = ParameterDirection.Output;

            }
            else
            {

                switch (dbType)
                {
                    case SqlDbType.Int:
                        par.Value = int.Parse(Value);
                        break;

                    case SqlDbType.VarChar:
                    case SqlDbType.NVarChar:
                        par.Value = Value;
                        break;

                    case SqlDbType.Decimal:
                        par.Value = decimal.Parse(Value);
                        break;

                    case SqlDbType.Binary:
                        par.Value = bool.Parse(Value);
                        break;

                    case SqlDbType.Float:
                        par.Value = float.Parse(Value);
                        break;

                    case SqlDbType.DateTime:
                        par.Value = DateTime.Parse(Value);
                        break;

                    case SqlDbType.Image:
                        par.Value = byte.Parse(Value);
                        break;

                    //case SqlDbType.Xml:
                    //    par.Value =string.par Value;
                    //    break;

                    default:
                        par.Value = Value;
                        break;
                }
            }

            cmd.Parameters.Add(par);

        }

        public DataTable returnDataTable(CommandType cmdType, string CommandName, string[] Parameters, string[] DbTypes, string[] ParameterTypes, string[] Values, string[] Lengths)
        {
            DataTable ds = new DataTable();

            try
            {

                string ParameterName = "";
                SqlDbType ParameterSqlDbType = SqlDbType.NVarChar;
                int ParameterLength = 8000;

                SqlCommand cmd = new SqlCommand(CommandName, DbCon);
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = 1000;

                for (int i = 0; i < Parameters.Length; i++)
                {
                    // ParameterTypes

                    ParameterName = Parameters.GetValue(i).ToString();
                    ParameterSqlDbType = getSqlDbType(DbTypes.GetValue(i).ToString());
                    ParameterLength = int.Parse(Lengths.GetValue(i).ToString());

                    addSqlParameter(cmd, ParameterName, ParameterSqlDbType, ParameterLength, Values.GetValue(i).ToString(), ParameterTypes.GetValue(i).ToString());

                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                DataTable table = new DataTable();
                table.Columns.Add("TYPE", typeof(string));
                table.Columns.Add("ERROR", typeof(string));

                table.Rows.Add("ERROR", ex.Message);
                table.Rows.Add("0", "ERROR : " + ex.Message);

                ds = table;
            }
            finally
            {
                if (DbCon.State == ConnectionState.Open)
                    DbCon.Close();
            }
            return ds;
        }

        //==========================================================================================

        public DataTable Execute_Non_Querry1(CommandType cmdType, string CommandName, SqlParameter[] Parameters)
        {

            int r = 0;
            DataTable table = new DataTable();

            table.Columns.Add("PARAMETER", typeof(string));
            table.Columns.Add("VALUE", typeof(string));

            try
            {

                SqlCommand cmd = new SqlCommand(CommandName, DbCon);
                cmd.CommandType = cmdType;

                for (int i = 0; i < Parameters.Length; i++)
                {
                    cmd.Parameters.Add(Parameters[i]);
                }


                if (DbCon.State == ConnectionState.Closed)
                    DbCon.Open();

                cmd.ExecuteNonQuery();


                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        table.Rows.Add(cmd.Parameters[i].ParameterName, cmd.Parameters[i].Value);
                    }
                }

            }
            catch (Exception ex)
            {
                table.Rows.Add("ERROR", ex.Message);
            }
            finally
            {
                if (DbCon.State == ConnectionState.Open)
                    DbCon.Close();
            }

            return table;

        }

        public DataSet Return_Dataset(CommandType cmdType, string CommandName, SqlParameter[] Parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(CommandName, DbCon);
                cmd.CommandType = cmdType;

                for (int i = 0; i < Parameters.Length; i++)
                {
                    cmd.Parameters.Add(Parameters[i]);
                }
                if (DbCon.State != ConnectionState.Open)
                    DbCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                DataTable table = new DataTable();
                table.Columns.Add("TOTAL", typeof(int));
                table.Columns.Add("TYPE", typeof(string));
                table.Columns.Add("ERROR", typeof(string));

                table.Rows.Add(0, "ERROR", ex.Message);

                ds.Tables.Add(table);

            }



            finally
            {
                //DbCon.Dispose();
                if (DbCon.State != ConnectionState.Closed)
                    DbCon.Close();

            }

            return ds;
        }


        public object ExecuteScalar(CommandType cmdType, string CommandName, SqlParameter[] Parameters)
        {

            try
            {

                SqlCommand cmd = new SqlCommand(CommandName, DbCon);
                cmd.CommandType = cmdType;

                for (int i = 0; i < Parameters.Length; i++)
                {
                    cmd.Parameters.Add(Parameters[i]);
                }


                if (DbCon.State != ConnectionState.Open)
                    DbCon.Open();

                return cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                return "ERROR";
            }
            finally
            {
                //                if (DbCon.State == ConnectionState.Open)
                if (DbCon.State != ConnectionState.Closed)
                    DbCon.Close();
            }

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


    }
}
