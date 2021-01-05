using Microsoft.AspNetCore.Http;
using Company_API.Contracts;
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
        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
    }
}
