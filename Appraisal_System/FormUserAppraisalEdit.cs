using Appraisal_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appraisal_System
{
    public partial class FormUserAppraisalEdit: Form
    {
        private int _userId;
        private int _year;
        private Action _bindDgv;
        private List<UserAppraisalCoefficients>  userAppraisalCoefficients;
        public FormUserAppraisalEdit()
        {
            InitializeComponent();
        }

        public FormUserAppraisalEdit(int userId,int year,Action bindDgv):this()
        {
            _userId = userId;
            _year = year;
            userAppraisalCoefficients = new List<UserAppraisalCoefficients>(); // 初始化集合
            _bindDgv = bindDgv;
        }

        private void FormUserAppraisalEdit_Load(object sender, EventArgs e)
        {
            CreateControls();
            List<UserAppraisalCoefficients> userAppraisalCoefficients = UserAppraisalCoefficients.ListByUserIdAndYear(_userId, _year);
            Console.WriteLine("userAppraisalCoefficients:\n" + string.Join("\n", userAppraisalCoefficients));

            foreach (var userAppraisalCoefficient in userAppraisalCoefficients)
            {
               
                //获取 FlowLayoutPanel（flp）控件中的所有子控件的集合
                var flCtrs = flp.Controls;
                foreach (Control flCtr in flCtrs)
                {
                    if(flCtr is Panel)
                    {

                        var plCtrs = flCtr.Controls;
                        foreach (var plCtr in plCtrs)
                        {
                            if (plCtr is TextBox)
                            {
                                int appraisalCoefficientId = Convert.ToInt32(((TextBox)plCtr).Name.Split('_')[1]);
                                // 打印 appraisalCoefficientId 的值
                                Console.WriteLine("appraisalCoefficientId: " + appraisalCoefficientId);
                                //Console.WriteLine("Count: " + userAppraisalCoefficients.Find(m => m.CoefficientId == appraisalCoefficientId).Count.ToString());
                                //查找匹配的元素
                                var foundItem = userAppraisalCoefficients.Find(m => m.CoefficientId == appraisalCoefficientId);

                                if (foundItem != null)
                                {
                                    ((TextBox)plCtr).Text = userAppraisalCoefficients.Find(m => m.CoefficientId == appraisalCoefficientId).Count.ToString();
                                }
                                else
                                {
                                    ((TextBox)plCtr).Text = "0";
                                }
                               

                            }
                        }
                        
                    }
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // 清空集合，避免重复添加
            userAppraisalCoefficients.Clear();
            var flCtrs = flp.Controls;
            
            foreach (Control flCtr in flCtrs)
            {
                if (flCtr is Panel)
                {

                    var plCtrs = flCtr.Controls;
                    foreach (var plCtr in plCtrs)
                    {
                        if (plCtr is TextBox)
                        {
                            UserAppraisalCoefficients userAppraisalCoefficient = new UserAppraisalCoefficients
                            {
                                UserId = _userId,
                                CoefficientId = Convert.ToInt32(((TextBox)plCtr).Name.Split('_')[1]),
                                AssessmentYear = _year,
                                Count = Convert.ToInt32(((TextBox)plCtr).Text)
                            };
                            userAppraisalCoefficients.Add(userAppraisalCoefficient);
                        }
                    }
                }
            }
            // 判断集合是否为空
            if (userAppraisalCoefficients.Any())
            {
                // 遍历集合并更新每个对象
                foreach (var userAppraisalCoefficient in userAppraisalCoefficients)
                {
                    // 调用 Update 方法
                    bool isUpdated = UserAppraisalCoefficients.Update(userAppraisalCoefficient);

                    if (!isUpdated)
                    {
                        MessageBox.Show($"更新失败：UserId = {userAppraisalCoefficient.UserId}, CoefficientId = {userAppraisalCoefficient.CoefficientId}");
                        return; // 如果更新失败，直接返回
                    }
                }

                // 更新成功
                MessageBox.Show("数据修改成功！");
                _bindDgv(); // 刷新数据
                this.Close();
            }
        }
        private void CreateControls()
        {
            List<AppraisalCoefficients> appraisalCoefficients = AppraisalCoefficients.ListAll();
            foreach (var appraisalCoefficient in appraisalCoefficients)
            {
                // 创建一个新的 Panel 控件，用于容纳其他控件
                Panel panel = new Panel();


                Label label = new Label
                {
                    // 设置 Label 的文本内容为 appraisalCoefficient.AppraisalType 的值
                    Text = appraisalCoefficient.AppraisalType,

                    // 设置 Label 的宽度为 60 像素
                    Width = 60,

                    // 设置 Label 的位置，X 坐标为 0，Y 坐标为 4（相对于其父容器）
                    Location = new Point(0, 4)
                };

                // 创建一个新的 TextBox 控件，用于用户输入文本
                TextBox textBox = new TextBox
                {
                    // 设置 TextBox 的位置，X 坐标为 66，Y 坐标为 0（相对于其父容器）
                    Location = new Point(66, 0),

                    // 设置 TextBox 的宽度为 120 像素
                    Width = 120,

                    // 设置 TextBox 的高度为 26 像素
                    Height = 26,
                    Name = "txtAppraisalType_" + appraisalCoefficient.Id,
                    //Text = userAppraisalCoefficient.Count.ToString()
                };

                panel.Controls.Add(label);
                panel.Controls.Add(textBox);
                flp.Controls.Add(panel);
            }
        }

       
    }
}
