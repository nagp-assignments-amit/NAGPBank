using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NAGPBank.CrossCutting.Error
{
    public class BankExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public BankExceptionMiddleware(RequestDelegate next)
        {
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
    }
}
