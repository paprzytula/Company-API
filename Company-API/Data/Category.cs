using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_API.Data
{
    [Table("Categories")]
    public partial class Category
    {
        public Category()
        {
            Skills = new HashSet<Skill>();
        }

        public Guid IdCategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
