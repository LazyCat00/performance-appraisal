using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appraisal_System.Common
{
    // FormFactory 类负责创建并管理 Form 类型的对象
    // 它通过反射机制动态加载窗体类型，并且管理这些窗体的显示与隐藏
    class FormFactory
    {
        // 存储所有的类型信息，类型是通过反射从 Appraisal_System.exe 中获取的
        private static List<Type> types;

        // 用来存储已经实例化的窗体对象
        private static Form form;

        // 存储已经实例化的所有窗体对象的集合，便于后续隐藏所有窗体
        private static List<Form> forms = new List<Form>();

        // 静态构造函数
        static FormFactory()
        {
            // 通过反射加载当前应用程序的程序集（Appraisal_System.exe）
            Assembly ass = Assembly.LoadFrom("Appraisal_System.exe");

            // 获取该程序集中的所有类型，并将它们存储到 types 列表中
            types = ass.GetTypes().ToList();
        }

        // 静态方法：根据窗体名称动态创建窗体实例
        public static Form CreateForm(string formName)
        {
            // 隐藏所有已经创建的窗体
            HideFormAll();
            //没有传入窗体名称 默认就是空白窗
            formName = formName == null ? "FormNone" : formName;
            // 查找已经创建的窗体列表，查看是否已有这个名称的窗体实例
            form = forms.Find(m => m.Name == formName);

            // 如果没有找到相应的窗体实例，则需要通过反射来动态创建
            if (form == null)
            {
                // 通过类型名查找对应的 Type 对象
                Type type = types.Find(m => m.Name == formName);

                // 使用 Activator.CreateInstance 创建窗体实例，类型转换为 Form
                form = (Form)Activator.CreateInstance(type);
            }

            // 将创建的窗体实例添加到窗体列表中，方便管理
            forms.Add(form);

            // 返回创建的窗体实例
            return form;
        }

        // 静态方法：隐藏所有已经创建的窗体
        public static void HideFormAll()
        {
            // 遍历所有窗体，并调用每个窗体的 Hide 方法隐藏它
            foreach (var form in forms)
            {
                form.Hide();
            }
        }
    }
}
