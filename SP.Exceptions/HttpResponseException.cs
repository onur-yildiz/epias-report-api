using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace SP.Exceptions
{
    [Serializable]
    public class HttpResponseException : Exception, IHttpResponseException
    {
        public HttpResponseException(int statusCode, string message, object? value = null) : base(message) =>
            (ResponseCode, Value) = (statusCode, value);

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public int ResponseCode { get; }
        public object? Value { get; }

        private static string AppendDetails(string main, string? details) => main + (details == null ? "" : $": {details}");

        public static HttpResponseException DatabaseError(string? details = null) => new(StatusCodes.Status502BadGateway, AppendDetails("An error occured on database", details));
        public static HttpResponseException MaxApiKeys() => new(StatusCodes.Status502BadGateway, "Max allowed API keys have reached");
        public static HttpResponseException IncorrectPassword() => new(StatusCodes.Status401Unauthorized, "Incorrect password");
        public static HttpResponseException NotExists(string item = "Item") => new(StatusCodes.Status404NotFound, $"{item} does not exist");
        public static HttpResponseException AlreadyExists(string item = "Item") => new(StatusCodes.Status409Conflict, $"{item} already exists");
        public static HttpResponseException InvalidToken(string? details = null) => new(StatusCodes.Status400BadRequest, AppendDetails("Invalid token", details));
        public static HttpResponseException NoRolesExist() => new(StatusCodes.Status404NotFound, "Could not find any roles");
        public static HttpResponseException Forbidden(string? details = null) => new(StatusCodes.Status403Forbidden, AppendDetails("Forbidden", details));
        public static HttpResponseException Unauthorized(string? details = null) => new(StatusCodes.Status401Unauthorized, AppendDetails("Unauthorized", details));
        public static HttpResponseException AccountMismatch() => new(StatusCodes.Status403Forbidden, "Account Mismatch");

    }
}
