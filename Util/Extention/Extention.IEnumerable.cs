using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Util
{
    public static partial class Extention
    {
        /// <summary>
        /// 复制序列中的数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="iEnumberable">原数据</param>
        /// <param name="startIndex">原数据开始复制的起始位置</param>
        /// <param name="length">需要复制的数据长度</param>
        /// <returns></returns>
        public static IEnumerable<T> Copy<T>(this IEnumerable<T> iEnumberable, int startIndex, int length)
        {
            var sourceArray = iEnumberable.ToArray();
            T[] newArray = new T[length];
            Array.Copy(sourceArray, startIndex, newArray, 0, length);

            return newArray;
        }

        /// <summary>
        /// 给IEnumerable拓展ForEach方法
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="iEnumberable">数据源</param>
        /// <param name="func">方法</param>
        public static void ForEach<T>(this IEnumerable<T> iEnumberable, Action<T> func)
        {
            foreach (var item in iEnumberable)
            {
                func(item);
            }
        }

        /// <summary>
        /// 给IEnumerable拓展ForEach方法
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="iEnumberable">数据源</param>
        /// <param name="func">方法</param>
        public static void ForEach<T>(this IEnumerable<T> iEnumberable, Action<T, int> func)
        {
            var array = iEnumberable.ToArray();
            for (int i = 0; i < array.Count(); i++)
            {
                func(array[i], i);
            }
        }

        /// <summary>
        /// IEnumerable转换为List'T'
        /// </summary>
        /// <typeparam name="T">参数</typeparam>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static List<T> CastToList<T>(this IEnumerable source)
        {
            return new List<T>(source.Cast<T>());
        }

        /// <summary>
        /// 将IEnumerable'T'转为对应的DataTable
        /// </summary>
        /// <typeparam name="T">数据模型</typeparam>
        /// <param name="iEnumberable">数据源</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> iEnumberable)
        {
            return iEnumberable.ToJson().ToDataTable();
        }

        /// <summary>
        /// ObservableCollection删除扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int RemoveAll<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            List<T> itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count();
        }

        /// <summary>
        /// ICollection删除扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int RemoveAll<T>(this ICollection<T> coll, Func<T, bool> condition)
        {
            List<T> itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count();
        }

        /// <summary>
        /// 获取序列中的随机元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static T Random<T>(this IEnumerable<T> coll)
        {
            if (coll == null || coll.Count() == 0) return default;
            int t = new Random().Next(0, coll.Count());
            return coll.Skip(t).First();
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> coll, int count)
        {
            if (coll == null) return new List<T>();
            if (coll.Count() == 0) return coll;
            count = count < coll.Count() ? count : coll.Count();
            List<T> ret = new List<T>();
            while (count > 0)
            {
                T re = coll.Random();
                if (!ret.Contains(re))
                {
                    ret.Add(re);
                    count--;
                }
            }
            return ret;
        }

        /// <summary>
        /// 扩展Linq的sum,timespan求和
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TimeSpan Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            TimeSpan sum = TimeSpan.Zero;
            checked
            {
                foreach (TSource item in source)
                {
                    sum += selector(item);
                }
            }

            return sum;
        }
    }
}