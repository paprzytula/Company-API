using AutoMapper;
using Company_API.Contracts;
using Company_API.Data;
using Company_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the Categories in the company's database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        
        public CategoriesController(ICategoryRepository categoryRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _categoryRepository = categoryRepository;

        }
        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns>List of Categories</returns>
        [HttpGet]
        [Authorize("Administrator, Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            var location = GetControllerNames();
            try
            {
                _logger.LogInfo("Attempted to get all Categories");
                var categories = await _categoryRepository.FindAll();
                var response = _mapper.Map<IList<CategoryDTO>>(categories);
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");


            }
        }
        /// <summary>
        /// Get Category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Category's record</returns>
        [HttpGet("{id}")]
        [Authorize("Administrator, Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetCategory(Guid id)
        {
            try
            {
                _logger.LogInfo($"Attempted to get a Category with id: {id}");
                var category = await _categoryRepository.FindById(id);
                if (category == null)
                {
                    _logger.LogWarn($"Category with id: {id} was not found.");
                    return NotFound();
                }
                var response = _mapper.Map<CategoryDTO>(category);
                _logger.LogInfo($"Successfully got a Category with id: {id}");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");

            }

        }
        /// <summary>
        /// Creates a Category
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO categoryDTO)
        {
            try
            {
                _logger.LogInfo($"Category submission attempted.");
                if (categoryDTO == null)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Category Data was Invalid.");
                    return BadRequest(ModelState);

                }
                var category = _mapper.Map<Category>(categoryDTO);
                var isSuccess = await _categoryRepository.Create(category);
                if (!isSuccess)
                {
                    return InternalError($"Category creation failed.");
                }
                _logger.LogInfo("Category created");
                return Created("Create", new { category });
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }

        }
        /// <summary>
        /// Updates a Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryUpdateDTO categoryDTO)
        {

            try
            {
                _logger.LogInfo($"Category Update attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()) || categoryDTO == null
                    || id != categoryDTO.IdCategory)
                {
                    _logger.LogWarn($"Empty Request was submitted.");
                    return BadRequest();
                }

                var isExists = await _categoryRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"Category Update failed: no Category with id: {id} was found.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Category Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var category = _mapper.Map<Category>(categoryDTO);
                var isSuccess = await _categoryRepository.Update(category);
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
        /// Deletes a Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {

            try
            {
                _logger.LogInfo($"Category Delete attempted - id: {id}");
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    _logger.LogWarn($"Category Delete failed: no  id was provided.");
                    return BadRequest();
                }
                var isExists = await _categoryRepository.IsExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"Category Update failed: no Category with id: {id} was found.");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Category Data was Invalid.");
                    return BadRequest(ModelState);
                }
                var category = await _categoryRepository.FindById(id);
                var isSuccess = await _categoryRepository.Delete(category);
                if (!isSuccess)
                {
                    return InternalError($"Delete Category Operation failed.");
                }

                _logger.LogWarn($"Category with id: {id} successfully deleted.");
                return NoContent();
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        private string GetControllerNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var description = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller} - {description}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator.");
        }
    }

}
