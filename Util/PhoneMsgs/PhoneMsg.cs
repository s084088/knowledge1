using System;
using System.Collections.Generic;
using Util.PhoneMsgs.Platforms;

namespace Util
{
    public static class PhoneMsg
    {
        public static IPhoneMsg Plat;

        public static List<VerifiMsg> VerifiMsgs = new List<VerifiMsg>();

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="content">短信内容</param>
        /// <param name="phone">目标手机号</param>
        /// <returns></returns>
        public static void Send(string content, string phone)
        {
            string s = Plat.SendSms(content, phone);
            if (s != "发送成功") throw new Exception(s);
        }

        /// <summary>
        /// 发送模板短信
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="tempid">模板ID</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static void SendTemp(string phone, string tempid, string[] content)
        {
            string s = Plat.SendtTemp(phone, tempid, content);
            if (s != "发送成功") throw new Exception(s);
        }

        /// <summary>
        /// 发送学生注册通知
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="tempCode"></param>
        /// <param name="content"></param>
        public static void SendTempSms(string phone, string tempCode, string content)
        {
            string s = Plat.SendTempSms(phone, tempCode, content);
            if (s != "发送成功") throw new Exception(s);
        }
    }

    /// <summary>
    /// 验证码模型
    /// </summary>
    public class VerifiMsg
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 方式
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}