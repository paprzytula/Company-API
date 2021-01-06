using Company_API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Data.Configurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder
                .HasKey(ep => new { ep.IdEmployee, ep.IdProject });
            builder
                    .HasOne(ep => ep.Employee)
                    .WithMany(ep => ep.EmployeeProjects)
                    .HasForeignKey(ep=>ep.IdEmployee);

            builder
                .HasOne(ep => ep.Project)
                .WithMany(ep => ep.EmployeeProjects)
                .HasForeignKey(ep => ep.IdProject);

        }
    }
}
