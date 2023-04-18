namespace Lubre.Repository.DataTransferObject.Outgoing;

public class ResponseCountryRequestDTO
{
    public Guid Id { get; set; }
    public string CountryName { get; set; } = string.Empty;//Provincia
    public string? Iso2Code { get; set; }
    public string? Iso3Code { get; set; }
    public string? PhoneCode { get; set; }
    public virtual ICollection<ResponseStateRequestDTO>? States { get; set; }
}

    