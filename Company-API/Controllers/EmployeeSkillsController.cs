using AutoMapper;
using Company_API.Contracts;
using Company_API.Data;
using Company_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the Employees Skills in the company's database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class EmployeeSkillsController : ControllerBase
    {
        private readonly IEmployeeSkillRepository _employeeSkillRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public EmployeeSkillsController(IEmployeeSkillRepository employeeSkillRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _employeeSkillRepository = employeeSkillRepository;
        }
        /// <summary>
        /// Get all Employees Skills
        /// </summary>
        /// <returns>List of Employees Skills</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeSkills()
        {
            try
            {
                _logger.LogInfo("Attempted to get all Employees Skills");
                var employeeSkills = await _employeeSkillRepository.FindAll();
                var response = _mapper.Map<IList<EmployeeSkillDTO>>(employeeSkills);
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");


            }
        }
        /// <summary>
        /// Get EmployeeSkill by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A EmployeeSkill's record</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetEmployeeSkill(Guid id)
        {
            try
            {
                _logger.LogInfo($"Attempted to get a EmployeeSkill with id: {id}");
                var employeeSkill = await _employeeSkillRepository.FindById(id);
                if (employeeSkill == null)
                {
                    _logger.LogWarn($"EmployeeSkill with id: {id} was not found.");
                    return NotFound();
                }
                var response = _mapper.Map<EmployeeSkillDTO>(employeeSkill);
                _logger.LogInfo($"Successfully got a EmployeeSkill with id: {id}");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");

            }

        }
        /// <summary>
        /// Creates a EmployeeSkill
        /// </summary>
        /// <param name="employeeSkillDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] EmployeeSkillCreateDTO employeeSkillDTO)
        {
            try
            {
                _logger.LogInfo($"EmployeeSkill submission attempted.");
                if (employeeSkillDTO == null)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"EmployeeSkill Data was Invalid.");
                    return BadRequest(ModelState);

                }
                var employeeSkill = _mapper.Map<EmployeeSkill>(employeeSkillDTO);
                var isSuccess = await _employeeSkillRepository.Create(employeeSkill);
                if (!isSuccess)
                {
                    return InternalError($"EmployeeSkill creation failed.");
                }
                _logger.LogInfo("EmployeeSkill created");
                return Created("Create", new { employeeSkill });
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }

        }
        /// <summary>
        /// Updates a EmployeeSkill by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeSkillDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeSkillUpdateDTO employeeSkillDTO)
        {
            var isExists = await _employeeSkillRepository.IsExists(id);
            try
            {
                _logger.LogInfo($"EmployeeSkill Update attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()) || employeeSkillDTO == null
                    || id != employeeSkillDTO.IdSkill)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest();
                }

                
                if (!isExists)
                {
                    _logger.LogWarn($"EmployeeSkill Update failed: no EmployeeSkill with id: {id} was found.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"EmployeeSkill Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var employeeSkill = _mapper.Map<EmployeeSkill>(employeeSkillDTO);
                var isSuccess = await _employeeSkillRepository.Update(employeeSkill);
                if (!isSuccess)
                {
                    return InternalError($"Update Operation failed.");
                }

                return NoContent();
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }

        }
        /// <summary>
        /// Deletes a EmployeeSkill by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {

            try
            {
                _logger.LogInfo($"EmployeeSkill Delete attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    _logger.LogWarn($"EmployeeSkill Delete failed: no  id was provided.");
                    return BadRequest();
                }
                var isExists = await _employeeSkillRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"EmployeeSkill Update failed: no EmployeeSkill with id: {id} was found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"EmployeeSkill Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var employeeSkill = await _employeeSkillRepository.FindById(id);
                var isSuccess = await _employeeSkillRepository.Delete(employeeSkill);
                if (!isSuccess)
                {
                    return InternalError($"Delete EmployeeSkill Operation failed.");
                }

                _logger.LogWarn($"EmployeeSkill with id: {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator.");
        }
    }

}
