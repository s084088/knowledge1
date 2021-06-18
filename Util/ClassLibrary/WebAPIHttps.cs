using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Util
{
    public class WebAPIHttps
    {
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">post的内容</param>
        /// <returns>返回string[0]代表内容,string[1]代表状态码</returns>
        public string[] PostResponse(string url, string postData)
        {
            string[] re = new string[] { string.Empty, string.Empty };
            try
            {
                //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
                //{
                //    return true;
                //};
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpClientHandler handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
                handler.AllowAutoRedirect = true;
                handler.UseCookies = true;
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true;
                //设置Http的正文
                HttpContent httpContent = new StringContent(postData);
                //设置Http的内容标头
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //设置Http的内容标头的字符
                httpContent.Headers.ContentType.CharSet = "utf-8";
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    //异步Post
                    HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                    //输出Http响应状态码
                    re[1] = response.StatusCode.ToString();
                    //确保Http响应成功
                    if (response.IsSuccessStatusCode)
                    {
                        //异步读取json
                        re[0] = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch { }
            return re;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">post的内容</param>
        /// <returns>返回string[0]代表内容,string[1]代表状态码</returns>
        public async Task<string[]> AsyncPostResponse(string url, string postData)
        {
            string[] re = new string[] { string.Empty, string.Empty };
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
                {
                    return true;
                };
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                HttpClientHandler handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
                handler.AllowAutoRedirect = true;
                handler.UseCookies = true;
                //设置Http的正文
                HttpContent httpContent = new StringContent(postData);
                //设置Http的内容标头
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //设置Http的内容标头的字符
                httpContent.Headers.ContentType.CharSet = "utf-8";
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    //异步Post
                    HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
                    //输出Http响应状态码
                    re[1] = response.StatusCode.ToString();
                    //确保Http响应成功
                    if (response.IsSuccessStatusCode)
                    {
                        //异步读取json
                        re[0] = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch { }
            return re;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public string GetResponse(string url, out string statusCode)
        {
            string result = string.Empty;

            using (HttpClient httpClient = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
                {
                    return true;
                };
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                statusCode = response.StatusCode.ToString();

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }
    }
}