using Microsoft.AspNetCore.Http;
using Company_API.Contracts;
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
        private readonly IEmployeesSkillRepository _employeesSkillRepository;
        public EmployeesSkillsController(IEmployeesSkillRepository employeesSkillRepository)
        {
            _employeesSkillRepository = employeesSkillRepository;
        }
    }
}
