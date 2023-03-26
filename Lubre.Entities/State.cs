using System.ComponentModel.DataAnnotations;

namespace Lubre.Entities;

public class State :Entity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public virtual Country? Country { get; set; }
    public virtual ICollection<Town>? Towns { get; set; }
}