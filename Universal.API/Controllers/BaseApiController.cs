using Microsoft.AspNetCore.Mvc;
using Universal.Shared.Responses;

namespace Universal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        protected new ApiResponse<T> Response<T>(T data)
        {
            var result = new ApiResponse<T>();

            if (data != null)
            {
                result.SetSuccess(data);
            }

            return result;
        }

        [NonAction]
        protected new ApiResponse<T> Response<T>(string errorMessage, bool isSuccess)
        {
            var result = new ApiResponse<T>();

            if (isSuccess)
            {
                result.SetSuccess();
            }
            else
            {
                result.SetFailure(errorMessage);
            }

            return result;
        }

        [NonAction]
        protected new ApiResponse<List<T>> Response<T>(List<T>? data)
        {
            var result = new ApiResponse<List<T>>();

            if (data != null)
            {
                if (data.Count > 0)
                    result.SetSuccess(data);
                else
                    result.Data = data;
            }

            return result;
        }

        [NonAction]
        protected new ApiResponse<T> Response<T>(T data, bool setSuccess, string errorMessage)
        {
            var result = new ApiResponse<T>();

            if (setSuccess)
            {
                result.SetSuccess(data);
            }
            else
            {
                result.SetFailure(errorMessage);
            }

            return result;
        }
    }
}
