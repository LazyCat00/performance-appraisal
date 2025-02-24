using Appraisal_System.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appraisal_System.Models
{
    public class AppraisalCoefficients
    {
        public int Id { get; set; }
        public string AppraisalType { get; set; }
        public decimal AppraisalCoefficient { get; set; }
        public int CalculationMethod { get; set; }
        public int IsDel { get; set; }

        public static List<AppraisalCoefficients> ListAll()
        {
            List<AppraisalCoefficients> appraisalCoefficients = new List<AppraisalCoefficients>();
            string sql = "SELECT* FROM AppraisalCoefficients;";

            // 执行查询并返回 DataTable
            DataTable dt = SqlHelper.ExecuteDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                appraisalCoefficients.Add(row.DataRowToModel<AppraisalCoefficients>());
            }
            return appraisalCoefficients;
        }
    }
}
