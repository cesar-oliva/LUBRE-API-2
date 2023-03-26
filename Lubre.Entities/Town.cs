using System.ComponentModel.DataAnnotations;

namespace Lubre.Entities;

public class Town :Entity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public required virtual State State { get; set; }
    public virtual ICollection<City>? Cities { get; set; }

}