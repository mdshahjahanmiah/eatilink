using Eatigo.Eatilink.DataObjects.Settings;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eatigo.Eatilink.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoClient _mongoClient;
        private readonly AppSettings _appSettings;
        private readonly IMongoDatabase _database;
        public Repository(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _mongoClient = new MongoClient(_appSettings.DatabaseSettings.ConnectionString);
            _database = _mongoClient.GetDatabase(_appSettings.DatabaseSettings.DatabaseName);
        }

        public async Task Add(T entity)
        {
            var collection = _database.GetCollection<T>(_appSettings.DatabaseSettings.CollectionName);
            await  collection.InsertOneAsync(entity);
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<T> Get()
        {
            var result = _database.GetCollection<T>(_appSettings.DatabaseSettings.CollectionName);
            return result;
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            var result= _database.GetCollection<T>(_appSettings.DatabaseSettings.CollectionName).Find(predicate).FirstOrDefault();
            return result;
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
