using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.DTOs
{
    public class CategoryDTO
    {
        public Guid IdCategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SkillDTO> Skills { get; set; }
    }
}
