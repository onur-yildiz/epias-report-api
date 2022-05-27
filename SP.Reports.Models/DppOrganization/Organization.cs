using System.Text.Json.Serialization;

namespace SP.Reports.Models.Organizations
{
    public class Organization
    {
        [JsonPropertyName("organizationId")]
        public int OrganizationId { get; set; }

        [JsonPropertyName("organizationName")]
        public string? OrganizationName { get; set; }

        [JsonPropertyName("organizationStatus")]
        public string? OrganizationStatus { get; set; }

        [JsonPropertyName("organizationETSOCode")]
        public string? OrganizationETSOCode { get; set; }

        [JsonPropertyName("organizationShortName")]
        public string? OrganizationShortName { get; set; }
    }
}
