using cwiczenia8.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace cwiczenia8.Extentions
{
    public static class ExceptionLoggingMiddlewareExtention
    {
        public static IApplicationBuilder UseExceptionLoggingMiddleware (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}
