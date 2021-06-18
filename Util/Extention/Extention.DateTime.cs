﻿using NodaTime;
using System;
using System.Globalization;

namespace Util
{
    public static partial class Extention
    {
        ///   <summary>
        ///  获取某一日期是该年中的第几周
        ///   </summary>
        ///   <param name="dateTime"> 日期 </param>
        ///   <returns> 该日期在该年中的周数 </returns>
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// 获取Js格式的timestamp
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns></returns>
        public static long ToJsTimestamp(this DateTime dateTime)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, 0), TimeZoneInfo.Local);
            long result = (dateTime.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位
            return result;
        }

        /// <summary>
        /// 获取js中的getTime()
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public static Int64 JsGetTime(this DateTime dt)
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (dt.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval;
        }

        /// <summary>
        /// 返回默认时间1970-01-01
        /// </summary>
        /// <param name="dt">时间日期</param>
        /// <returns></returns>
        public static DateTime Default(this DateTime dt)
        {
            return DateTime.Parse("1970-01-01");
        }

        /// <summary>
        /// 转为标准时间（北京时间，解决Linux时区问题）
        /// </summary>
        /// <param name="dt">当前时间</param>
        /// <returns></returns>
        public static DateTime ToCstTime(this DateTime dt)
        {
            Instant now = SystemClock.Instance.GetCurrentInstant();
            var shanghaiZone = DateTimeZoneProviders.Tzdb["Asia/Shanghai"];
            return now.InZone(shanghaiZone).ToDateTimeUnspecified();
        }

        /// <summary>
        /// 转化为默认时间字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDefaultString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转化为默认日期字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDefaultDateString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 取本周第一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDay(this DateTime dt)
        {
            dt = dt.Date;

            int weeknow = Convert.ToInt32(dt.DayOfWeek);

            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            return dt.AddDays(daydiff);
        }
    }
}