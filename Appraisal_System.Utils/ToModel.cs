using System;
using System.Data;
using System.Reflection;

namespace Appraisal_System.Utils
{
    public static class ToModel
    {
        //扩展方法 DataRowToModel<TModel>
        //通过 this DataRow dr 来扩展 DataRow 类型的。
        //向现有类型（在这里是 DataRow）添加新的方法
        //TModel 是一个占位符
        public static TModel DataRowToModel<TModel>(this DataRow dr)
        {
            Type type = typeof(TModel); // 获取目标类型
            TModel model = (TModel)Activator.CreateInstance(type); // 创建目标类型的实例

            // 获取目标类型的所有公共属性
            PropertyInfo[] properties = type.GetProperties();

            // 遍历每个属性
            foreach (var prop in properties)
            {
                if (prop.CanWrite) // 确保属性是可写的
                {
                    // 检查DataRow是否包含该列
                    if (dr.Table.Columns.Contains(prop.Name))
                    {
                        object value = dr[prop.Name];

                        // 如果DataRow中的值不是DBNull，则进行赋值
                        if (value != DBNull.Value)
                        {
                            try
                            {
                                // 处理布尔类型字段
                                if (prop.PropertyType == typeof(bool))
                                {
                                    // 对于布尔类型属性，转换成 bool
                                    value = Convert.ToBoolean(value);
                                }
                                else if (prop.PropertyType == typeof(int))
                                {
                                    // 对于int类型，转换成 int
                                    value = Convert.ToInt32(value);
                                }

                                // 将DataRow中的值转换为目标属性的类型并赋值
                                prop.SetValue(model, Convert.ChangeType(value, prop.PropertyType));
                            }
                            catch (Exception ex)
                            {
                                // 如果类型转换失败，可以记录错误日志
                                Console.WriteLine($"Error setting property {prop.Name}: {ex.Message}");
                            }
                        }
                    }
                }
            }

            return model; // 返回填充好的对象
        }
    }
}
