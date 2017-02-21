using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SMS_Server.Models
{
    public class DBModels : DbContext
    {
        //variable connection
        protected SqlConnection con;
        
        

        //open connecttion
        public bool Open(string Connection = "DefaultConnection")
        {
            con = new SqlConnection(@WebConfigurationManager.ConnectionStrings[Connection].ToString());
            try
            {
                bool b = true;
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                return b;
            }
            catch (SqlException ex)
            {
                return false;
            }

        }
        //end Opend connection


        //close connection
        public bool Close()
        {
            try
            {
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public int ToInt(object s)
        {
            try
            {
                return Int32.Parse(s.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public int DataInsert(string sql)
        {
            string query = sql;//+ ";SELECT @@Identifity";
            int LastID = 0;
            try
            {
                if(con.State.ToString()=="Open")
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    //cmd.ExecuteNonQuery();
                    LastID = this.ToInt(cmd.ExecuteScalar());
                }
                return this.ToInt(LastID);
            }
            catch
            {
                return 0;
            }
        }

      //get table
      public DataTable GetTable(string query)
        {
            //string query = sql;
            DataTable dataTable = new DataTable();
            try
            {
                if(con.State.ToString()=="Open")
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    return dataTable;
                }
                else
                {
                    return dataTable;
                }

            }
            catch
            {
                return dataTable;
            }
        }
        public DataRow GetRow(string sql)
        {
            DataTable d = GetTable(sql);
            DataRow row = d.NewRow();
            if(d.Rows.Count>0)
            {
                row = d.Rows[0];
            }
            return row;
        }
     
        public string GetValue(string sql)
        {
            DataRow row = GetRow(sql);
            return row[0].ToString();

        }
        public DataTable GetTable(string sql, Dictionary<string, string> FIEL_OVER_ORDER, int from_numrow, int to_numrow)
        {
            int k = 0;
            string FILD_ORDER = "";
            foreach (KeyValuePair<string, string> entery in FIEL_OVER_ORDER)
            {
                k++;
                FILD_ORDER += (k != 1 ? "," : "") + "(" + entery.Key + ")" + entery.Value;
            }
            from_numrow++;
            to_numrow++;
            string query = "SELECT * FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY" + FILD_ORDER + ")AS RowNo FROM(" + sql + ")x) d_limit WHERE d_limit.RowNo BETWEEN"+from_numrow+"AND"+to_numrow+"";

            DataTable d = new DataTable();
            if(con.State.ToString()=="Open")
            {
                SqlCommand cmd = new SqlCommand(query,con);
                SqlDataReader reader = cmd.ExecuteReader();
                d.Load(reader);

                return d;

            }
            else
            {
                return d;
            }


        }


    }
}