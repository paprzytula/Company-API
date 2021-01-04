using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Company_API.Data
{
    [Table("Employees")]
    public partial class Employee
    {
        public Employee()
        {
            EmployeesSkills = new HashSet<EmployeesSkill>();
        }

        public Guid IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string Picture { get; set; }
        public Guid? IdDepartment { get; set; }

        public virtual Department IdDepartmentNavigation { get; set; }
        public virtual ICollection<EmployeesSkill> EmployeesSkills { get; set; }
    }
}
