using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using System.Text;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Relive.Server.API.Middlewares
{
    public class ExceptionHandler
    {
        RequestDelegate _next;
        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionHandler> _logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception:\n{e}");
                httpContext.Response.StatusCode = 500;
                var errorObj = new { Message = "Server Error!" };
                await httpContext.Response.WriteAsJsonAsync(errorObj);
            }
        }
    }

    public static class ExcpetionHandlerExtention
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandler>();
            return builder;
        }
    }
}
