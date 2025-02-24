using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Appraisal_System.Utils
{
    public class SqlHelper
    {
        public static string _connectionString { get; set; }

        //执行一个不返回结果集的SQL命令（如INSERT、UPDATE、DELETE等），并返回受影响的行数。
        //<param name = "query" > 要执行的SQL查询字符串（例如INSERT、UPDATE、DELETE等）
        //params 是 C# 中的一个关键字，它使得方法能够接受可变数量的参数
        //params 允许你传递 0 个、1 个或多个 SqlParameter 对象，而不需要显式地传递一个数组。
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // 执行查询，返回一个标量值（例如 COUNT, SUM）
        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        // 执行查询，返回 SqlDataReader（多行数据）
        public SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddRange(parameters);

            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        // 执行查询，返回 DataTable（多行数据，适合展示）
        public static DataTable ExecuteDataTable(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddRange(parameters);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}
