using System;

namespace Util
{
    /// <summary>
    /// 串口通信包 -- 请求
    /// </summary>
    public class Send_Package : Socket_Package
    {
        /// <summary>
        /// 通信包类型
        /// </summary>
        public override int PackageType { get; } = 1;

        /// <summary>
        /// 操作人
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 请求接口
        /// </summary>
        public string Ctrl { get; set; }

        /// <summary>
        /// 主数据
        /// </summary>
        public object Content { get; set; }
    }

    /// <summary>
    /// 串口通信包 -- 接收
    /// </summary>
    public class Recv_Package : Socket_Package
    {
        /// <summary>
        /// 通信包类型
        /// </summary>
        public override int PackageType { get; } = 2;

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 主数据
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }
    }

    /// <summary>
    /// 串口通信包基类
    /// </summary>
    public abstract class Socket_Package
    {
        /// <summary>
        /// 通信包类型
        /// </summary>
        public abstract int PackageType { get; }

        /// <summary>
        /// 包号--发送时每次随机生成,返回的包号需和发送的一致
        /// </summary>
        public string PackageNumber { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }

    public class ApiPakcage
    {
        public string Code { get; set; }

        public object Result { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public bool Success { get; set; } = true;
    }

}