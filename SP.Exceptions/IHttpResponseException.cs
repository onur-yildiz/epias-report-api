using System.Runtime.Serialization;

namespace SP.Exceptions
{
    public interface IHttpResponseException
    {
        int ResponseCode { get; }
        object? Value { get; }

        void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}