
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class SkillDTO
    {
        public string Question { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public CategoryDTO CategoryDTO { get; set; }

    }
    public class SkillCreateDTO
    {
        public string Question { get; set; }
        public string Description { get; set; }
        public CategoryDTO CategoryDTO { get; set; }

    }
    public class SkillUpdateDTO
    {
        public string Question { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public CategoryDTO CategoryDTO { get; set; }

    }
}
