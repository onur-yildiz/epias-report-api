using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SP.Reports.Models.Organizations
{
    public class OrganizationContainer
    {
        [JsonPropertyName("organizations")]
        public Organization[]? Organizations { get; set; }
    }
}
