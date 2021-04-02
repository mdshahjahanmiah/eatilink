using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eatigo.Eatilink.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        IMongoCollection<T> Get();
        T Get(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}
