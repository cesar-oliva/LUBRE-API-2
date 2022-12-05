using System.ComponentModel.DataAnnotations;
using Lubre.Abstractions;

namespace Lubre.Entities;

public class Employee : Person, IEntity
{
    [Required]
    public int FileNumber { get; set; } //legajo
    [Required]
    [StringLength (11)]
    public string CuilNumber { get; set; } //CUIL  
    [Required]
    public DateTime StartDate { get; set; } //fecha de ingreso
    private DateTime EndDate { get; set; } //fecha de 
    public Guid UnitId { get; set; }
    public Unit Unit { get; set; }
    public Guid PositionId { get; set; }
    public Position Position { get; set; }
    public virtual ICollection<Document> Documents { get; set; }

    public int antiquity(){
        return DateTime.Now.Year - StartDate.Year;       
    }
}