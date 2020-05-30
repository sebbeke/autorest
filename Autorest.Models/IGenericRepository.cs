using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoRest.Models
{
    public interface IGenericRepository<T>
    {
        T Add(T model);
        Task<T> AddAsync(T model);
        void Delete(string id);
        Task DeleteAsync(string id);
        T Update(string id, Dictionary<string, object> updateDefinition);
        Task<T> UpdateAsync(string id, Dictionary<string, object> updateDefinition);
        T Replace(T model);
        Task<T> ReplaceAsync(T model);
        T Get(string id);
        Task<T> GetAsync(string id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
    }
}