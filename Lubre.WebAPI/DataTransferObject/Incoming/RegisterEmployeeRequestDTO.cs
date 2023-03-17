
namespace Lubre.WebAPI.DataTransferObject.Incoming;

public class RegisterEmployeeRequestDTO
{
    public string DniNumber { get; set; } //DNI
    public string Name { get; set; } //1er y 2do nombre
    public string LastName { get; set; } //apellido
    public Guid GenderId { get; set; }
    public DateTime DateOfBirth { get; set; } //fecha de nacimiento
    public string Address { get; set; } //direccion    
    public string PhotoUrl { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public int FileNumber { get; set; } //legajo
    public string CuilNumber { get; set; } //CUIL  
    public DateTime StartDate { get; set; } //fecha de ingreso
    public DateTime EndDate { get; set; } //fecha de Salida
    public Guid UnitId { get; set; }
    public Guid PositionId { get; set; }    
}
    