namespace Util.PhoneMsgs.Platforms
{
    public interface IPhoneMsg
    {
        /// <summary>
        /// 发送短信,
        /// </summary>
        /// <param name="content">发送的内容</param>
        /// <param name="phone">发送的手机</param>
        /// <returns></returns>
        string SendSms(string content, string phone);

        /// <summary>
        /// 发送模板短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="temp"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        string SendtTemp(string phone, string temp, string[] context);

        /// <summary>
        /// 发送模板短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="tempCode"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        string SendTempSms(string phone, string tempCode, string content);
    }
}