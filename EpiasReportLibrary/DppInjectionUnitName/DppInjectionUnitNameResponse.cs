using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EpiasReportLibrary.DppInjectionUnitName
{
    public class DppInjectionUnitNameResponse: IResponseBase<DppInjectionUnitNameContainer>
    {
        [JsonPropertyName("body")]
        public DppInjectionUnitNameContainer? Body { get; set; }

        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }
    }
}
