using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class EmployeeDTO
    {
        public Guid IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string Picture { get; set; }
        public Guid? IdDepartment { get; set; }

        public virtual DepartmentDTO IdDepartmentNavigation { get; set; }
        public virtual ICollection<EmployeeSkillDTO> EmployeeSkills { get; set; }
    }
    public class EmployeeCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string Picture { get; set; }
    }
    public class EmployeeUpdateDTO
    {
        public Guid IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string Picture { get; set; }
        public Guid? IdDepartment { get; set; }
    }
}
