namespace SP.Reports.Models.Api
{
    public interface IApiPaths
    {
        string DayAheadMcp { get; set; }
        string Dpp { get; set; }
        string DppInjectionUnitName { get; set; }
        string DppOrganization { get; set; }
        string IntraDayAof { get; set; }
        string IntraDaySummary { get; set; }
        string IntraDayVolumeSummary { get; set; }
        string McpSmp { get; set; }
        string RealTimeGeneration { get; set; }
        string Smp { get; set; }
    }
}