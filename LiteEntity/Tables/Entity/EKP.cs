using System;
using System.Collections.Generic;
using System.Text;

namespace LiteEntity.Tables
{
    public class EKP : DbBase
    {
        private Entity entity;
        private KP kP;

        /// <summary>
        /// 对应实体
        /// </summary>
        public virtual Entity Entity { get => entity; set { entity = value; PCEH(); } }
        public string EntityID { get; set; }

        /// <summary>
        /// 对应知识点
        /// </summary>
        
        public virtual KP KP { get => kP; set { kP = value; PCEH(); } }
        public string KPID { get; set; }
    }
}
