using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models;

[Table("Patient")]
public class Patient
{
   [Key]
   public int IdPatient { get; set; }
   
   [MaxLength(100)]
   public string FirstName { get; set; }
   
   [MaxLength(100)]
   public string LastName { get; set; }
   
   public DateTime Birthdate { get; set; }
   
   public virtual ICollection<Prescription> Prescriptions { get; set; }
}