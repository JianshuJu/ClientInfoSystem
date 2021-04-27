using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using Microsoft.Extensions.Logging;

namespace ClientInfoSystem.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ClientInfoSystemExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ClientInfoSystemExceptionMiddleware> _logger;

        public ClientInfoSystemExceptionMiddleware(RequestDelegate next, ILogger<ClientInfoSystemExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Middleware is catching exception");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            _logger.LogError(("Starting Logging for exception"));
            var errorModel = new ErrorResponseModel
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                InnerExceptionMessage = ex.InnerException?.Message,
            };
            _logger.LogError("Message: {exMessage} \nStack Trace: {exStackTrace} \nInnerExceptionMessage: {exInnerExceptionMessage}",
                errorModel.ExceptionMessage, errorModel.ExceptionStackTrace, errorModel.InnerExceptionMessage);

            switch (ex)
            {
                case ConflictException conflictException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                case NotFoundException notFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case Exception exception:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            await Task.CompletedTask;
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseClientInfoSystemExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientInfoSystemExceptionMiddleware>();
        }
    }
}
