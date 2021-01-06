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
    /// Endpoint used to interact with the Employees in the company's database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _employeeRepository = employeeRepository;
        }
        /// <summary>
        /// Get all Employees
        /// </summary>
        /// <returns>List of Employees</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                _logger.LogInfo("Attempted to get all Employees");
                var employees = await _employeeRepository.FindAll();
                var response = _mapper.Map<IList<EmployeeDTO>>(employees);
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");


            }
        }
        /// <summary>
        /// Get Employee by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Employee's record</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetEmployee(Guid id)
        {
            try
            {
                _logger.LogInfo($"Attempted to get a Employee with id: {id}");
                var employee = await _employeeRepository.FindById(id);
                if (employee == null)
                {
                    _logger.LogWarn($"Employee with id: {id} was not found.");
                    return NotFound();
                }
                var response = _mapper.Map<EmployeeDTO>(employee);
                _logger.LogInfo($"Successfully got a Employee with id: {id}");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");

            }

        }
        /// <summary>
        /// Creates a Employee
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDTO employeeDTO)
        {
            try
            {
                _logger.LogInfo($"Employee submission attempted.");
                if (employeeDTO == null)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Employee Data was Invalid.");
                    return BadRequest(ModelState);

                }
                var employee = _mapper.Map<Employee>(employeeDTO);
                var isSuccess = await _employeeRepository.Create(employee);
                if (!isSuccess)
                {
                    return InternalError($"Employee creation failed.");
                }
                _logger.LogInfo("Employee created");
                return Created("Create", new { employee });
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }

        }
        /// <summary>
        /// Updates a Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeUpdateDTO employeeDTO)
        {

            try
            {
                _logger.LogInfo($"Employee Update attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()) || employeeDTO == null
                    || id != employeeDTO.IdEmployee)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest();
                }

                var isExists = await _employeeRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"Employee Update failed: no Employee with id: {id} was found.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Employee Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var employee = _mapper.Map<Employee>(employeeDTO);
                var isSuccess = await _employeeRepository.Update(employee);
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
        /// Deletes a Employee by id
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
                _logger.LogInfo($"Employee Delete attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    _logger.LogWarn($"Employee Delete failed: no  id was provided.");
                    return BadRequest();
                }
                var isExists = await _employeeRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"Employee Update failed: no Employee with id: {id} was found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Employee Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var employee = await _employeeRepository.FindById(id);
                var isSuccess = await _employeeRepository.Delete(employee);
                if (!isSuccess)
                {
                    return InternalError($"Delete Employee Operation failed.");
                }

                _logger.LogWarn($"Employee with id: {id} successfully deleted.");
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
