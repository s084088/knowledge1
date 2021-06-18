using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiteEntity.Tables
{
    /// <summary>
    /// 知识节点
    /// </summary>
    public class KP : DbBase
    {
        private string name;
        private string text;
        private ObservableCollection<EKP> eKPs;
        private ObservableCollection<KPR> extends;
        private ObservableCollection<KPR> preconditions;
        private bool isAbstract;
        private int type;
        private int difficulty;
        private int importance;

        /// <summary>
        /// 学科 1数学 2英语
        /// </summary>
        public int Subject { get; set; }

        /// <summary>
        /// 知识的名称
        /// </summary>
        [MaxLength(63)]
        public string Name { get => name; set { name = value; PCEH(); } }

        /// <summary>
        /// 此知识的说明
        /// </summary>
        [MaxLength(1024)]
        public string Text { get => text; set { text = value; PCEH(); } }

        /// <summary>
        /// 知识的类型  1,事实性知识  2,概念性知识  3,过程性知识
        /// </summary>
        public int Type { get => type; set { type = value; PCEH(); } }

        /// <summary>
        /// 难度
        /// </summary>
        public int Difficulty { get => difficulty; set { difficulty = value; PCEH(); } }

        /// <summary>
        /// 重要性
        /// </summary>
        public int Importance { get => importance; set { importance = value; PCEH(); } }

        /// <summary>
        /// 是否为虚知识
        /// </summary>
        public bool IsAbstract { get => isAbstract; set { isAbstract = value; PCEH(); } }

        /// <summary>
        /// 此知识包含的实体
        /// </summary>
        public virtual ObservableCollection<EKP> EKPs { get => eKPs; set { eKPs = value; PCEH(); } }

        /// <summary>
        /// 此知识的衍生知识
        /// </summary>
        public virtual ObservableCollection<KPR> Extends { get => extends; set { extends = value; PCEH(); } }

        /// <summary>
        /// 此知识的前置知识
        /// </summary>
        public virtual ObservableCollection<KPR> Preconditions { get => preconditions; set { preconditions = value; PCEH(); } }
    }
}
