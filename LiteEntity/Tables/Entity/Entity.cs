using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiteEntity.Tables
{
    /// <summary>
    /// 实体表
    /// </summary>
    public class Entity : DbBase
    {
        private string name;
        private string text;
        private ObservableCollection<EKP> eKPs;

        /// <summary>
        /// 实体名称
        /// </summary>
        [MaxLength(63)]
        public string Name { get => name; set { name = value; PCEH(); } }

        /// <summary>
        /// 实体说明
        /// </summary>
        [MaxLength(1024)]
        public string Text { get => text; set { text = value; PCEH(); } }

        /// <summary>
        /// 实体的知识
        /// </summary>
        public virtual ObservableCollection<EKP> EKPs { get => eKPs; set { eKPs = value; PCEH(); } }
    }
}
