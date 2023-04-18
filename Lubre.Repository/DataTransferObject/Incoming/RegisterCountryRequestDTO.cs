namespace Lubre.Repository.DataTransferObject.Incoming;

public class RegisterCountryRequestDTO
{
    public string CountryName{ get; set; } = string.Empty;
    public string? Iso2Code { get; set; }
    public string? Iso3Code { get; set; }
    public string? PhoneCode { get; set; }
}