using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Data.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.IdSkill);
            builder.Property(s => s.Description).IsRequired();
            builder.Property(s => s.Answer);
            builder.Property(s => s.Question).IsRequired();
            builder
                .HasOne(s => s.Category)
                .WithMany(s => s.Skills)
                .HasForeignKey(s => s.IdCategory);
        }
    }
}
