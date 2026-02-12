using System.Text.Json;
using Universal.Core.Exceptions;

namespace Universal.Core.Services
{
    public interface IErrorMessageService
    {
        string GetMessage(ErrorCode errorCode);
    }
    public class ErrorMessageService : IErrorMessageService
    {
        private readonly Dictionary<string, string> _errors;

        public ErrorMessageService()
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Resources",
                "errors.json");

            var json = File.ReadAllText(filePath);

            _errors = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                      ?? new Dictionary<string, string>();
        }

        public string GetMessage(ErrorCode errorCode)
        {
            var key = ((int)errorCode).ToString();

            return _errors.TryGetValue(key, out var message)
                ? message
                : "Unknown error";
        }
    }
}