using Company_API.Contracts;
using Company_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Services
{
    public class EmployeeSkillRepository : IEmployeeSkillRepository
    {
       
        private readonly ApplicationDbContext _db;
        public EmployeeSkillRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IList<EmployeeSkill>> FindAll()
        {
            return await _db.EmployeeSkills.ToListAsync();
        }

        public async Task<EmployeeSkill> FindById(Guid id)
        {
            return await _db.EmployeeSkills.FindAsync(id);
        }

        public async Task<bool> Create(EmployeeSkill entity)
        {
            await _db.EmployeeSkills.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(EmployeeSkill entity)
        {
            _db.EmployeeSkills.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(EmployeeSkill entity)
        {
            _db.EmployeeSkills.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> IsExists(Guid IdEmployee, Guid IdSkill )
        {
            return await _db.EmployeeSkills.AnyAsync(q => q.IdEmployee == IdEmployee && q.IdSkill == IdSkill);
        }
        /// <summary>
        /// Obsolete function since 2 Guids are necessary. Use overloaded version with 2 parameters instead.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null</returns>
        public  Task<bool> IsExists(Guid id)
        {
            return null;
        }
    }
}