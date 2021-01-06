using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Data.Configurations
{
    public class EmployeeSkillConfiguration : IEntityTypeConfiguration<EmployeeSkill>
    {
        public void Configure(EntityTypeBuilder<EmployeeSkill> builder)
        {
            builder
                        .HasKey(es => new { es.IdEmployee, es.IdSkill });
            builder
                .HasOne(es => es.Employee)
                .WithMany(es => es.Skills)
                .HasForeignKey(es => es.IdEmployee);
            builder
                .HasOne(es => es.Skill)
                .WithMany(es => es.Employees)
                .HasForeignKey(es => es.IdSkill);
        }
    }
}
