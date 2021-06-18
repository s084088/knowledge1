using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util;

namespace KnowledgeAPI.Comm
{
    public class UrlCheck : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string address = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string path = context.HttpContext.Request.Path.Value.ToUpper();
            string token = context.HttpContext.Request.Headers["token"];
            string device = context.HttpContext.Request.Headers["deviceid"];
            string param = context.ActionArguments.ToJson();

            LogHelper.WriteLog($" \t IP:{address} \t PATH:{path.ToLower()} \t Token:{token} \t DeviceID:{device} \t PARAM:{param}", "logs/AccessLog", DateTime.Now.ToDefaultDateString() + ".log");


        }

    }
}
