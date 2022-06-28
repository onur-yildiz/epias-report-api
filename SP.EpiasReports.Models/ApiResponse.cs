using Microsoft.AspNetCore.Http;

namespace SP.EpiasReports.Models
{
    public class ApiResponse<T> : IApiResponse<T>  where T : class
    {
        public ApiResponse(int statusCode, string message = "", T? value = default)
        {
            ResponseCode = statusCode;
            Message = message;
            Value = value;
        }

        public int ResponseCode { get; }
        public string Message { get; }
        public T? Value { get; }

        public static ApiResponse<T> Success(T? value = default) => new(StatusCodes.Status200OK, "Success", value);
    }
}