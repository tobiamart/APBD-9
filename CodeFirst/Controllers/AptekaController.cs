using CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    }
}