namespace SP.Reports.Models.Api
{
    public class ApiPaths : IApiPaths
    {
        public string McpSmp { get; set; } = String.Empty;
        public string DayAheadMcp { get; set; } = String.Empty;
        public string RealTimeGeneration { get; set; } = String.Empty;
        public string Dpp { get; set; } = String.Empty;
        public string IntraDayAof { get; set; } = String.Empty;
        public string IntraDaySummary { get; set; } = String.Empty;
        public string IntraDayVolumeSummary { get; set; } = String.Empty;
        public string Smp { get; set; } = String.Empty;
        public string DppOrganization { get; set; } = String.Empty;
        public string DppInjectionUnitName { get; set; } = String.Empty;

    }
} 
