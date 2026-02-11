
using Universal.Shared.Responses;

namespace Universal.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Data = ex.StackTrace ?? string.Empty,
                    Error = "Internal Server Error",
                    ErrorCode = StatusCodes.Status500InternalServerError
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}