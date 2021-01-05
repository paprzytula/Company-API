using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company_API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeesSkill> EmployeesSkills { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.Property(e => e.IdCategory).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.IdDepartment);

                entity.Property(e => e.IdDepartment).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PK_Employee1");

                entity.Property(e => e.IdEmployee).ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DateOfEmployment).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Picture).HasMaxLength(150);

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdDepartment)
                    .HasConstraintName("FK_Employees_Employees");
            });

            modelBuilder.Entity<EmployeesSkill>(entity =>
            {
                entity.HasKey(e => new { e.IdEmloyee, e.IdSkill });

                entity.HasOne(d => d.IdEmloyeeNavigation)
                    .WithMany(p => p.EmployeesSkills)
                    .HasForeignKey(d => d.IdEmloyee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeesSkills_Employees");

                entity.HasOne(d => d.IdSkillNavigation)
                    .WithMany(p => p.EmployeesSkills)
                    .HasForeignKey(d => d.IdSkill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeesSkills_Skills");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.IdSkill)
                    .HasName("PK_Skill_1");

                entity.Property(e => e.IdSkill).ValueGeneratedNever();

                entity.Property(e => e.Question).HasMaxLength(100);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK_Skills_Categories");
            });








        }
    }
}
