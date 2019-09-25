using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DPD.WebApp
{
    public class LoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine($"request started at {DateTime.Now:hh:mm:ss.fff}");
            
            await next.Invoke(context);

            Console.WriteLine($"request ended at {DateTime.Now:hh:mm:ss.fff}");
        }
    }
}