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

    //身份基数
    public class AppraisalBases
    {
        public int Id { get; set; }
        public string BaseType { get; set; }
        public int AppraisalBase { get; set; }
        public int IsDel { get; set; }


        // 执行查询 并 返回 所有 身份基数
        public static List<AppraisalBases> ListAll()
        {
            List<AppraisalBases> appraisalBasesList = new List<AppraisalBases>();

            string query = "SELECT Id, BaseType, AppraisalBase, IsDel FROM AppraisalBases WHERE IsDel = 0";  // 只想查询未删除的记录

            // 执行查询并返回 DataTable
            DataTable dt = SqlHelper.ExecuteDataTable(query);

            // 将 DataTable 中的数据映射到 List<AppraisalBases>
            foreach (DataRow row in dt.Rows)
            {
                appraisalBasesList.Add(row.DataRowToModel<AppraisalBases>());
                //AppraisalBases appraisalBase = MapToAppraisalBase(row);  // 调用映射方法
                //appraisalBasesList.Add(appraisalBase);
            }

            return appraisalBasesList;
        }
    }
}
