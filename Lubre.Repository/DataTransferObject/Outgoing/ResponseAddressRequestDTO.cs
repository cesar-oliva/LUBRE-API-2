namespace Lubre.Repository.DataTransferObject.Outgoing;

public class ResponseAddressRequestDTO
{
    public Guid Id { get; set; }
    public string Street { get; set; } = string.Empty;//calle
    public string Number { get; set; } = string.Empty; //numero
    public string Floor { get; set; } = string.Empty; //piso
    public string Flat { get; set; } = string.Empty; //departamento
    public string Neighborhood{ get; set; } = string.Empty; //barrio
    public string Lot{ get; set; } = string.Empty;//lote
    public string House{ get; set; } = string.Empty;//casa
    public string Block{ get; set; } = string.Empty; //Manzana
    public string CountryName { get; set; } //pais
    public string StateName { get; set; } //Provincia
    public string TownName { get; set; } //Departamento
    public string CityName { get; set; } //Localidad
}
    