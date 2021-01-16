using Company_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_API.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Task<Employee> FindById(string id);
        Task<bool> IsExists(string id);
    }
}
