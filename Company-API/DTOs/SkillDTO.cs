
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class SkillDTO
    {
        public string Question { get; set; }
        public string Description { get; set; }
        public Guid IdSkill { get; set; }
        public Guid? IdCategory { get; set; }

        public virtual CategoryDTO IdCategoryNavigation { get; set; }
        public virtual ICollection<EmployeeSkillDTO> EmployeeSkills { get; set; }
    }
    public class SkillCreateDTO
    {
        public string Question { get; set; }
        public string Description { get; set; }
        public Guid? IdCategory { get; set; }
    }
    public class SkillUpdateDTO
    {
        public string Question { get; set; }
        public string Description { get; set; }
        public Guid IdSkill { get; set; }
        public Guid? IdCategory { get; set; }
    }
}
