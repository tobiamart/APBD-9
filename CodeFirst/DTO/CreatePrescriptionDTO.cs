namespace CodeFirst.DTO;

public class CreatePrescriptionDTO
{
    public GetPatientDto Patient { get; set; }
    
    public int IdDoctor { get; set; }
    
    public List<GetPrescriptionMedicamentDto> Medicaments { get; set; }
    
    public DateTime Date {get;set;}
    
    public DateTime DueDate {get;set;}
}