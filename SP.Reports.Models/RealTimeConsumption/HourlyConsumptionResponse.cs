﻿using System.Text.Json.Serialization;

namespace SP.Reports.Models.RealTimeConsumption
{
    public class HourlyConsumptionResponse : IResponseBase<HourlyConsumptionContainer>
    {
        [JsonPropertyName("body")]
        public HourlyConsumptionContainer? Body { get; set; }

        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }
    }
}
