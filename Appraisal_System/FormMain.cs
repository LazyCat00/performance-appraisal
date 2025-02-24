using Appraisal_System.Common;
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
   

    public partial class FormMain: Form
    {
        FormUserManager formUserManager;
        FormBaseManager formBaseManager;
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form form = FormFactory.CreateForm("FormUserManager");
            form.MdiParent = this;
            form.Parent = splitContainer1.Panel2;
            form.Show();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void trvMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in trvMenu.Nodes)
            {
                //设置当前节点的背景色为白色。这样做是为了清除其他节点的选中样式（即将所有节点的背景色重置为白色）。
                node.BackColor =Color.White;
                //设置当前节点的前景色（文本颜色）为黑色。与上面的一行一样，目的是将所有节点的文本颜色设置为黑色，从而清除之前的选中样式。
                node.ForeColor = Color.Black;
            }
            //bool b1 = ((TreeView)sender).SelectedNode == trvMenu.SelectedNode;
            //bool b2 = ((TreeView)sender).SelectedNode == e.Node;

            //设置被选中的节点 e.Node 的背景色为系统默认的高亮颜色。
            e.Node.BackColor = SystemColors.Highlight;
            //设置被选中的节点 e.Node 的文本颜色为白色，以突出显示该节点。
            e.Node.ForeColor = Color.White;



            // 创建一个新的窗体实例（类型由反射机制动态决定）
            Form form = FormFactory.CreateForm(e.Node.Tag?.ToString());
            // 将新创建的窗体的 MDI 父窗体设置为当前窗体 'this'。
            // MDI（多文档界面）是指一个主窗体包含多个子窗体，在这种情况下，'this' 代表当前窗体。
            // 通过设置 MdiParent，子窗体会在当前窗体的工作区域中显示，而不是独立窗口。
            form.MdiParent = this;
            // 将新创建的窗体的 Parent 属性设置为 splitContainer1.Panel2。
            // 这意味着窗体将被嵌入到一个名为 `splitContainer1` 的控件的右侧面板中。
            // splitContainer 是一个将父窗体分成多个区域的控件，Panel2 通常是右侧面板。
            // Parent 属性定义了该控件的父容器。
            form.Parent = splitContainer1.Panel2;
            // 显示新创建的窗体。此时窗体已经设置好父容器和 MDI 父窗体。
            // 通过调用 Show() 方法，窗体就会在其父容器中显示出来。
            form.Show();

        }
    }
}
