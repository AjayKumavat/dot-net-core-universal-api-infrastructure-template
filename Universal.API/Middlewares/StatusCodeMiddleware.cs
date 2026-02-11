
using Universal.Shared.Responses;

namespace Universal.API.Middlewares
{
    public class StatusCodeMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);

            if (context.Response.HasStarted)
                return;

            if (context.Response.StatusCode == 401)
            {
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<string>();
                response.Success = false;
                response.ErrorCode = 401;
                response.Error = "Unauthorized";

                await context.Response.WriteAsJsonAsync(response);
            }
            else if (context.Response.StatusCode == 403)
            {
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<string>();
                response.Success = false;
                response.ErrorCode = 403;
                response.Error = "Access Denied";

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
