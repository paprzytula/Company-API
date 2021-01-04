using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Company_API.Data
{
    [Table("Skills")]
    public partial class Skill
    {
        public Skill()
        {
            EmployeesSkills = new HashSet<EmployeesSkill>();
        }

        public string Question { get; set; }
        public string Description { get; set; }
        public Guid IdSkill { get; set; }
        public Guid? IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<EmployeesSkill> EmployeesSkills { get; set; }
    }
}
