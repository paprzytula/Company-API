using System;
using System.Collections.Generic;



namespace Company_API.Data
{
    public partial class Department
    {
        public Guid IdDepartment { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
