
using System.ComponentModel.DataAnnotations;
namespace Lubre.Entities;

public class Unit:Entity
{
    [Required]
    public string Name { get; set; }

    public virtual ICollection<Employee>? Employees { get; set; }
}