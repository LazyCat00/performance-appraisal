using Appraisal_System.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appraisal_System.Models
{
    //员工
   public class UserAppraisalBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public int BaseTypeId { get; set; }
        public string BaseType { get; set; }
        public int AppraisalBase { get; set; }
        public int IsDel { get; set; }

        public static DataTable GetDtJoinAppraisal()
        {
            // 左外连接 查询 有身份基数的 员工
            string query = "SELECT  u.Id, u.UserName, u.Sex, u.Password, u.BaseTypeId, u.IsDel, a.BaseType,a.AppraisalBase FROM Users u LEFT JOIN AppraisalBases a ON u.BaseTypeId = a.Id;";

            // 执行查询并返回 DataTable
            DataTable dt = SqlHelper.ExecuteDataTable(query);
            return dt;
        }

        // 左外连接查询 并 返回 员工
        public static List<UserAppraisalBase> GetListJoinAppraisal()
        {
            List<UserAppraisalBase> users = new List<UserAppraisalBase>();

            // 执行查询并返回 DataTable
            DataTable dt = GetDtJoinAppraisal();

            // 将 DataTable 中的数据映射到 List<AppraisalBases>
            foreach (DataRow row in dt.Rows)
            {
                users.Add(row.DataRowToModel<UserAppraisalBase>());           
            }

            return users;
        }

        public static int Insert(UserAppraisalBase user)
        {
            // SQL 插入语句
            string sql = "INSERT INTO Users (UserName, Sex, Password, BaseTypeId, IsDel) " +
                         "VALUES (@UserName, @Sex, @Password, @BaseTypeId, @IsDel);";
          int row =  SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@UserName", user.UserName), new SqlParameter("@Sex", user.Sex), new SqlParameter("@Password", user.Password),
                new SqlParameter("@BaseTypeId", user.BaseTypeId), new SqlParameter("@IsDel", user.IsDel));

            return row;
            

        }
        public static int Update(UserAppraisalBase user)
        {
            // SQL Update query
            string sql = "UPDATE Users SET UserName = @UserName, Sex = @Sex, Password = @Password, BaseTypeId = @BaseTypeId, IsDel = @IsDel " +
                         "WHERE Id = @Id;";

            // Execute the update query
            int row = SqlHelper.ExecuteNonQuery(sql,
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Sex", user.Sex),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@BaseTypeId", user.BaseTypeId),
                new SqlParameter("@IsDel", user.IsDel),
                new SqlParameter("@Id", user.Id));
            // Return the number of rows affected
            return row;
        }


    }
}
