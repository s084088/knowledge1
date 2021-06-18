using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Knowledge.Tables
{
    public class KLLine : DbBase
    {
        /// <summary>
        /// 源
        /// </summary>
        public virtual KLPoint Source { get; set; }
        public string SourceID { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public virtual KLPoint Target { get; set; }
        public string TargetID { get; set; }

        /// <summary>
        /// 描述,线上的字
        /// </summary>
        [MaxLength(127)]
        public string Description { get; set; }
    }
}
