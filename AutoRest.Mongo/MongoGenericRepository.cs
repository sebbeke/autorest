using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRest.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AutoRest.Mongo
{
    public class MongoGenericRepository<T> : IGenericRepository<T>
    {
            public IMongoCollection<T> Collection { get; set; }

            public MongoGenericRepository(MongoConfiguration configuration)
            {

                Collection = configuration.GetDatabase().GetCollection<T>(typeof(T).Name.Replace("Mongo", ""));
            }

            public virtual T Add(T model)
            {
                return AddAsync(model).GetAwaiter().GetResult();
            }

            public virtual async Task<T> AddAsync(T model)
            {
                try
                {
                    var type = model.GetType().GetProperty("Id");
                    if (type != null)
                    {
                        type.SetValue(model, ObjectId.GenerateNewId().ToString());
                    }
                    await Collection.InsertOneAsync(model);
                    return model;
                }
                catch (Exception e)
                {
                    return default;
                }
            }

            public virtual void Delete(string id)
            {
                DeleteAsync(id).GetAwaiter();
            }

            public virtual async Task DeleteAsync(string id)
            {
                try
                {
                    var filter = Builders<T>.Filter;
                    await Collection.DeleteOneAsync(filter.Eq("Id", new ObjectId(id)));
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            public virtual T Update(string id, Dictionary<string, object> updateDefinition)
            {
                return UpdateAsync(id, updateDefinition).GetAwaiter().GetResult();
            }

            public virtual async Task<T> UpdateAsync(string id, Dictionary<string, object> updateDefinition)
            {
                try
                {
                    var updateBuilder = Builders<T>.Update;
                    var definition = updateDefinition.Aggregate<KeyValuePair<string, object>, UpdateDefinition<T>>(null, (current, item) => current == null ? updateBuilder.Set(item.Key, item.Value) : current.Set(item.Key, item.Value));
                    var filter = Builders<T>.Filter;
                    await Collection.UpdateOneAsync(filter.Eq("Id", new ObjectId(id)), definition);
                    return await GetAsync(id);
                }
                catch
                {
                    return default(T);
                }
            }

            public T Replace(T model)
            {
                return ReplaceAsync(model).GetAwaiter().GetResult();
            }

            public async Task<T> ReplaceAsync(T model)
            {
                var id = model.GetType().GetProperty("Id")?.GetValue(model).ToString();
                var filter = Builders<T>.Filter.Eq("Id", new ObjectId(id));
                await Collection.ReplaceOneAsync(filter, model);
                return model;
            }

            public virtual T Get(string id)
            {
                return GetAsync(id).GetAwaiter().GetResult();
            }

            public async Task<T> GetAsync(string id)
            {
                var filter = Builders<T>.Filter;
                var collection = await Collection.FindAsync(filter.Eq("Id", new ObjectId(id)));
                return collection.First();
            }

            public virtual IEnumerable<T> GetAll()
            {
                return this.GetAllAsync().GetAwaiter().GetResult();
            }

            public virtual async Task<IEnumerable<T>> GetAllAsync()
            {
                var collection = await Collection.FindAsync(new BsonDocument());
                return collection.ToEnumerable();
            }
        }
}