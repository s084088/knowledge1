using System;

namespace Util
{
    /// <summary>
    /// GUID帮助类
    /// </summary>
    public static class GuidHelper
    {
        /// <summary>
        /// 生成48位主键,时间戳+GUID
        /// </summary>
        /// <returns></returns>
        public static string GenerateKey()
        {
            return Guid.NewGuid().ToSequentialGuid().ToUpper();
        }

        /// <summary>
        /// 获取随机4位数字,可用于验证码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GenerateNumberKey(string number = "")
        {
            return number + (new Random().Next(1, 9999)).ToString().PadLeft(4, '0');
        }

        /// <summary>
        /// 生成GUID主键,前12位为机器码和时间戳,相比GenerateKey,速度快,节省空间,唯一性可能较差,但理论上够用
        /// </summary>
        /// <returns></returns>
        public static string GenerateGuid()
        {
            return Extention.SequenceGuid().ToString("N").ToUpper();
        }
    }
}