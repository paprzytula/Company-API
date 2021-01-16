using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
    }
    public class CategoryUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
