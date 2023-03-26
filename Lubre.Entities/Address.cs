using System.ComponentModel.DataAnnotations;

namespace Lubre.Entities;

public class Address :Entity
{
    
    [Required]
    public string Street { get; set; } = string.Empty;//calle
    [Required]
    public string Number { get; set; } = string.Empty; //numero
    public string Floor { get; set; } = string.Empty; //piso
    public string Flat { get; set; } = string.Empty; //departamento
    public string Neighborhood{ get; set; } = string.Empty; //barrio
    public string Lot{ get; set; } = string.Empty;//lote
    public string House{ get; set; } = string.Empty;//casa
    public string Block{ get; set; } = string.Empty; //Manzana
    public Guid CityId { get; set; }
    public City? City { get; set; }
}

