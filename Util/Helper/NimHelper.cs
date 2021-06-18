using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Util.Helper
{
    /// <summary>
    /// 网易云信工具类
    /// </summary>
    public static class NimHelper
    {
        /// <summary>
        /// IM地址
        /// </summary>
        public static string IMUrl { get; set; } = "https://api.netease.im";
        public static string VcloudUrl { get; set; } = "https://vcloud.163.com";
        public static string Appkey { get; set; }
        public static string Secret { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="secret"></param>
        public static void Init(string appkey, string secret)
        {
            Appkey = appkey;
            Secret = secret;
        }

        /// <summary>
        /// 使用x-www-form-urlencoded头和云信进行通讯
        /// </summary>
        /// <param name="url">url路径,全路径</param>
        /// <param name="keyValuePairs">键值对</param>
        /// <returns></returns>
        public static JObject UrlencodedPost(string url, List<KeyValuePair<string, string>> keyValuePairs)
        {
            string text = "0";
            string timeStamp = GetTimeStamp();
            ByteArrayContent content = new FormUrlEncodedContent(keyValuePairs);

            content.Headers.Add("AppKey", Appkey);
            content.Headers.Add("Nonce", text);
            content.Headers.Add("CurTime", timeStamp);
            content.Headers.Add("CheckSum", EncryptToSHA1(Secret + text + timeStamp));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "utf-8" };
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                //异步读取json
                string result = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                return json;
            }
        }

        /// <summary>
        /// 使用json头和云信进行通讯
        /// </summary>
        /// <param name="url">url路径,全路径</param>
        /// <param name="postContent">Jobject的json对象</param>
        /// <returns></returns>
        public static JObject JsonPost(string url, JObject postContent)
        {
            string text = "0";
            string timeStamp = GetTimeStamp();
            ByteArrayContent content = new StringContent(postContent.ToJson());
            content.Headers.Add("AppKey", Appkey);
            content.Headers.Add("Nonce", text);
            content.Headers.Add("CurTime", timeStamp);
            content.Headers.Add("CheckSum", EncryptToSHA1(Secret + text + timeStamp));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                //异步读取json
                string result = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                return json;
            }
        }

        /// <summary>
        /// 创建用户返回Token若用户存在,直接获取Token,若为网络问题,返回null
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public static string CreateUser(string userid)
        {
            string text = "0";
            string timeStamp = GetTimeStamp();
            string token = Guid.NewGuid().ToString("N").ToLower();
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("accid", userid), new KeyValuePair<string, string>("token", token) });
            content.Headers.Add("AppKey", Appkey);
            content.Headers.Add("Nonce", text);
            content.Headers.Add("CurTime", timeStamp);
            content.Headers.Add("CheckSum", EncryptToSHA1(Secret + text + timeStamp));

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "utf-8" };
            using (HttpClient httpClient = new HttpClient())
            {
                //异步Post
                HttpResponseMessage response = httpClient.PostAsync(IMUrl + "/nimserver/user/create.action", content).Result;
                try
                {
                    //异步读取json
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject json = JObject.Parse(result);
                    return json.GetValue("info").Value<string>("token");
                }
                catch
                {
                    return GetUserToken(userid);
                }
            }
        }

        /// <summary>
        /// 获取用户Token,若获取不到,返回null
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public static string GetUserToken(string userid)
        {
            string text = "0";
            string timeStamp = GetTimeStamp();
            string token = Guid.NewGuid().ToString("N").ToLower();
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("accid", userid), new KeyValuePair<string, string>("token", token) });
            content.Headers.Add("AppKey", Appkey);
            content.Headers.Add("Nonce", text);
            content.Headers.Add("CurTime", timeStamp);
            content.Headers.Add("CheckSum", EncryptToSHA1(Secret + text + timeStamp));

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "utf-8" };
            using (HttpClient httpClient = new HttpClient())
            {
                //异步Post
                HttpResponseMessage response = httpClient.PostAsync(IMUrl + "/nimserver/user/update.action", content).Result;
                try
                {
                    //异步读取json
                    string result = response.Content.ReadAsStringAsync().Result;
                    return response.IsSuccessStatusCode == true ? token : null;
                }
                catch
                {
                    return null;
                }
            }
        }
        //        /// <summary>
        //        /// 查询用户最新状态
        //        /// </summary>
        //        /// <param name="userid">accid</param>
        //        /// <param name="begintime">开始时间，毫秒级</param>
        //        /// <param name="endtime">截止时间，毫秒级</param>
        //        /// <param name="limit">本次查询的消息条数上限(最多100条),小于等于0，或者大于100，会提示参数错误</param>
        //        /// <param name="reverse">1按时间正序排列，2按时间降序排列。其它返回参数414错误。默认是按降序排列</param>
        //        /// <returns></returns>
        //        public static string GetUserLastStatus(string userid,string begintime,string endtime,int limit, int reverse)
        //        {
        //            string text = "0";
        //            string timeStamp = GetTimeStamp();
        //            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("accid", userid), new KeyValuePair<string, string>("begintime", begintime), new KeyValuePair<string, string>("endtime", endtime), new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("reverse", reverse.ToString()) });
        //            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("accid", userid),new KeyValuePair<string, string>("begintime", begintime), new KeyValuePair<string, string>("endtime", endtime), new KeyValuePair<string, int>("limit", limit), new KeyValuePair<string, int>("reverse", reverse) });
        //            content.Headers.Add("AppKey", Appkey);
        //            content.Headers.Add("Nonce", text);
        //            content.Headers.Add("CurTime", timeStamp);
        //            content.Headers.Add("CheckSum", EncryptToSHA1(Secret + text + timeStamp));

        //            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "utf-8" };
        //            using (HttpClient httpClient = new HttpClient())
        //            {
        //                //异步Post
        //                HttpResponseMessage response = httpClient.PostAsync("https://api.netease.im/nimserver/history/queryUserEvents.action", content).Result;
        //                try
        //                {
        //                    //异步读取json
        //                    string result = response.Content.ReadAsStringAsync().Result;
        //                    return result;
        //                }
        //                catch
        //                {
        //                    return null;
        //                }
        //            }
        //        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private static string GetTimeStamp()
        {
            return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
        }

        /// <summary>
        /// SHA加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string EncryptToSHA1(string s)
        {
            char[] array = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            byte[] array2 = sHA1CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(s));
            sHA1CryptoServiceProvider.Clear();
            sHA1CryptoServiceProvider.Dispose();
            int num = array2.Length;
            StringBuilder stringBuilder = new StringBuilder(num * 2);
            for (int i = 0; i < num; i++)
            {
                stringBuilder.Append(array[array2[i] >> 4 & 15]);
                stringBuilder.Append(array[array2[i] & 15]);
            }
            return stringBuilder.ToString();
        }
    }
}