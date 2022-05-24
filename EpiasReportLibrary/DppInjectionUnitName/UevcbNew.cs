using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EpiasReportLibrary.DppInjectionUnitName
{
    public class UevcbNew
    {
        [JsonPropertyName("eic")]
        public string? Eic { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
