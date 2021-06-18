using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NKnowledge.Tables
{
    [Table("Line")]
    public class Line : DbBase
    {
        /// <summary>
        /// 起点
        /// </summary>
        public virtual Node Source { get; set; }
        public int SourceID { get; set; }

        /// <summary>
        /// 终点
        /// </summary>
        public virtual Node Target { get; set; }
        public int TargetID { get; set; }
    }
}