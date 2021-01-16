
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeDTO> Employees { get; set; }
    }
    public class DepartmentCreateDTO
    {
        public string Name { get; set; }
    }
        public class DepartmentUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
