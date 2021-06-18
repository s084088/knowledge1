using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;

namespace Util.PhoneMsgs.Platforms
{
    public class MSGaliyun : IPhoneMsg
    {
        public string AccessKeyID { get; set; }
        public string AccessKeySecret { get; set; }

        public MSGaliyun(string accessKeyID, string accessKeySecret)
        {
            AccessKeyID = accessKeyID;
            AccessKeySecret = accessKeySecret;
        }

        public string SendSms(string content, string phone)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", AccessKeyID, AccessKeySecret);
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", "15950083044");
            request.AddQueryParameters("SignName", "爱学仕");
            request.AddQueryParameters("TemplateCode", "SMS_187930887");
            request.AddQueryParameters("TemplateParam", "{\"code\":\"666666\"}");
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                return "发送成功";
            }
            catch (ServerException e)
            {
                return "阿里云服务器错误:" + e.ErrorCode + e.Message;
            }
            catch (ClientException e)
            {
                return "调用错误:" + e.ErrorCode + e.Message;
            }
        }

        public string SendtTemp(string phone, string temp, string[] context)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", AccessKeyID, AccessKeySecret);
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", phone);
            request.AddQueryParameters("SignName", "爱学仕");
            request.AddQueryParameters("TemplateCode", temp);
            request.AddQueryParameters("TemplateParam", "{\"code\":\"" + context[0] + "\"}");
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                return "发送成功";
            }
            catch (ServerException e)
            {
                return "阿里云服务器错误:" + e.ErrorCode + e.Message;
            }
            catch (ClientException e)
            {
                return "调用错误:" + e.ErrorCode + e.Message;
            }
        }

        public string SendTempSms(string phone, string tempCode, string content)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", AccessKeyID, AccessKeySecret);
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", phone);
            request.AddQueryParameters("SignName", "爱学仕");
            request.AddQueryParameters("TemplateCode", tempCode);
            request.AddQueryParameters("TemplateParam", content);
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                return "发送成功";
            }
            catch (ServerException e)
            {
                return "阿里云服务器错误:" + e.ErrorCode + e.Message;
            }
            catch (ClientException e)
            {
                return "调用错误:" + e.ErrorCode + e.Message;
            }
        }


    }
}