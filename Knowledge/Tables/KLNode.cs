using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Knowledge.Tables
{
    /// <summary>
    /// 知识图谱节点
    /// </summary>
    [Table("KL_Node")]
    public class KLNode : DbBase
    {
        /// <summary>
        /// 对应知识点
        /// </summary>
        public virtual KLPoint Point { get; set; }
        public string PointID { get; set; }

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
    }
}