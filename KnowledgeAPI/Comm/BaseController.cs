using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util;
using Util.Model;

namespace KnowledgeAPI.Comm
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 返回含内容的数据包
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [NonAction]
        public Package Pack(object obj)
        {
            return new Package
            {
                success = true,
                resultCode = 200,
                resultDesc = "调用成功",
                result = obj,
            };
        }

        /// <summary>
        /// 抛错
        /// </summary>
        /// <param name="msg"></param>
        [NonAction]
        public void ApiError(string msg = null)
        {
            throw new ApiException(msg);
        }

        /// <summary>
        /// 返回错误数据包
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Err(int level, string message, string tag = null)
        {
            if (tag != null) tag = "    " + tag;
            return Json(new Package
            {
                success = false,
                resultCode = level,
                resultDesc = message + tag,
                result = null,
            });
        }
    }

    /// <summary>
    /// 返回数据包模型
    /// </summary>
    public class Package
    {
        public bool success { get; set; }

        public int resultCode { get; set; }

        public string resultDesc { get; set; }

        public string resultTime { get; set; } = DateTime.Now.ToDefaultString();

        public object result { get; set; }
    }
}
