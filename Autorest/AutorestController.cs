using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autorest.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Autorest
{
    public class AutoRestController<T> : ControllerBase where T : class
    {
        private readonly IAutoRestRepository<T> _repository;


        public AutoRestController(IAutoRestRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<T>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<T> Get(string id)
        {
            return await _repository.GetAsync(id);
        }

        [HttpPost]
        public async Task<T> Post([FromBody]T value)
        {
            if (ModelState.IsValid)
                return await _repository.AddAsync(value);
            return null;
        }

        [HttpPut]
        public async Task<T> Put([FromBody]T value)
        {
            if (ModelState.IsValid)
                return await _repository.ReplaceAsync(value);
            return null;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
