using Company_API.Contracts;
using Company_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Services
{ 
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Category>> FindAll()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> FindById(Guid id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<bool> Create(Category entity)
        {
            await _db.Categories.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Category entity)
        {
            _db.Categories.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(Category entity)
        {
            _db.Categories.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }
    }
}
