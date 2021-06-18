using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Model
{
    /// <summary>
    /// API返回专用错误
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// 错误等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 错误备注
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// API返回专用错误
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="level">错误等级 , 默认1</param>
        /// <param name="tag">错误备注,默认null</param>
        public ApiException(string message = null, int level = 1, string tag = null) : base(message)
        {
            Level = level;
            Tag = tag;
        }
    }
}
