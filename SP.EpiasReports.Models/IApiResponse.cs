namespace SP.EpiasReports.Models
{
    public interface IApiResponse<T> where T : class
    {
        public string Message { get; }
        public int ResponseCode { get; }
        public T? Value { get; }
    }
}