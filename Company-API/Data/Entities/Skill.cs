using System;
using System.Collections.Generic;



namespace Company_API.Data
{
    public partial class Skill
    {

        public Guid IdSkill { get; set; }
        public Guid IdEmployee { get; set; }
        public Guid IdCategory { get; set; }
        public Category Category { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public virtual ICollection<EmployeeSkill> Employees { get; set; }
    }
}
