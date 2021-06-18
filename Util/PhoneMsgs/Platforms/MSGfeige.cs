using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Util.PhoneMsgs.Platforms
{
    public class MSGfeige : IPhoneMsg
    {
        private string account;
        private string pwd;
        private string key;

        public MSGfeige(string account, string pwd, string key)
        {
            this.account = account;
            this.pwd = pwd;
            this.key = key;
        }

        /// <summary>
        /// 推送数据 POST方式
        /// </summary>
        /// <param name="weburl">POST到的网址</param>
        /// <param name="data">POST的参数及参数值</param>
        /// <param name="encode">编码方式</param>
        /// <returns></returns>
        private string PushToWeb(string weburl, string data, Encoding encode)
        {
            try
            {
                byte[] byteArray = encode.GetBytes(data);

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(weburl));
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();

                //接收返回信息：
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                StreamReader aspx = new StreamReader(response.GetResponseStream(), encode);
                string msg = aspx.ReadToEnd();
                aspx.Close();
                response.Close();
                return msg;
            }
            catch
            {
                //记录错误日志
                return "";
            }
        }

        public string SendSms(string content, string phone)
        {
            BaseSmsRequest request = new BaseSmsRequest
            {
                Account = account,
                Pwd = pwd,//登录web平台 http://sms.feige.ee  在管理中心--基本资料--接口密码 或者首页 接口秘钥 如登录密码修改，接口密码会发生改变，请及时修改程序
                Content = content,
                Mobile = phone,
                SignId = Convert.ToInt64(key), //登录web平台 http://sms.feige.ee  在签名管理中--新增签名--获取id
            };

            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            arge.AppendFormat("&Content={0}", request.Content);
            arge.AppendFormat("&Mobile={0}", request.Mobile);
            arge.AppendFormat("&SignId={0}", request.SignId);
            arge.AppendFormat("&SendTime={0}", request.SendTime);
            string weburl = "http://api.feige.ee/SmsService/Send";
            string resp = PushToWeb(weburl, arge.ToString(), Encoding.UTF8);
            //Console.WriteLine("SendSms:" + resp);
            try
            {
                SendSmsResponse response = JsonConvert.DeserializeObject<SendSmsResponse>(resp);
                if (response.Code == 0)
                {
                    return "发送成功";
                }
                else
                {
                    return response.Message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string SendtTemp(string phone, string temp, string[] context)
        {
            TemplateSmsRequest request = new TemplateSmsRequest
            {
                Account = account,
                Pwd = pwd,
                Content = string.Join("||", context),
                Mobile = phone,
                SignId = Convert.ToInt64(key),
                TemplateId = Convert.ToInt64(temp),//模板id   //登录web平台 http://sms.feige.ee  在模板管理中--新增模板--获取id
                //SendTime=Convert.ToInt64(common.ToUnixStamp(DateTime.Now.AddHours(1)))//定时短信 把时间转换成时间戳的格式
            };

            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            arge.AppendFormat("&Content={0}", request.Content);
            arge.AppendFormat("&Mobile={0}", request.Mobile);
            arge.AppendFormat("&SignId={0}", request.SignId);
            arge.AppendFormat("&TemplateId={0}", request.TemplateId);
            //arge.AppendFormat("&SendTime={0}", request.SendTime);
            string weburl = "http://api.feige.ee/SmsService/Template";
            string resp = PushToWeb(weburl, arge.ToString(), Encoding.UTF8);
            //Console.WriteLine("Template:" + resp);
            try
            {
                SendSmsResponse response = JsonConvert.DeserializeObject<SendSmsResponse>(resp);
                if (response.Code == 0)
                {
                    return "发送成功";
                }
                else
                {
                    return response.Message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string SendTempSms(string phone, string tempCode, string content)
        {
            throw new NotImplementedException();
        }

        private class SendSmsResponse
        {
            //状态码
            public int Code { get; set; }

            /// <summary>
            /// 返回错误信息
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// 发送Id
            /// </summary>
            public string SendId { get; set; }

            /// <summary>
            /// 无效号码数量
            /// </summary>
            public int InvalidCount { get; set; }

            /// <summary>
            /// 成功数量
            /// </summary>
            public int SuccessCount { get; set; }

            /// <summary>
            /// 黑名单数量
            /// </summary>
            public int BlackCount { get; set; }
        }

        private class BaseSmsRequest
        {
            /// <summary>
            /// 账号
            /// </summary>
            public string Account { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Pwd { get; set; }

            /// <summary>
            /// 发送内容
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// 发送号码
            /// </summary>
            public string Mobile { get; set; }

            //发送时间戳
            public long? SendTime { get; set; }

            /// <summary>
            /// 用户签名
            /// </summary>
            public long SignId { get; set; }

            /// <summary>
            /// 扩展码
            /// </summary>
            public string ExtNo { get; set; }
        }

        private class TemplateSmsRequest : BaseSmsRequest
        {
            /// <summary>
            /// 模板Id
            /// </summary>
            public long TemplateId { get; set; }
        }
    }
}