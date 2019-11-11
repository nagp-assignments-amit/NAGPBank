using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NAGPBank.CrossCutting.Error
{
    public class BankExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<BankExceptionMiddleware> logger;

        public BankExceptionMiddleware(RequestDelegate next, ILogger<BankExceptionMiddleware> logger)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var trackingId = Guid.NewGuid().ToString();
            exception.Data.Add("TrackingId", trackingId);

            var requestUrl = context.Request.Host.Value + context.Request.Path.Value;
            var requestMethod = context.Request.Method;

            exception.Data.Add("RequestUrl", requestUrl);
            exception.Data.Add("RequestMethod", requestMethod);

            var exceptionMessage = GetExceptionMessageForLogging(exception);
            logger.LogError(exceptionMessage);

            var response = context.Response;
            var customException = exception as BankBaseException;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Unexpected error";

            if (null != customException)
            {
                message = customException.Message;
                statusCode = customException.Code;
            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new BankErrorResponse
            {
                message = message
            }));
        }

        public static string GetExceptionMessageForLogging(Exception exception)
        {
            var exceptionWithoutNewLine = exception.ToString().Replace("\r\n", " || ").Replace("\n", " || ").Replace("\r", " || ");
            string trackingId = (exception.Data["TrackingId"] ?? "").ToString();
            string requestMethod = exception.Data["RequestMethod"].ToString();
            string requestUrl = exception.Data["RequestUrl"].ToString();
            return trackingId + " " + requestMethod + " " + requestUrl + " " + exceptionWithoutNewLine;
        }
    }
}
