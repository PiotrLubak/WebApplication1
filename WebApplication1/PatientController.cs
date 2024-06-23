using Api.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _petientService;

        public PatientController(IPatientService petientService)
        {
            _petientService = petientService;
        }

        [HttpGet("{idPatient}")]
        public IActionResult GetPatinentDetails([FromRoute] int idPatient)
        {
            try
            {
                var patient = _petientService.GetPatientWithVisists(idPatient);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}