using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Knowledge.Tables
{
    [Table("N_K_Line")]
    public class NKLine : DbBase
    {
        /// <summary>
        /// 起点
        /// </summary>
        public virtual NKNode Source { get; set; }
        public string SourceID { get; set; }

        /// <summary>
        /// 终点
        /// </summary>
        public virtual NKNode Target { get; set; }
        public string TargetID { get; set; }
    }
}