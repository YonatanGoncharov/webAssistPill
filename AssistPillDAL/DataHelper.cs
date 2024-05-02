using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace FinalProjectDAL
{  /// <summary>
   /// Summary description for DBHelper
   /// this class manege the connection to the Data Bage
   /// it can return a query resault ro exequte a query;
   /// </summary>
    public static class DBHelper
    {
        private static readonly string connectionstring = $@"Provider=Microsoft.ACE.OLEDB.12.0;
                                                          Data Source= C:\webAssistPill\webAssistPill\AssistPillDAL\Data\FinalProjectDB.accdb;
                                                          Persist Security Info=False;";
        private static OleDbConnection GetConnection()
        {
            OleDbConnection conn = new OleDbConnection();
            // add a connection string to the connection
            conn.ConnectionString = connectionstring;
            try
            {
                //open the connection to the data base
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// The GetDataSet method returns a .NET DataSet object that contains a DataTable.  
        /// The DataTable contains the results for each of the rules in the RET rule set that was checked.
        /// </summary>
        /// <param name="query">sql select query</param>
        /// <returns>Dataset of all the the datatables in the query</returns>
        public static DataSet GetDataSet(string query)
        {
            try
            {
                OleDbConnection connection = GetConnection();
                if (connection == null)
                    return null;

                OleDbCommand c = new OleDbCommand(query, connection);
                OleDbDataAdapter d = new OleDbDataAdapter(c);
                DataSet ds = new DataSet();
                d.Fill(ds);
                connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                //catch the error (if there is an error while connecting.
                return null;

            }
        }
        /// <summary>
        /// return a data table frm the data base basen on an SQL query from the user
        /// </summary>
        /// <param name="query">an SQL QUERY</param>
        /// <returns>Data Table: one Data Table</returns>
        public static DataTable GetDataTable(string query)
        {
            try
            {
                OleDbConnection connection = GetConnection();
                if (connection == null)
                    return null;

                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataAdapter d = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                d.Fill(ds);
                connection.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                //catch the error (if there is an error while connecting.
                return null;

            }
        }

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected. 
        /// </summary>
        /// <param name="sql">sql statment </param>
        /// <returns>number of rows affected</returns>
        public static int ExecuteNonQuery(string sql)
        {
            try
            {
                OleDbConnection connection = GetConnection();
                if (connection == null)
                    return -1;
                OleDbCommand command = new OleDbCommand(sql, connection);
                //   connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                //catch the error (if there is an error while connecting.
                return -1;

            }
        }
    }
}

