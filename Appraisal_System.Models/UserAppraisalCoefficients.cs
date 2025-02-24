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
    public class UserAppraisalCoefficients
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoefficientId { get; set; }
        public int Count { get; set; }
        public int AssessmentYear { get; set; }
        public string AppraisalType { get; set; }
        public int AppraisalCoefficient { get; set; }
        public int CalculationMethod{ get; set; }
        public int IsDel { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, UserId: {UserId}, CoefficientId: {CoefficientId}, Count: {Count}, AssessmentYear: {AssessmentYear}, AppraisalType: {AppraisalType}, AppraisalCoefficient: {AppraisalCoefficient}, CalculationMethod: {CalculationMethod}, IsDel: {IsDel}";
        }

        public static List<UserAppraisalCoefficients> ListAll()
        {
            List<UserAppraisalCoefficients> userAppraisalCoefficients = new List<UserAppraisalCoefficients>();

            string sql = "SELECT ua.*,ac.AppraisalType,ac.AppraisalCoefficient,ac.CalculationMethod FROM UserAppraisals ua INNER JOIN AppraisalCoefficients ac ON ua.CoefficientId = ac.Id WHERE ua.IsDel = 0 AND ac.IsDel = 0;";

            // 执行查询并返回 DataTable
            DataTable dt = SqlHelper.ExecuteDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                userAppraisalCoefficients.Add(row.DataRowToModel<UserAppraisalCoefficients>());
            
            }
            return userAppraisalCoefficients;
        }

        public static List<UserAppraisalCoefficients> ListByUserIdAndYear(int userId, int year)
        {
            // 初始化返回的列表
            List<UserAppraisalCoefficients> userAppraisalCoefficients = new List<UserAppraisalCoefficients>();

            // 构建 SQL 查询
            //字符串前面的 @ 符号是** 逐字字符串字面量（verbatim string literal）**的标识符。它的作用是：
            //1.逐字字符串字面量的作用
            //忽略转义字符：在普通字符串中，反斜杠 \ 是转义字符（如 \n 表示换行，\t 表示制表符）。而在逐字字符串中，反斜杠会被当作普通字符处理。
            //保留换行和空格：逐字字符串可以跨越多行，并保留字符串中的换行符和缩进。
            string sql = @"
        SELECT ua.*, ac.AppraisalType, ac.AppraisalCoefficient, ac.CalculationMethod 
        FROM UserAppraisals ua 
        INNER JOIN AppraisalCoefficients ac ON ua.CoefficientId = ac.Id 
        WHERE ua.IsDel = 0 
          AND ac.IsDel = 0 
          AND ua.UserId = @UserId 
          AND ua.AssessmentYear = @Year;";

            // 定义 SQL 参数
            SqlParameter[] parameters = {
        new SqlParameter("@UserId", userId),
        new SqlParameter("@Year", year)
    };

            // 执行查询并获取 DataTable
            DataTable dt = SqlHelper.ExecuteDataTable(sql, parameters);

            // 遍历 DataTable，将每一行转换为 UserAppraisalCoefficients 对象
            foreach (DataRow row in dt.Rows)
            {
                UserAppraisalCoefficients userAppraisal = new UserAppraisalCoefficients
                {
                    // 假设 UserAppraisalCoefficients 类的属性与查询结果的列名一致
                    Id = Convert.ToInt32(row["Id"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    Count = Convert.ToInt32(row["Count"]),
                    CoefficientId = Convert.ToInt32(row["CoefficientId"]),
                    AssessmentYear = Convert.ToInt32(row["AssessmentYear"]),
                    AppraisalType = row["AppraisalType"].ToString(),
                    AppraisalCoefficient = Convert.ToInt32(row["AppraisalCoefficient"]),
                    CalculationMethod = Convert.ToInt32(row["CalculationMethod"])
                };

                // 将对象添加到列表中
                userAppraisalCoefficients.Add(userAppraisal);
            }

            // 返回结果
            return userAppraisalCoefficients;
        }

    

        public static bool Update(UserAppraisalCoefficients userAppraisal)
        {
            string sql = @"
                UPDATE UserAppraisals 
                SET 
                    Count = @Count, 
                    AssessmentYear = @AssessmentYear    
                WHERE UserId = @UserId 
                  AND CoefficientId = @CoefficientId;";

            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userAppraisal.UserId),
                new SqlParameter("@CoefficientId", userAppraisal.CoefficientId),
                new SqlParameter("@Count", userAppraisal.Count),
                new SqlParameter("@AssessmentYear", userAppraisal.AssessmentYear),
            };

            int rowsAffected = SqlHelper.ExecuteNonQuery(sql, parameters);

            return rowsAffected > 0;
        }
    }
}
