using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DustDtfManagers.Managers
{
    /// <summary>
    /// allows very fast bulk reads and writes to a sql server database
    /// </summary>
    public class BulkSqlManager
    {
        //private int sqlTimeout;
        private string connectionString;
        private int bulkWriteSize;

        public BulkSqlManager(string connectionString, int bulkWriteSize)
        {
            //this.sqlTimeout = sqlTimeout;
            this.connectionString = connectionString;
            this.bulkWriteSize = bulkWriteSize;
        }

        public SqlConnection GetSqlConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
                return null;//for testing

            return new SqlConnection(connectionString);
        }

        public void RunSql(string sql)
        {
            var con = GetSqlConnection();
            try
            {
                con.Open();
                var cmd = new SqlCommand(sql, con);//todo note possible sql injection
                cmd.CommandTimeout = con.ConnectionTimeout;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Dispose();
            }
        }

        public object RunSqlWithResult(string sql)
        {
            var con = GetSqlConnection();
            if (con == null)
                return 0;//for testing
            try
            {
                con.Open();
                var cmd = new SqlCommand(sql, con);//todo note possible sql injection
                cmd.CommandTimeout = con.ConnectionTimeout;
                var obj = cmd.ExecuteScalar();
                return obj;
            }
            catch
            {
                throw;
            }

            finally
            {
                con.Dispose();
            }
        }

        public void Truncate(string table)
        {
            var sql = "TRUNCATE TABLE " + table;
            RunSql(sql);
        }

        /// <summary>
        /// save a list from any class using generics - also supported by java
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="tableName"></param>
        public void BulkSave<T>(List<T> list, string tableName)
        {
            var dataTable = ToDataTable(list);

            var dbConn = GetSqlConnection();
            try
            {
                dbConn.Open();
                SqlBulkCopy bulkInsert = new SqlBulkCopy(dbConn);
                bulkInsert.BatchSize = bulkWriteSize;
                bulkInsert.BulkCopyTimeout = dbConn.ConnectionTimeout;
                bulkInsert.DestinationTableName = tableName;
                bulkInsert.WriteToServer(dataTable);
            }
            catch
            {
                throw;
            }
            finally
            {
                dbConn.Dispose();
            }

        }
        public int GetTableCount(string table)
        {
            var obj = RunSqlWithResult("select count(*) from " + table + " with (nolock)");
            return Convert.ToInt32(obj);
        }

        public void Close(SqlDataReader reader)
        {
            reader.Close();
            GC.Collect();
        }

        public SqlDataReader BulkReadAll(string table)
        {
            return BulkRead("select * from " + table + " with (nolock)");
        }

        public SqlDataReader BulkRead(string sql)
        {
            var con = GetSqlConnection();
            if (con == null)
                return null;//for testing
            try
            {
                con.Open();
                var cmd = new SqlCommand(sql, con);//todo note possible sql injection
                cmd.CommandTimeout = con.ConnectionTimeout;
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                throw;
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
