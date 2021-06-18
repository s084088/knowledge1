namespace Util.Helper
{
    /// <summary>
    /// 串口通信帮助类
    /// </summary>
    public static class SocketHelper
    {
        /// <summary>
        /// 检查数组的和校验
        /// </summary>
        /// <returns></returns>
        public static bool CheckSum(byte[] bytes)
        {
            if (bytes.Length < 2) return false;

            byte rst = 0x00;
            //计算累加和
            for (int j = 0; j < bytes.Length - 1; j++)
            {
                unchecked
                {
                    rst += bytes[j];
                }
            }

            return bytes[bytes.Length - 1] == rst;
        }

        /// <summary>
        /// 计算累加校验和
        /// </summary>
        /// <param name="SendByteArray">要校验的字节数组</param>
        /// <returns>对字节数组进行校验所得到的校验和</returns>
        public static byte GetSum(byte[] bytes)
        {
            byte rst = 0x00;
            //计算累加和
            for (int j = 0; j < bytes.Length; j++)
            {
                unchecked
                {
                    rst += bytes[j];
                }
            }
            //累加和计算结束
            return rst;
        }
    }
}