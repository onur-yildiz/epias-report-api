using MongoDB.Bson.Serialization.Attributes;

namespace SP.Settings.Models
{
    public class Report
    {
        //[BsonRequired]
        //[BsonElement("reportEndpoint")]
        //public string ReportEndpoint { get; set; }

        [BsonRequired]
        [BsonElement("order")]
        public int Order { get; set; }

        [BsonElement("name")]
        public ReportName[] Name { get; set; }

        [BsonElement("children")]
        public object[] Children { get; set; } // TODO investigate error when type is Report[]

        [BsonElement("reportName")]
        public ReportName[] ReportName { get; set; }

        [BsonElement("path")]
        public string Path { get; set; }

        //[BsonElement("queryParams")]
        //public List<string> QueryParams { get; set; }
    }
}