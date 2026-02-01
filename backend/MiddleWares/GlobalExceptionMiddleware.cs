using Serilog;
using System.Net;
using System.Text.Json;



namespace backend.MiddleWares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // 🔥 ALLOW CORS PREFLIGHT
            if (context.Request.Method == HttpMethods.Options)
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                return;
            }

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Unhandled exception | Path: {Path} | Method: {Method}",
                    context.Request.Path,
                    context.Request.Method);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }

}
