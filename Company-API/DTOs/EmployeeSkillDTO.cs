
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class EmployeeSkillDTO
    {
        public Guid IdEmployee { get; set; }
        public Guid IdSkill { get; set; }
        public string Answer { get; set; }

        public virtual EmployeeDTO IdEmployeeNavigation { get; set; }
        public virtual SkillDTO IdSkillNavigation { get; set; }
    }
    public class EmployeeSkillCreateDTO
    {
        public Guid IdEmployee { get; set; }
        public Guid IdSkill { get; set; }
        public string Answer { get; set; }
    }
    public class EmployeeSkillUpdateDTO
    {
        public Guid IdEmployee { get; set; }
        public Guid IdSkill { get; set; }
        public string Answer { get; set; }
    }
}
