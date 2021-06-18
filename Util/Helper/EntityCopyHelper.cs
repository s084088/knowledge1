using System;
using System.Collections.Generic;
using System.Reflection;

namespace Util.Helper
{
    public static class EntityCopyHelper
    {
        /// <summary>
        /// 反射实现两个类的对象之间相同属性的值的复制
        /// 适用于初始化新实体
        /// </summary>
        /// <typeparam name="D">返回的实体</typeparam>
        /// <typeparam name="S">数据源实体</typeparam>
        /// <param name="s">数据源实体</param>
        /// <returns>返回的新实体</returns>
        public static D Mapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>(); //构造新实例
            try
            {
                var Types = s.GetType();//获得类型
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType)//判断属性名是否相同
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }

        public static List<string> GetFields<T>(T t)
        {
            List<string> fields = new List<string>();
            try
            {
                List<Type> types = new List<Type>
                {
                    typeof(string),
                    typeof(int),typeof(int?),
                    typeof(DateTime),typeof(DateTime?),
                    typeof(decimal),typeof(decimal?),
                    typeof(double),typeof(double?),
                    typeof(float),typeof(float?),
                    typeof(short),typeof(short?),
                    typeof(long),typeof(long?),
                };
                Type type = t.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (types.Contains(pi.PropertyType)) fields.Add(pi.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fields;
        }
    }
}