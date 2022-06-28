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

        /// <summary>
        /// Response code includes standard response status codes and API specific ones.
        /// </summary>
        public int ResponseCode { get; }

        /// <summary>
        /// Message regarding the response
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Requested data
        /// </summary>
        public T? Value { get; }

        public static ApiResponse<T> Success(T? value = default) => new(StatusCodes.Status200OK, "Success", value);
    }
}