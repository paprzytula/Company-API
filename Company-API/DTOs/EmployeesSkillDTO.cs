
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class EmployeesSkillDTO
    {
        public Guid IdEmloyee { get; set; }
        public Guid IdSkill { get; set; }
        public string Answer { get; set; }

        public virtual EmployeeDTO IdEmloyeeNavigation { get; set; }
        public virtual SkillDTO IdSkillNavigation { get; set; }
    }
}
