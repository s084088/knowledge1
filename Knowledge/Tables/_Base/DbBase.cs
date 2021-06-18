using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Util;

namespace Knowledge.Tables
{
    /// <summary>
    /// 所有表的公共字段
    /// </summary>
    public class DbBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key, MaxLength(63)]
        public string ID { get; set; } = GuidHelper.GenerateGuid();

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 删除标志: 0正常,1删除
        /// </summary>
        [DefaultValue(0)]
        public int DeleteFlag { get; set; }

        public void IsNew()
        {
            ID = GuidHelper.GenerateGuid();
            CreateTime = DateTime.Now;
        }
    }
}
