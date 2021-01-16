using System;
using System.Collections.Generic;



namespace Company_API.Data
{
    public partial class Skill
    {

        public int Id { get; set; }
        public Employee Employee { get; set; }
        public Category Category { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public ICollection<Employee> Employees { get; set; }


    }
}
