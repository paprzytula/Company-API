﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Data.Entities
{
    public partial class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  ICollection<Employee> Employees { get; set; }
    }
}
