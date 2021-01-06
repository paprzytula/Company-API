using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Company_API.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {        
        Task<IList<T>> FindAll();
        Task<T> FindById(Guid id);
        Task<bool> IsExists(Guid id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
