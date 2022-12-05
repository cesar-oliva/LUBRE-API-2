
using System.ComponentModel.DataAnnotations;

namespace Lubre.Entities;


public class Gender:Entity
{
[Required]
public string Name { get; set; }
}