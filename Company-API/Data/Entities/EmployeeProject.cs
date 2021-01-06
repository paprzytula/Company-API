using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Data.Entities
{
    public class EmployeeProject
    {
        public Guid IdProject { get; set; }
        public Guid IdEmployee { get; set; }
        public virtual Project Project { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
