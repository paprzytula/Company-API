using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class EmployeeDTO : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string Picture { get; set; }


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
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string Picture { get; set; }
    }
}
