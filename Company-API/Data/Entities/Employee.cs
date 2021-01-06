using Company_API.Data.Entities;
using System;
using System.Collections.Generic;



namespace Company_API.Data
{
    public partial class Employee
    {
        public Guid IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string Picture { get; set; }
        public Department Department { get; set; }
        public Guid IdDepartment { get; set; }
        public ICollection<EmployeeSkill> Skills { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
