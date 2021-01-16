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
{/// <summary>
/// Endpoint used to interact with the Skills in the company's database
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSkills()
        {
            try
            {
                _logger.LogInfo("Attempted to get all Skills");
                var skills = await _skillRepository.FindAll();
            var response = _mapper.Map<IList<SkillDTO>>(skills);
            return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");


            }
        }
        /// <summary>
        /// Get Skill by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Skill's record</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetSkill(int id)
        {
            try
            {
                _logger.LogInfo($"Attempted to get a Skill with id: {id}");
                var skill = await _skillRepository.FindById(id);
                if (skill == null)
                {
                    _logger.LogWarn($"Skill with id: {id} was not found.");
                    return NotFound();
                }
                var response = _mapper.Map<SkillDTO>(skill);
                _logger.LogInfo($"Successfully got a Skill with id: {id}");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");

            }

        }
        /// <summary>
        /// Creates a Skill
        /// </summary>
        /// <param name="skillDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SkillCreateDTO skillDTO)
        {
            try
            {
                _logger.LogInfo($"Skill submission attempted.");
                if (skillDTO == null)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Skill Data was Invalid.");
                    return BadRequest(ModelState);

                }
                var skill = _mapper.Map<Skill>(skillDTO);
                var isSuccess = await _skillRepository.Create(skill);
                if (!isSuccess)
                {
                    return InternalError($"Skill creation failed.");
                }
                _logger.LogInfo("Skill created");
                return Created("Create", new { skill });
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }

        }
        /// <summary>
        /// Updates a Skill by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="skillDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SkillUpdateDTO skillDTO)
        {

            try
            {
                _logger.LogInfo($"Skill Update attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()) || skillDTO == null
                    || id != skillDTO.Id)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest();
                }

                var isExists = await _skillRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"Skill Update failed: no Skill with id: {id} was found.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Skill Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var skill = _mapper.Map<Skill>(skillDTO);
                var isSuccess = await _skillRepository.Update(skill);
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
        /// Deletes a Skill by id
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
                _logger.LogInfo($"Skill Delete attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    _logger.LogWarn($"Skill Delete failed: no  id was provided.");
                    return BadRequest();
                }
                var isExists = await _skillRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"Skill Update failed: no Skill with id: {id} was found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Skill Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var skill = await _skillRepository.FindById(id);
                var isSuccess = await _skillRepository.Delete(skill);
                if (!isSuccess)
                {
                    return InternalError($"Delete Skill Operation failed.");
                }

                _logger.LogWarn($"Skill with id: {id} successfully deleted.");
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