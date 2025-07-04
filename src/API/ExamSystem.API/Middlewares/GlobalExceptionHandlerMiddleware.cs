using ExamSystem.Application.Exceptions.Base;

namespace ExamSystem.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            
            catch (BaseException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)ex.Status;
                var res = new { Status = (int)ex.Status, ex.Message };
                await context.Response.WriteAsJsonAsync(res);
            }
            
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                var res = new { Status = 500, ex.Message };
                await context.Response.WriteAsJsonAsync(res);
            }

        }
    }
}
