using System.ComponentModel.DataAnnotations;

namespace Lubre.Entities;

public class Country :Entity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Iso2Code { get; set; }
    public string? Iso3Code { get; set; }
    public string? PhoneCode { get; set; }
    public virtual ICollection<State>? States { get; set; }
}