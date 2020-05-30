using System.Collections.Generic;
using System.Threading.Tasks;
using Autorest.Domain.Infrastructure;
using AutoRest.Models;

namespace Autorest.Domain.Repository
{
    public class AutoRestrepository<T>: IAutoRestRepository<T>
    {
        private readonly IGenericRepository<T> _genericRepositoryImplementation;
        private readonly IRepositoryObservable<T> _observable;

        public AutoRestrepository(IGenericRepository<T> repository, IRepositoryObservable<T> observable)
        {
            _genericRepositoryImplementation = repository;
            _observable = observable;
        }

        public T Add(T model)
        {
            return this.AddAsync(model).GetAwaiter().GetResult();
        }

        public async Task<T> AddAsync(T model)
        {
            var res = await _genericRepositoryImplementation.AddAsync(model);
            _observable.Add(res);
            return res;
        }

        public void Delete(string id)
        {
            this.DeleteAsync(id).GetAwaiter().GetResult();
        }

        public async Task DeleteAsync(string id)
        {
            var res = await GetAsync(id);
            await _genericRepositoryImplementation.DeleteAsync(id);
            _observable.Delete(res);
        }

        public T Update(string id, Dictionary<string, object> updateDefinition)
        {
            return UpdateAsync(id, updateDefinition).GetAwaiter().GetResult();
        }

        public async Task<T> UpdateAsync(string id, Dictionary<string, object> updateDefinition)
        {
            var res = await _genericRepositoryImplementation.UpdateAsync(id, updateDefinition);
            _observable.Edit(res);
            return res;
        }

        public T Replace(T model)
        {
            return this.ReplaceAsync(model).GetAwaiter().GetResult();
        }

        public async Task<T> ReplaceAsync(T model)
        {
            var res = await _genericRepositoryImplementation.ReplaceAsync(model);
            _observable.Edit(res);
            return res;
        }

        public T Get(string id)
        {
            return GetAsync(id).GetAwaiter().GetResult();
        }

        public async Task<T> GetAsync(string id)
        {
            return await _genericRepositoryImplementation.GetAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return GetAllAsync().GetAwaiter().GetResult();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _genericRepositoryImplementation.GetAllAsync();
        }
    }
}
