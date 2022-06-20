namespace SP.Reports.Models.Api
{
    public class ApiPaths
    {
        public ApiPaths(string mcpSmp, string dayAheadMcp, string realTimeGeneration, string dpp, string intraDayAof, string intraDaySummary, string intraDayVolumeSummary, string smp, string dppOrganization, string dppInjectionUnitName)
        {
            McpSmp = mcpSmp;
            DayAheadMcp = dayAheadMcp;
            RealTimeGeneration = realTimeGeneration;
            Dpp = dpp;
            IntraDayAof = intraDayAof;
            IntraDaySummary = intraDaySummary;
            IntraDayVolumeSummary = intraDayVolumeSummary;
            Smp = smp;
            DppOrganization = dppOrganization;
            DppInjectionUnitName = dppInjectionUnitName;
        }

        public string McpSmp { get; set; } 
        public string DayAheadMcp { get; set; } 
        public string RealTimeGeneration { get; set; } 
        public string Dpp { get; set; } 
        public string IntraDayAof { get; set; } 
        public string IntraDaySummary { get; set; } 
        public string IntraDayVolumeSummary { get; set; } 
        public string Smp { get; set; } 
        public string DppOrganization { get; set; } 
        public string DppInjectionUnitName { get; set; } 

    }
}
