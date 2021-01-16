using Company_API.Contracts;
using Company_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Services
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _db;
        public SkillRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Skill>> FindAll()
        {
            return await _db.Skills.ToListAsync();
        }

        public async Task<Skill> FindById(int id)
        {
            return await _db.Skills.FindAsync(id);
        }

        public async Task<bool> Create(Skill entity)
        {
            await _db.Skills.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Skill entity)
        {
            _db.Skills.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(Skill entity)
        {
            _db.Skills.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _db.Skills.AnyAsync(q => q.Id == id);
        }

    }
}
