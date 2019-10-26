using Microsoft.AspNetCore.Http;
using NetCore.Template.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NetCore.Template.Infrastructure
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ConfigurationAccessor configurationAccessor;

        public ExceptionHandlingMiddleware(RequestDelegate next, ConfigurationAccessor configurationAccessor)
        {
            this.next = next;
            this.configurationAccessor = configurationAccessor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await GenericBadRequestException(context, ex).ConfigureAwait(false);
            }
        }

        private Task GenericBadRequestException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse { Message = configurationAccessor.DetailedErrors ? ex.Message : 
                "There has been an error, please contact system administrator" }));
        }
    }
}