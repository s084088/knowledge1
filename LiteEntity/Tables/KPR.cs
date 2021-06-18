using System;
using System.Collections.Generic;
using System.Text;

namespace LiteEntity.Tables
{
    public class KPR : DbBase
    {
        private KP origin;
        private KP target;
        private int type;
        private int tightness;

        /// <summary>
        /// 学科 1数学 2英语
        /// </summary>
        public int Subject { get; set; }

        /// <summary>
        /// 起点
        /// </summary>
        public virtual KP Origin { get => origin; set { origin = value; PCEH(); } }
        public string OriginID { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public virtual KP Target { get => target; set { target = value; PCEH(); } }
        public string TargetID { get; set; }

        /// <summary>
        /// 类型, 1,递进 2,包含 3,衍生 4,相似
        /// </summary>
        public int Type { get => type; set { type = value; PCEH(); } }

        /// <summary>
        /// 紧密度
        /// </summary>
        public int Tightness { get => tightness; set { tightness = value; PCEH(); } }

        /// <summary>
        /// 描述
        /// </summary>
        public string Text { get; set; }
    }
}
