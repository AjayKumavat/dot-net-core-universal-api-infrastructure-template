
using Universal.Core.Exceptions;
using Universal.Core.Services;
using Universal.Shared.Responses;

namespace Universal.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly IErrorMessageService _errorMessageService;

        public ExceptionMiddleware(IErrorMessageService errorMessageService)
        {
            _errorMessageService = errorMessageService;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (UniversalException ex)
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";

                var message = _errorMessageService.GetMessage(ex.ErrorCode);

                var response = new ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = (int)ex.ErrorCode,
                    Error = message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = (int)ErrorCode.UnknownError,
                    Error = "Something went wrong"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}