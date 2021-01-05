using AutoMapper;
using Company_API.Contracts;
using Company_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Controllers
{/// <summary>
/// Endpoint used to interact with the Departments in the company's database
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
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
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
  var departments = await _departmentRepository.FindAll();
            var response = _mapper.Map<IList<DepartmentDTO>>(departments);
            return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message} - {e.InnerException}");

                return StatusCode(500, "Something went wrong. Please contact the Administrator.");

            }

        }
    }
}
