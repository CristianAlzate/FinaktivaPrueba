using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finaktiva.Repository.Repository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
