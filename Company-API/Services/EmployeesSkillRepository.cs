using Company_API.Contracts;
using Company_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Services
{
    public class EmployeesSkillRepository : IEmployeesSkillRepository
    {
       
        private readonly ApplicationDbContext _db;
        public EmployeesSkillRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IList<EmployeesSkill>> FindAll()
        {
            return await _db.EmployeesSkills.ToListAsync();
        }

        public async Task<EmployeesSkill> FindById(Guid id)
        {
            return await _db.EmployeesSkills.FindAsync(id);
        }

        public async Task<bool> Create(EmployeesSkill entity)
        {
            await _db.EmployeesSkills.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(EmployeesSkill entity)
        {
            _db.EmployeesSkills.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(EmployeesSkill entity)
        {
            _db.EmployeesSkills.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }
    }
}