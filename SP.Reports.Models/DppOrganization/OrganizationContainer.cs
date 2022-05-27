using System.Text.Json.Serialization;

namespace SP.Reports.Models.Organizations
{
    public class OrganizationContainer
    {
        [JsonPropertyName("organizations")]
        public Organization[]? Organizations { get; set; }
    }
}
