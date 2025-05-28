namespace CodeFirst.DTO;

public class GetPatientDto
{ 
    public int IdPatient { get; set; } 
    public string FirstName { get; set; } = null!; 
    public string LastName { get; set; } = null!; 
    public DateTime Birthdate { get; set; }
}