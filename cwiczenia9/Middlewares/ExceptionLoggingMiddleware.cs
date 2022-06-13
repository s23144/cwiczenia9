using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace cwiczenia8.Middlewares
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);

            }catch(Exception ex)
            {
                await LogAsync(context, ex);
             
            }

        }

        public async Task LogAsync(HttpContext context , Exception exception)
        {
            using var writer = new StreamWriter("log,txt", true);
            await writer.WriteLineAsync($"{DateTime.Now},{exception.Message},{context.TraceIdentifier}");
            await _next(context);
        }

        

        


    }

   

    
}
