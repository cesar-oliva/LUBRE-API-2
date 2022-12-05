
using System.ComponentModel.DataAnnotations;

namespace Lubre.Entities;
//[Table("Documents")]
public class Document:Entity
{
    [Required]
    public DateTime Expires { get; set; }    
}