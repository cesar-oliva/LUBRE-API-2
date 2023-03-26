
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lubre.Entities;

 public class Person:Entity
{
    [Required]
    [StringLength (8)]
    public string DniNumber { get; set; } //DNI
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } //1er y 2do nombre
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } //apellido
    [Required]
    public DateTime DateOfBirth { get; set; } //fecha de nacimiento
    [Required]
    [MaxLength(150)]
    public Guid AddressId { get; set; } //direccion    
    public Address Address { get; set; }
    public string PhotoUrl { get; set; }
    [Required(ErrorMessage = "Field can't be empty")]
    [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    [Required]
    public Guid GenderId { get; set; } //Id Sexo
    public Gender Gender { get; set; }
}