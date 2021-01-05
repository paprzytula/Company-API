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
/// Endpoint used to interact with the Employees in the company's database
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class EmployeesController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
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
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
  var employees = await _employeeRepository.FindAll();
            var response = _mapper.Map<IList<EmployeeDTO>>(employees);
            return Ok(response);
            }
            catch (Exception e )
            {
                _logger.LogError($"{e.Message} - {e.InnerException}");
                return StatusCode(500, "Something went wrong. Please contact the Administrator.");

            }

        }
    }
}
