using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Company_API.Data
{
    [Table("EmployeesSkills")]
    public partial class EmployeesSkill
    {
        public Guid IdEmloyee { get; set; }
        public Guid IdSkill { get; set; }
        public string Answer { get; set; }

        public virtual Employee IdEmloyeeNavigation { get; set; }
        public virtual Skill IdSkillNavigation { get; set; }
    }
}
