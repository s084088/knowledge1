using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Util
{
    public static partial class Extention
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        static Extention()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                return setting;
            });
        }

        /// <summary>
        /// 将一个object转string，如果是数字，去掉末尾的小数点和0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string TryToNumberString(this object obj)
        {
            if (obj == null) return null;
            string str = obj.ToString();
            if (decimal.TryParse(str, out decimal dec))
            {
                str = dec.ToString("0.########");
            }
            return str;
        }

        /// <summary>
        /// 将一个object对象序列化，返回一个byte[]
        /// </summary>
        /// <param name="obj">能序列化的对象</param>
        /// <returns></returns>
        public static byte[] ToBytes(this object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /// <summary>
        /// 判断是否为Null或者空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null)
                return true;
            else
            {
                string objStr = obj.ToString();
                return string.IsNullOrEmpty(objStr);
            }
        }

        /// <summary>
        /// 将对象序列化成Json字符串
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 2
            };
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }

        /// <summary>
        /// 实体类转json数据，速度快
        /// </summary>
        /// <param name="t">实体类</param>
        /// <returns></returns>
        public static string EntityToJson(this object t)
        {
            if (t == null)
                return null;
            string jsonStr = "";
            jsonStr += "{";
            PropertyInfo[] infos = t.GetType().GetProperties();
            for (int i = 0; i < infos.Length; i++)
            {
                jsonStr = jsonStr + "\"" + infos[i].Name + "\":\"" + infos[i].GetValue(t).ToString() + "\"";
                if (i != infos.Length - 1)
                    jsonStr += ",";
            }
            jsonStr += "}";
            return jsonStr;
        }

        /// <summary>
        /// 深复制
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj) where T : class
        {
            if (obj == null)
                return null;

            return obj.ToJson().ToObject<T>();
        }

        /// <summary>
        /// 将对象序列化为XML字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ToXmlStr<T>(this T obj)
        {
            var jsonStr = obj.ToJson();
            var xmlDoc = JsonConvert.DeserializeXmlNode(jsonStr);
            string xmlDocStr = xmlDoc.InnerXml;

            return xmlDocStr;
        }

        /// <summary>
        /// 使用JObject更新实体对象 (会覆盖相同属性的值)
        /// </summary>
        /// <param name="entity">对应实体(不可为null)</param>
        /// <param name="obj">JObject对象(不能包含实体不存在的属性,属性的值类型必须一致)</param>
        public static void UpdateFromJObject(this object entity, JObject obj)
        {
            IEnumerable<JProperty> properties = obj.Properties();
            foreach (JProperty item in properties)
            {
                string field = item.Name;
                JToken jToken = item.Value;
                field = field.Substring(0, 1).ToUpper() + field.Substring(1);
                entity.SetPropertyValue(field, ((JValue)jToken).Value);
            }
        }

        /// <summary>
        /// 将对象序列化为XML字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="rootNodeName">根节点名(建议设为xml)</param>
        /// <returns></returns>
        public static string ToXmlStr<T>(this T obj, string rootNodeName)
        {
            var jsonStr = obj.ToJson();
            var xmlDoc = JsonConvert.DeserializeXmlNode(jsonStr, rootNodeName);
            string xmlDocStr = xmlDoc.InnerXml;

            return xmlDocStr;
        }

        /// <summary>
        /// 获取某属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).GetValue(obj);
        }

        /// <summary>
        /// 获取某属性类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static Type GetPropertyType(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).PropertyType;
        }

        /// <summary>
        /// 获取json值,若取不到,不报错,返回空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static T GetValue<T>(this JToken json, string field)
        {
            if (json == null) return default;
            try { return json.Value<T>(field); }
            catch { return default; }
        }

        /// <summary>
        /// 设置某属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            if (value != null && (value.GetType() == typeof(long) || value.GetType() == typeof(short)))
                value = Convert.ToInt32(value);
            try
            {
                obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).SetValue(obj, value);
            }
            catch
            {
                try
                {
                    obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).SetValue(obj, Convert.ToDecimal(value));
                }
                catch
                {
                    obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).SetValue(obj, Convert.ToDateTime(value));
                }
            }
        }

        /// <summary>
        /// 为jToken增加序列(必须为数组json)
        /// </summary>
        /// <param name="jToken"></param>
        public static JToken AddIndex(this JToken jToken)
        {
            int i = 1;
            jToken.ForEach(l => ((JObject)l).Add("Index", i++));
            return jToken;
        }
    }
}