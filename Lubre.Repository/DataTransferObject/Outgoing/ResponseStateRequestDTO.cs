namespace Lubre.Repository.DataTransferObject.Outgoing;

public class ResponseStateRequestDTO
{
    public Guid Id { get; set; }
    public string StateName { get; set; } = string.Empty;//Provincia
    public string CountryName { get; set; } //Pais
}
    