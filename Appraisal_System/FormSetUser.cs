using Appraisal_System.Models;
using Appraisal_System.Utils;
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
    
    public partial class FormSetUser: Form
    {
        private DelbindDgv _delBindDgv;
        private UserAppraisalBase _user;
        public FormSetUser(DelbindDgv delBindDgv)
        {
            InitializeComponent();
            _delBindDgv = delBindDgv;
        }

        public FormSetUser(DelbindDgv delBindDgv,int userId):this(delBindDgv)
        {
            _user = UserAppraisalBase.GetListJoinAppraisal().Find(m => m.Id == userId);
           
           
        }


        private void FormSetUser_Load(object sender, EventArgs e)
        {
            List<AppraisalBases> appraisalBases = AppraisalBases.ListAll();
            cbxBase.DataSource = appraisalBases;
            cbxBase.DisplayMember = "BaseType";
            cbxBase.ValueMember = "Id";

            txtUserName.Text = _user.UserName;
            cbxBase.SelectedValue = _user.BaseTypeId;
            cbxSex.Text = _user.Sex;
            if (_user.IsDel == 1)
            {
                checkIsStop.Checked = true;
            }
            else
            {
                checkIsStop.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            int baseTypeInt = (int)cbxBase.SelectedValue;
            string sex = cbxSex.Text;
            int isDel;
            if (checkIsStop.Checked)
            {
                isDel = 1;
            }
            else
            {
                isDel = 0;
            }

            if(_user == null)
            {
                UserAppraisalBase user = new UserAppraisalBase
                {
                    UserName = userName,
                    Password = "123456",
                    Sex = sex,
                    IsDel = isDel,
                    BaseTypeId = baseTypeInt,

                };
                UserAppraisalBase.Insert(user);
                //在新增员工完成时触发 委托 来更新父窗体的数据绑定
                
                MessageBox.Show("用户添加成功！");
                
            }
            else
            {
                _user.UserName=userName;
                _user.BaseTypeId=baseTypeInt;
                _user.IsDel = isDel;
                _user.Sex=sex;
                if (_user.IsDel == 1)
                {
                    checkIsStop.Checked = true;
                }
                else
                {
                    checkIsStop.Checked = false;
                }
             int row = UserAppraisalBase.Update(_user);
             MessageBox.Show("用户修改成功！");
            }
            _delBindDgv();
            this.Close();
        }
    }
}
