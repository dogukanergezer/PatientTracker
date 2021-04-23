
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PatientTracker.Business.Abstract;
using PatientTracker.Business.LoggerService;
using PatientTracker.Entities.Models;

using System.Threading.Tasks;

namespace PatientTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private ILoggerManager _logger;
        private IPatientService _patientService;

        public PatientController(ILoggerManager logger,IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }

        /// <summary>
        /// Get Patient By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientById(id);
            if (patient != null)
            {
                _logger.LogInfo($"Returned patient with id: {id}");
                return Ok(patient);
            }
            _logger.LogError($"Patient with id: {id}, hasn't been found in db.");
            return NotFound();
        }
     
        /// <summary>
        /// Create an Patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            //  [ApiController] olmasaydı Model.IsValid ile test etmemiz gerekirdi
            var createdPatient = await _patientService.AddPatient(patient);
            return Ok(createdPatient);
        }
        /// <summary>
        /// Update the patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdatePatient([FromBody] Patient patient)
        {
            var patientDb = await _patientService.GetPatientById(patient.Id);

            if (patientDb != null)
            {
                patientDb.Name = patient.Name;
                patientDb.OwnerInfo = patient.OwnerInfo;
                patientDb.PictureUrl = patient.PictureUrl;
                patientDb.Genus = patient.Genus;
                patientDb.Diagnosis = patient.Diagnosis;
                await _patientService.UpdatePatient(patientDb);
                _logger.LogInfo($"Patient has updated successfully");
                return Ok(patient);
            }
            _logger.LogError($"Patient hasn't been found in db.");
            return NotFound();
        }
        /// <summary>
        /// Delete the patient
        /// </summary>
        /// <param name="id"></param>

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            if (await _patientService.GetPatientById(id) != null)
            {
                await _patientService.DeletePatient(id);
                _logger.LogInfo($"Patient has deleted successfully");
                return Ok();//200
            }
            _logger.LogError($"Patient with id: {id}, hasn't been found in db.");
            return NotFound();

        }
        /// <summary>
        /// Get patients by query
        /// </summary>
        /// <param name="patientParameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]

        public IActionResult GetPatients([FromQuery] PatientParameters patientParameters)
        {
           
            var patients = _patientService.GetPatients(patientParameters);
            var metadata = new
            {
                patients.TotalCount,
                patients.PageSize,
                patients.CurrentPage,
                patients.TotalPages,
                patients.HasNext,
                patients.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            _logger.LogInfo($"Returned {patients.TotalCount} patients from database.");
            return Ok(patients);
        }
        
      
    }
}
