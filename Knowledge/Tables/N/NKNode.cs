using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Knowledge.Tables
{
    /// <summary>
    /// 节点
    /// </summary>
    [Table("N_K_Node")]
    public class NKNode : DbBase
    {
        /// <summary>
        /// 知识点名称
        /// </summary>
        [MaxLength(127)]
        public string Name { get; set; }

        /// <summary>
        /// 类型 1数学
        /// </summary>
        public int Type { get; set; }
    }
}
