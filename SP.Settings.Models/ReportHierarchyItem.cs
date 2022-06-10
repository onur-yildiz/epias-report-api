using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace SP.Settings.Models
{
    [BsonIgnoreExtraElements]
    public class ReportHierarchyItem
    {
        //[BsonRequired]
        //[BsonElement("reportEndpoint")]
        //public string ReportEndpoint { get; set; }

        [BsonRequired]
        [BsonElement("order")]
        public int Order { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonElement("folderName")]
        public ReportName[]? FolderName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonElement("children")]
        public List<ReportHierarchyItem>? Children { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonElement("reportName")]
        public ReportName[]? ReportName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonElement("reportKey")]
        public string? ReportKey { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonElement("path")]
        public string? Path { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [BsonElement("roles")]
        public string[]? Roles { get; set; }

        //[BsonElement("queryParams")]
        //public List<string> QueryParams { get; set; }
    }
}