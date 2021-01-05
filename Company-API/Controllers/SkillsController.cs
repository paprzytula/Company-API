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
/// Endpoint used to interact with the Skills in the company's database
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class SkillsController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly ISkillRepository _skillRepository;
        public SkillsController(ISkillRepository skillRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _skillRepository = skillRepository;
        }
        /// <summary>
        /// Get all Skills
        /// </summary>
        /// <returns>List of Skills</returns>
        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            try
            {
     var skills = await _skillRepository.FindAll();
            var response = _mapper.Map<IList<SkillDTO>>(skills);
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
