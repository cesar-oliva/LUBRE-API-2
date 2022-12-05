using System.ComponentModel.DataAnnotations;
using Lubre.Abstractions;

namespace Lubre.Entities;
public class Entity : IEntity
{
    [Key]
    public Guid Id { get ; set; }
}
