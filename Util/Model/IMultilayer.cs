using System.Collections.Generic;

namespace Util
{
    /// <summary>
    /// 多层模型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMultilayer<T>
    {
        /// <summary>
        /// 自身ID
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        string ParentID { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        ICollection<T> Children { get; set; }
    }
}