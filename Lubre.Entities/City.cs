using System.ComponentModel.DataAnnotations;

namespace Lubre.Entities;

public class City :Entity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public required virtual Town Town { get; set; }
}