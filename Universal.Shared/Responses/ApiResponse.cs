using System.Text.Json;

namespace Universal.Shared.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            ErrorCode = 1000;
            Success = true;
            Error = "No Records";
        }

        public int ErrorCode { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public void SetSuccess()
        {
            Success = true;
            ErrorCode = 0;
            Error = "";
        }

        public void SetSuccess(T data)
        {
            this.Data = data;
            Success = true;
            ErrorCode = 0;
            Error = "";
        }

        public void SetFailure(string failureMessage)
        {
            Success = false;
            ErrorCode = 417;
            Error = failureMessage;
        }

        public void SetAccessDenied()
        {
            Success = false;
            ErrorCode = 403;
            Error = "Access Denied";
        }

        public void SetStatus(bool isSuccess)
        {
            if (isSuccess)
            {
                SetSuccess();
            }
            else
                SetFailure("");
        }

        public void SetInvalidModel()
        {
            Success = false;
            ErrorCode = 422;
            Error = "Invalid Model";
        }

        public bool IsSuccess()
        {
            return ErrorCode == 0 ? true : false;
        }
    }
}