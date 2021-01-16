using Company_API.Contracts;
using System.Security.Claims;
using Company_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Company_API.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<Employee> userManager;
        private readonly SignInManager<Employee> signInManager;
        private readonly RoleManager<Employee> roleManager;
        public EmployeeRepository(ApplicationDbContext db, UserManager<Employee> userManager, SignInManager<Employee> signInManager, RoleManager<Employee> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.userManager = userManager;
        }
       


        public async Task<IList<Employee>> FindAll()
        {
            return await db.Employees.ToListAsync();
        }

        public async Task<Employee> FindById(string id)
        {
            return await db.Employees.FindAsync(id);
        }

        public async Task<bool> Create(Employee entity)
        {
            await db.Employees.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Employee entity)
        {
            db.Employees.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(Employee entity)
        {
            db.Employees.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> IsExists(string id)
        {
            return await db.Employees.AnyAsync(q => q.Id == id);
        }

        public Task<Employee> FindById(int id)
        {
            return null;
        }

        public Task<bool> IsExists(int id)
        {
            return null;
        }
    }
}