using System;
using System.Security.Cryptography;

namespace Util
{
    public static partial class Extention
    {
        private static readonly RNGCryptoServiceProvider RandomGenerator = new RNGCryptoServiceProvider();

        /// <summary>
        /// 转为有序的GUID
        /// 注：长度为50字符
        /// </summary>
        /// <param name="guid">新的GUID</param>
        /// <returns></returns>
        public static string ToSequentialGuid(this Guid guid)
        {
            var timeStr = DateTime.Now.Ticks.ToString("x8");
            var newGuid = $"{timeStr.PadLeft(16, '0')}{guid.ToString("N")}";

            return newGuid;
        }

        /// <summary>
        /// 获取序列Guid
        /// </summary>
        /// <returns></returns>
        public static Guid SequenceGuid()
        {
            byte[] randomBytes = new byte[10];
            RandomGenerator.GetBytes(randomBytes);
            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);
            if (BitConverter.IsLittleEndian) Array.Reverse(timestampBytes);
            byte[] guidBytes = new byte[16];
            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
            Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(guidBytes, 0, 4);
                Array.Reverse(guidBytes, 4, 2);
            }
            return new Guid(guidBytes);
        }
    }
}