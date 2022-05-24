namespace SP.Reports.Models
{
    public interface IResponseBase<T> where T : class
    {
        public T? Body { get; set; }
        public string? ResultCode { get; set; }
        public string? ResultDescription { get; set; }
    }
}
