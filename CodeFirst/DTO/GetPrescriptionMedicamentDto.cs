namespace CodeFirst.DTO;

public class GetPrescriptionMedicamentDto
{
    public int IdMedicament { get; set; }
    
    public int IdPrescription { get; set; }
    
    public int Dose { get; set; }
    
    public string Details { get; set; }
    
}