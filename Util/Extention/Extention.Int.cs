using System;

namespace Util
{
    public static partial class Extention
    {
        /// <summary>
        /// int转Ascll字符
        /// </summary>
        /// <param name="ascllCode"></param>
        /// <returns></returns>
        public static string ToAscllStr(this int ascllCode)
        {
            if (ascllCode >= 0 && ascllCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)ascllCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }
    }
}