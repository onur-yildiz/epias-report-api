using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EpiasReportLibrary.DppInjectionUnitName
{
    public class DppInjectionUnitNameContainer
    {
        [JsonPropertyName("injectionUnitNames")]
        public UevcbNew[]? InjectionUnitNames { get; set; }
    }
}
