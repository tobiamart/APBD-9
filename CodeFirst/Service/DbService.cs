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

        public async Task<List<GetPatientDTO>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Select(p => new GetPatientDTO
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate
                })
                .ToListAsync();
        }
    }
}