using Company_API.Contracts;
using Company_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
               private readonly ApplicationDbContext _db;
        public DepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Department>> FindAll()
        {
            return await _db.Departments.ToListAsync();
        }

        public async Task<Department> FindById(int id)
        {
            return await _db.Departments.FindAsync(id);
        }

        public async Task<bool> Create(Department entity)
        {
            await _db.Departments.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Department entity)
        {
            _db.Departments.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(Department entity)
        {
            _db.Departments.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _db.Departments.AnyAsync(q => q.Id == id);
        }
    }
}