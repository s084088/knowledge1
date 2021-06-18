using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Util;

namespace LiteEntity.Tables
{
    /// <summary>
    /// 所有表的公共字段
    /// </summary>
    public class DbBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key, MaxLength(63)]
        public string ID { get; set; } = GuidHelper.GenerateKey();

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

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 通知界面更新指定属性名
        /// </summary>
        /// <param name="propertyName"></param>
        public void PC(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// 通知界面更新指定属性名(属性名由调用方法获取)
        /// </summary>
        /// <param name="propertyName"></param>
        public void PCEH([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}