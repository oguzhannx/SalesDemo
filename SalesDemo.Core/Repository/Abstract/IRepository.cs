using SalesDemo.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesDemo.Core.Repository.Abstract
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Result<ICollection<TEntity>>GetAll();
        Task<Result<ICollection<TEntity>>> GetAllAsync();
        Result<ICollection<TEntity>>FilterBy(Expression<Func<TEntity, bool>> filter);
        Task<Result<ICollection<TEntity>>> FilterByAsync(Expression<Func<TEntity, bool>> filter);
        Result<TEntity> GetById(string id, string type = "object");
        Task<Result<TEntity>> GetByIdAsync(string id, string type = "object");
        Result<TEntity> InsertOne(TEntity entity);
        Task<Result<TEntity>> InsertOneAsync(TEntity entity);
        Result<ICollection<TEntity>>InsertMany(ICollection<TEntity> entities);
        Task<Result<ICollection<TEntity>>> InsertManyAsync(ICollection<TEntity> entities);
        Result<TEntity> ReplaceOne(TEntity entity, string id, string type = "object");
        Task<Result<TEntity>> ReplaceOneAsync(TEntity entity, string id, string type = "object");
        Result<TEntity> DeleteOne(Expression<Func<TEntity, bool>> filter);
        Task<Result<TEntity>> DeleteOneAsync(Expression<Func<TEntity, bool>> filter);
        Result<TEntity> DeleteById(string id);
        Task<Result<TEntity>> DeleteByIdAsync(string id);
        void DeleteMany(Expression<Func<TEntity, bool>> filter);
        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter);

    }
}
