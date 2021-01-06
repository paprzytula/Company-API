using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder 
                .HasKey(e => e.IdEmployee);
            builder
                .HasOne(e => e.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.IdDepartment);
            builder 
                .Property(e => e.DateOfBirth)
                .IsRequired();
            builder
                .Property(e => e.DateOfEmployment)
                .IsRequired();
            builder
                .Property(e => e.FirstName)
                .IsRequired();
            builder
                .Property(e => e.LastName)
                .IsRequired();
            builder
                .Property(e => e.Picture);
                
        }
    }
}
