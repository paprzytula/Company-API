using System;
using System.Collections.Generic;



namespace Company_API.Data
{
    public partial class EmployeeSkill
    {
        public Guid IdEmployee { get; set; }
        public Employee Employee { get; set; }
        public Guid IdSkill { get; set; }
        public Skill Skill { get; set; }
    }
}
