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
    public partial class FormBaseManager: Form
    {
        public FormBaseManager()
        {
            InitializeComponent();
        }

        private void FormBaseManager_Load(object sender, EventArgs e)
        {
            dgvBase.DataSource = AppraisalBases.ListAll();
        }
    }
}
