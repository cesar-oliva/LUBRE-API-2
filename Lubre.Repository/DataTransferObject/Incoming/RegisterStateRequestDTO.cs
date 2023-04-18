namespace Lubre.Repository.DataTransferObject.Incoming;

public class RegisterStateRequestDTO
{
    public string StateName{ get; set; } = string.Empty;//calle
    public Guid CountryId { get; set; }
}