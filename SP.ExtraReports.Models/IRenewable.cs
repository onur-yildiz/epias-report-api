namespace SP.ExtraReports.Models
{
    public interface IRenewable
    {
        double Biomass { get; set; }
        double DammedHydro { get; set; }
        double Geothermal { get; set; }
        double River { get; set; }
        double Sun { get; set; }
        double Wind { get; set; }
    }
}