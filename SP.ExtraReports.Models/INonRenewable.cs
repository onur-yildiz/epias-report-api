namespace SP.ExtraReports.Models
{
    public interface INonRenewable
    {
        double AsphaltiteCoal { get; set; }
        double BlackCoal { get; set; }
        double Fueloil { get; set; }
        double GasOil { get; set; }
        double ImportCoal { get; set; }
        double Lignite { get; set; }
        double Lng { get; set; }
        double Naphta { get; set; }
        double NaturalGas { get; set; }
        double Nucklear { get; set; }
        double Wasteheat { get; set; }
    }
}