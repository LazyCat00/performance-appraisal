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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Appraisal_System
{
    //delegate 关键字：用来声明一个委托类型。
    //void 表示委托所引用的方法没有返回值
    //()空括号表示这个委托没有参数，即它指向的方法不接受任何参数。
    public delegate void DelbindDgv();//声明一个 委托 类型 DelbindDgv
    public partial class FormUserManager: Form
    {
        DelbindDgv delbindDgv;
       
        public FormUserManager()
        {
            InitializeComponent();
        }

        private void FormUserManager_Load(object sender, EventArgs e)
        {

            BindCbx();

            BindDgv();

            delbindDgv = BindDgv;
        }

        private void BindCbx()
        {
            List<AppraisalBases> appraisalBases = AppraisalBases.ListAll();
            appraisalBases.Insert(0, new AppraisalBases
            {
                Id = 0,
                BaseType = "--查询所有--",
                AppraisalBase = 0,
                IsDel = 0

            });
            cbxBase.DataSource = appraisalBases;
            cbxBase.DisplayMember = "BaseType";
            cbxBase.ValueMember = "Id";
        }

        //绑定数据到 DataGridView
        private void BindDgv()
        {
            string userName = txtUserName.Text.Trim();
            //获取身份
            int baseTypeInt = (int)cbxBase.SelectedValue;
            //是否已停职
            bool isDel = checkIsDel.Checked;
            // 取消自动填充
            dgvUserAppraisal.AutoGenerateColumns = false;

            if (baseTypeInt == 0)
            {
                dgvUserAppraisal.DataSource = UserAppraisalBase.GetListJoinAppraisal().FindAll(m => m.UserName.Contains(userName));
            }
            else
            {
                dgvUserAppraisal.DataSource = UserAppraisalBase.GetListJoinAppraisal().FindAll(m => m.UserName.Contains(userName) && m.BaseTypeId == baseTypeInt);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDgv();
        }

        private void dgvUserAppraisal_MouseDown(object sender, MouseEventArgs e)
        {
            //判断是否为右键
            if (e.Button == MouseButtons.Right)
            {
                tsmAdd.Visible = true;
                tsmEdit.Visible = false;
                tsmStart.Visible = false;
                tsmStop.Visible = false;
            }
        }

        private void dgvUserAppraisal_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex>-1)
                {
                    int isDel =(int) dgvUserAppraisal.SelectedRows[0].Cells["IsDel"].Value;
                    if (isDel == 0)
                    {
                        tsmStop.Visible = true;
                    }else
                    {
                        tsmStart.Visible = true;
                    }
                        dgvUserAppraisal.Rows[e.RowIndex].Selected = true;
                    tsmAdd.Visible = true;
                    tsmEdit.Visible = true;
                 
                }
            }
        }

        //按下新建后
        private void tsmAdd_Click(object sender, EventArgs e)
        {
            FormSetUser formSetUser = new FormSetUser(delbindDgv);
            formSetUser.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int userId =(int) dgvUserAppraisal.SelectedRows[0].Cells["Id"].Value;
            FormSetUser formSetUser = new FormSetUser(delbindDgv,userId);
            formSetUser.ShowDialog();
        }
    }
}
