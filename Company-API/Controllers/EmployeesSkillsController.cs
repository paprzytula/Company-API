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
/// Endpoint used to interact with the Employees Skills in the company's database
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class EmployeesSkillsController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeesSkillRepository _employeesSkillRepository;
        public EmployeesSkillsController(IEmployeesSkillRepository employeesSkillRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _employeesSkillRepository = employeesSkillRepository;
        }
        /// <summary>
        /// Get all Employees Skills
        /// </summary>
        /// <returns>List of Employees Skills</returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployeesSkills()
        {
            try
            {
                var employeesSkills = await _employeesSkillRepository.FindAll();
                var response = _mapper.Map<IList<CategoryDTO>>(employeesSkills);
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
