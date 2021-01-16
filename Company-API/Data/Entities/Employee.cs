using Company_API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;



namespace Company_API.Data
{
    public partial class Employee : IdentityUser
    {
        public Employee()
        {
            this.Projects = new HashSet<Project>();
            this.Skills = new HashSet<Skill>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string Picture { get; set; }
        public Department Department { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public  ICollection<Project> Projects { get; set; }
    }
}
