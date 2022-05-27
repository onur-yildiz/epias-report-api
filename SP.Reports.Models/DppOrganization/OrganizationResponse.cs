using System.Text.Json.Serialization;

namespace SP.Reports.Models.Organizations
{
    public class OrganizationResponse : IResponseBase<OrganizationContainer>
    {
        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }

        [JsonPropertyName("body")]
        public OrganizationContainer? Body { get; set; }
    }
}
