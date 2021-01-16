using System;
using System.Collections.Generic;

namespace Company_API.Data
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
