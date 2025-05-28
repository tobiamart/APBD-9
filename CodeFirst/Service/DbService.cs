using CodeFirst.Data;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeFirst.DTO;

namespace CodeFirst.Services
{
    public class DbService
    {
        private readonly AppDbContext _context;

        public DbService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetPatientDto>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Select(p => new GetPatientDto
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate
                })
                .ToListAsync();
        }

        public async Task<GetPatientDto?> GetPatientByIdAsync(int idPatient)
        {
            return await _context.Patients
                .Select(p => new GetPatientDto
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate
                })
                .FirstOrDefaultAsync(p => p.IdPatient == idPatient);
        }

        public async Task<int>  CreatePrescription(CreatePrescriptionDTO dto)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                
                if(dto.Date >= dto.DueDate)
                    throw new Exception("Date must be after due date");
                var prescription = new Prescription
                {
                    IdPatient = dto.Patient.IdPatient,
                    IdDoctor = dto.IdDoctor,
                    Date = dto.Date,
                    DueDate = dto.DueDate
                };
                await _context.Prescriptions.AddAsync(prescription);
                await _context.SaveChangesAsync(); 

                for (int i = 0; i < dto.Medicaments.Count; i++)
                {
                    var medicaments = await _context.Medicaments
                        .Select(p => new { p.IdMedicament })
                        .Where(p => p.IdMedicament == dto.Medicaments[i].IdMedicament)
                        .ToListAsync();
                     
                    var medicamentsCount = medicaments.Count;
                    
                    if (medicamentsCount == 0)
                    {
                        throw new Exception("Medicaments must be exist");
                    }
                    
                    var prescriptionMedicament = new PrescriptionMedicament
                    {
                        IdMedicament = dto.Medicaments[i].IdMedicament,
                        IdPrescription = prescription.IdPrescription,
                        Dose = dto.Medicaments[i].Dose,
                        Details = dto.Medicaments[i].Details
                    };
                    await _context.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}