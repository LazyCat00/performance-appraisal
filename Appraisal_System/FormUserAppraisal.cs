using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Appraisal_System.Models;

namespace Appraisal_System
{
    public partial class FormUserAppraisal : Form
    {

        //只是用于执行一个无参数、无返回值的操作，使用 Action 更简洁。
        Action bindDgv;
        public FormUserAppraisal()
        {
            InitializeComponent();
        }

        private void FormUserAppraisal_Load(object sender, EventArgs e)
        {
            SetCol();
            BindDgvUserAppraisal();
            bindDgv = BindDgvUserAppraisal;
        }

        private void BindDgvUserAppraisal()
        {
            // 获取需要被扩展的表（dt）
            DataTable dtUserApprasial = UserAppraisalBase.GetDtJoinAppraisal();
            // 获取系数表集合（也就是需要扩展的）
            List<AppraisalCoefficients> appraisalCoefficients = AppraisalCoefficients.ListAll();

            // 通过系数表来填充 dtUserApprasial(dt)
            foreach (var item in appraisalCoefficients)
            {
                // 添加系数名列
                dtUserApprasial.Columns.Add(new DataColumn
                {
                    ColumnName = "AppraisalTypeKey" + item.Id
                });

                dtUserApprasial.Columns.Add(new DataColumn
                {
                    ColumnName = "AppraisalCoefficient" + item.Id
                });

                dtUserApprasial.Columns.Add(new DataColumn
                {
                    ColumnName = "CalculationMethodKey" + item.Id
                });
            }

            // 年度考核
            dtUserApprasial.Columns.Add(new DataColumn
            {
                ColumnName = "AssessmentYear"
            });

            // 实发年终奖
            dtUserApprasial.Columns.Add(new DataColumn
            {
                ColumnName = "YearBonus"
            });

            // 给 dtUserApprasial 填充数据
            List<UserAppraisalCoefficients> userAppraisalCoefficients = UserAppraisalCoefficients.ListAll();
            for (int i = 0; i < dtUserApprasial.Rows.Count; i++)
            {
                var uacFilter = userAppraisalCoefficients.FindAll(m => m.UserId == (int)dtUserApprasial.Rows[i]["Id"] && m.AssessmentYear == Convert.ToInt32(cbxYear.Text));
                //
                double[] yearBonusArray = new double[uacFilter.Count];


                for (int j = 0; j < uacFilter.Count; j++)
                {
                    //获取考核次数
                    string appraisalTypeKey = "AppraisalTypeKey" + uacFilter[j].CoefficientId;
                    int appraisalTypeCountValue = uacFilter[j].Count;

                    //获取考核系数
                    string appraisalCoefficientKey = "AppraisalCoefficient" + uacFilter[j].CoefficientId;
                    int appraisalCoefficientValue = uacFilter[j].AppraisalCoefficient;

                    //获取计算方式
                    string calculationMethodKey = "CalculationMethodKey" + uacFilter[j].CoefficientId;
                    int calculationMethodValue = (int)uacFilter[j].CalculationMethod;

                    dtUserApprasial.Rows[i][appraisalTypeKey] = appraisalTypeCountValue;
                    dtUserApprasial.Rows[i][appraisalCoefficientKey] = appraisalCoefficientValue;
                    dtUserApprasial.Rows[i][calculationMethodKey] = calculationMethodValue;

                    yearBonusArray[j] = appraisalCoefficientValue * appraisalTypeCountValue * calculationMethodValue;
                }

                dtUserApprasial.Rows[i]["AssessmentYear"] = cbxYear.Text;

                double yearBonusAll = 0;
                for (int j = 0; j < yearBonusArray.Length; j++)
                {
                    yearBonusAll += yearBonusArray[j];
                }

                double yearBonus = (1 + yearBonusAll) * Convert.ToDouble(dtUserApprasial.Rows[i]["AppraisalBase"]);



                dtUserApprasial.Rows[i]["YearBonus"] = yearBonus < 0 ? 0 : yearBonus;
            }

            dgvUserAppraisal.AutoGenerateColumns = false;
            dgvUserAppraisal.DataSource = dtUserApprasial;
        }

        private void SetCol()
        {
            List<AppraisalCoefficients> appraisalCoefficients = AppraisalCoefficients.ListAll();
            List<DataGridViewTextBoxColumn> dataGridViewTextBoxColumns = new List<DataGridViewTextBoxColumn>();

            foreach (var appraisalCoefficient in appraisalCoefficients)
            {
                // 确保列名与 FormUserAppraisal_Load 中一致
                dataGridViewTextBoxColumns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = appraisalCoefficient.AppraisalType,
                        Name = "AppraisalTypeKey" + appraisalCoefficient.Id, // 确保列名一致
                        DataPropertyName = "AppraisalTypeKey" + appraisalCoefficient.Id, // 确保列名一致
                        ReadOnly = true,
                        Width = 88
                    }
                );

                //dataGridViewTextBoxColumns.Add(
                //    new DataGridViewTextBoxColumn
                //    {
                //        HeaderText = "系数",
                //        Name = "AppraisalCoefficient" + appraisalCoefficient.Id,
                //        DataPropertyName = "AppraisalCoefficient" + appraisalCoefficient.Id, 
                //        ReadOnly = true,
                //        Width = 88
                //    }
                //);

                //dataGridViewTextBoxColumns.Add(
                //    new DataGridViewTextBoxColumn
                //    {
                //        HeaderText = "计算方式",
                //        Name = "CalculationMethodKey" + appraisalCoefficient.Id, 
                //        DataPropertyName = "CalculationMethodKey" + appraisalCoefficient.Id, 
                //        ReadOnly = true,
                //        Width = 88
                //    }
                //);
            }

            // 添加 'AssessmentYear' 和 'YearBonus' 列
            dataGridViewTextBoxColumns.Add(
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "考核年度",
                    Name = "AssessmentYear",
                    DataPropertyName = "AssessmentYear",
                    ReadOnly = true,
                    Width = 88
                }
            );

            dataGridViewTextBoxColumns.Add(
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "实发年终奖",
                    Name = "YearBonus",
                    DataPropertyName = "YearBonus",
                    ReadOnly = true,
                    Width = 88
                }
            );

            dgvUserAppraisal.Columns.AddRange(dataGridViewTextBoxColumns.ToArray());
        }

        private void dgvUserAppraisal_MouseDown(object sender, MouseEventArgs e)
        {
            tsmEdit.Visible = false;
        }
        private void dgvUserAppraisal_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //被右键
            if (e.Button == MouseButtons.Right)
            {
                //如果单元格被选中
                if (e.RowIndex>-1)
                {
                    //清除所有单元格样式
                    dgvUserAppraisal.ClearSelection();
                    //被选中的单元格高亮（被选中）
                    dgvUserAppraisal.Rows[e.RowIndex].Selected = true;
                    tsmEdit.Visible = true;
                }    
            }
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int year =Convert.ToInt32(cbxYear.Text) ;
            int userId = (int)dgvUserAppraisal.SelectedRows[0].Cells["Id"].Value;
            FormUserAppraisalEdit formUserAppraisalEdit = new FormUserAppraisalEdit(userId,year,bindDgv);
            formUserAppraisalEdit.ShowDialog();
        }
    }
}
