using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdWebApi.Middleware
{
    public class ApiKeyAuth
    {
        private readonly RequestDelegate __next;
        private const string APIKEY = "ApiKey";
        private string __allowedKey = string.Empty;

        public ApiKeyAuth(RequestDelegate next, IServiceProvider serviceProvider)
        {
            __next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(GetResponse("ApiKey was not provided.", response.StatusCode));

                return;
            }

            if (string.IsNullOrWhiteSpace(__allowedKey))
            {
                var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
                __allowedKey = appSettings.GetValue<string>(APIKEY);
            }

            if (__allowedKey != extractedApiKey)
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(GetResponse("Unauthorized ApiKey.", response.StatusCode));

                return;
            }

            await __next(context);
        }

        private string GetResponse(string error, int statusCode)
        {
            return JsonSerializer.Serialize(new
            {
                title = error,
                status = statusCode
            });
        }
    }
}
