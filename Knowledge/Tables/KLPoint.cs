using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Knowledge.Tables
{
    /// <summary>
    /// 知识节点
    /// </summary>
    [Table("KL_Point")]
    public class KLPoint : DbBase
    {
        /// <summary>
        /// 知识点名称
        /// </summary>
        [MaxLength(127)]
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [MaxLength(2047)]
        public string Explain { get; set; }

        /// <summary>
        /// 对应类型
        /// </summary>
        public virtual KLLevel Type { get; set; }
        public string TypeID { get; set; }

        /// <summary>
        /// 对应过程
        /// </summary>
        public virtual KLLevel Process { get; set; }
        public string ProcessID { get; set; }

        /// <summary>
        /// 对应等级
        /// </summary>
        public virtual KLLevel Level { get; set; }
        public string LevelID { get; set; }

        /// <summary>
        /// 知识点的节点
        /// </summary>
        public virtual ICollection<KLNode> Nodes { get; set; }
    }
}
