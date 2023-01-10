using Microsoft.AspNetCore.Diagnostics;
using PersonManagementApi.Models;
using System.Net;
using System.Text.Json;

namespace PersonManagementApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            Guid logId = Guid.NewGuid();

            ApiResponse<string> apiResponse = new ApiResponse<string>
            {
                IsSuccess = false,
                Messages = new List<string> { $"logId : {logId}","Internal Server Error" }
            };

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogError($"logId:{logId}-{exception.ToString()}");
            var result = JsonSerializer.Serialize(apiResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
