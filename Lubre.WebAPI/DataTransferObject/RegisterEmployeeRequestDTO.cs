
namespace Lubre.WebAPI.DataTransferObject;

public class RegisterEmployeeRequestDTO
{
    public Guid Id { get; set; }
    public string DniNumber { get; set; } //DNI
    public string Name { get; set; } //1er y 2do nombre
    public string LastName { get; set; } //apellido
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; } //fecha de nacimiento
    public int Age { get;}
    public string Address { get; set; } //direccion    
    public string PhotoUrl { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public int FileNumber { get; set; } //legajo
    public string CuilNumber { get; set; } //CUIL  
    public DateTime StartDate { get; set; } //fecha de ingreso
    public DateTime EndDate { get; set; } //fecha de Salida
    public int antiquity { get;}
    public string Unit { get; set; }
    public string Position { get; set; }    

}
    