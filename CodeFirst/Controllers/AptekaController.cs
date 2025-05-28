using CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CodeFirst.DTO;
using CodeFirst.Models;

namespace CodeFirst.Controllers
{
    [ApiController]
    [Route("apteka")]
    public class AptekaController : ControllerBase
    {
        private readonly DbService _dbService;

        public AptekaController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _dbService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("patients/{idPatient}")]
        public async Task<IActionResult> GetPatientById(int idPatient)
        {
            var patient = await _dbService.GetPatientByIdAsync(idPatient);
            return Ok(patient);
        }

        [HttpPost("prescriptions")]
        public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionDTO dto)
        {
            var result = await _dbService.CreatePrescription(dto);
            if (result == 0)
            {
                return BadRequest();
            }
            return Created();
        }
    }
}