using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util;
using Util.Model;

namespace KnowledgeAPI.Comm
{
    public class ErrHandle : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string token = context.HttpContext.Request.Headers["token"];
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string deviceid = context.HttpContext.Request.Headers["deviceid"];
            string message = context.Exception.Message;
            string detail = context.Exception.ToString();
            string path = context.HttpContext.Request.Path.Value.ToLower();

            //异常分类
            if (context.Exception is ApiException a)
            {
                if (a.Level == 103)
                {
                    LogHelper.WriteLog($" \t IP:{ip} \t Message:{message} \t PATH:{path} \t ", "logs/AuthLog", DateTime.Now.ToDefaultDateString() + ".log");
                }
                else
                {
                    LogHelper.WriteLog($" \t IP:{ip} \t Message:{message} \t PATH:{path} \t Detail:{detail}", "logs/AccessErrorLog", DateTime.Now.ToDefaultDateString() + ".log");
                }
                context.Result = new BaseController().Err(a.Level, a.Message, a.Tag);
            }
            else
            {
                LogHelper.WriteLog($" \t IP:{ip} \t Message:{message} \t PATH:{path} \t Detail:{detail}", "logs/AccessErrorLog", DateTime.Now.ToDefaultDateString() + "_Fatal.log");
                context.Result = new BaseController().Err(9, "系统繁忙", "请重新再试");
            }
        }
    }
}
