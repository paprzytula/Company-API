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
/// Endpoint used to interact with the Departments in the company's database
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public DepartmentsController(IDepartmentRepository departmentRepository,
            ILoggerService logger, 
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _departmentRepository = departmentRepository;
        }
        /// <summary>
        /// Get all Departments
        /// </summary>
        /// <returns>List of Departments</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                _logger.LogInfo("Attempted to get all Departments");
                var departments = await _departmentRepository.FindAll();
                var response = _mapper.Map<IList<DepartmentDTO>>(departments);
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");


            }
        }
        /// <summary>
        /// Get Department by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Department's record</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDepartment(int id)
        {
            try
            {
                _logger.LogInfo($"Attempted to get a Department with id: {id}");
                var department = await _departmentRepository.FindById(id);
                if (department == null)
                {
                    _logger.LogWarn($"Department with id: {id} was not found.");
                    return NotFound();
                }
                var response = _mapper.Map<DepartmentDTO>(department);
                _logger.LogInfo($"Successfully got a Department with id: {id}");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");

            }

        }
        /// <summary>
        /// Creates a Department
        /// </summary>
        /// <param name="departmentDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDTO departmentDTO)
        {
            try
            {
                _logger.LogInfo($"Department submission attempted.");
                if (departmentDTO == null)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Department Data was Invalid.");
                    return BadRequest(ModelState);

                }
                var department = _mapper.Map<Department>(departmentDTO);
                var isSuccess = await _departmentRepository.Create(department);
                if (!isSuccess)
                {
                    return InternalError($"Department creation failed.");
                }
                _logger.LogInfo("Department created");
                return Created("Create", new { department });
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }

        }
        /// <summary>
        /// Updates a Department by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentUpdateDTO departmentDTO)
        {

            try
            {
                _logger.LogInfo($"Department Update attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()) || departmentDTO == null
                    || id != departmentDTO.IdDepartment)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest();
                }

                var isExists = await _departmentRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"Department Update failed: no Department with id: {id} was found.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Department Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var department = _mapper.Map<Department>(departmentDTO);
                var isSuccess = await _departmentRepository.Update(department);
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
        /// Deletes a Department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                _logger.LogInfo($"Department Delete attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    _logger.LogWarn($"Department Delete failed: no  id was provided.");
                    return BadRequest();
                }
                var isExists = await _departmentRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"Department Update failed: no Department with id: {id} was found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Department Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var department = await _departmentRepository.FindById(id);
                var isSuccess = await _departmentRepository.Delete(department);
                if (!isSuccess)
                {
                    return InternalError($"Delete Department Operation failed.");
                }

                _logger.LogWarn($"Department with id: {id} successfully deleted.");
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
