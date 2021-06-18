using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Util
{
    public class WebAPI
    {
        /// <summary>
        /// Post请求,异步
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">post的内容</param>
        /// <returns>返回string[0]代表内容,string[1]代表状态码</returns>
        public async Task<string[]> AsyncPostResponse(string url, string postData)
        {
            string[] re = new string[] { string.Empty, string.Empty };
            try
            {
                //设置Http的正文
                HttpContent httpContent = new StringContent(postData);
                //设置Http的内容标头
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //设置Http的内容标头的字符
                httpContent.Headers.ContentType.CharSet = "utf-8";
                using (HttpClient httpClient = new HttpClient())
                {
                    //异步Post
                    HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
                    //输出Http响应状态码
                    re[1] = response.StatusCode.ToString();
                    //确保Http响应成功
                    if (response.IsSuccessStatusCode)
                    {
                        //异步读取json
                        re[0] = await response.Content.ReadAsStringAsync();
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
        public string[] PostResponse(string url, string postData)
        {
            string[] re = new string[] { string.Empty, string.Empty };
            try
            {
                //设置Http的正文
                HttpContent httpContent = new StringContent(postData);
                //设置Http的内容标头
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //设置Http的内容标头的字符
                httpContent.Headers.ContentType.CharSet = "utf-8";
                using (HttpClient httpClient = new HttpClient())
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

        // 泛型：Post请求
        public T PostResponse<T>(string url, string postData) where T : class, new()
        {
            T result = default(T);

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    //Newtonsoft.Json
                    string json = JsonConvert.DeserializeObject(s).ToString();
                    result = JsonConvert.DeserializeObject<T>(json);
                }
            }
            return result;
        }

        // 泛型：Get请求
        public T GetResponse<T>(string url) where T : class, new()
        {
            T result = default(T);

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    string json = JsonConvert.DeserializeObject(s).ToString();
                    result = JsonConvert.DeserializeObject<T>(json);
                }
            }
            return result;
        }

        // Get请求
        public string GetResponse(string url, out string statusCode)
        {
            string result = string.Empty;

            using (HttpClient httpClient = new HttpClient())
            {
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

        // Put请求
        public string PutResponse(string url, string putData, out string statusCode)
        {
            string result = string.Empty;
            HttpContent httpContent = new StringContent(putData);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PutAsync(url, httpContent).Result;
                statusCode = response.StatusCode.ToString();
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }

        // 泛型：Put请求
        public T PutResponse<T>(string url, string putData) where T : class, new()
        {
            T result = default(T);
            HttpContent httpContent = new StringContent(putData);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PutAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    string json = JsonConvert.DeserializeObject(s).ToString();
                    result = JsonConvert.DeserializeObject<T>(json);
                }
            }
            return result;
        }
    }
}