using Company_API.Contracts;
using Company_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Employee>> FindAll()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task<Employee> FindById(Guid id)
        {
            return await _db.Employees.FindAsync(id);
        }

        public async Task<bool> Create(Employee entity)
        {
            await _db.Employees.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Employee entity)
        {
            _db.Employees.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(Employee entity)
        {
            _db.Employees.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }
    }
}