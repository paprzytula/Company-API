using Company_API.Contracts;
using Company_API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

		public async Task<Employee> FindById(string id)
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

		public async Task<bool> IsExists(string id)
		{
			return await _db.Employees.AnyAsync(q => q.Id == id);
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