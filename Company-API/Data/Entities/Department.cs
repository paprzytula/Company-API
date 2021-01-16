using System;
using System.Collections.Generic;



namespace Company_API.Data
{
    public partial class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
