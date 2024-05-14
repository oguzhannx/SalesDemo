using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SalesDemo.Core.DbSettingModels;
using SalesDemo.Core.Models.Concrete;
using SalesDemo.Core.Repository.Abstract;
using SalesDemo.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesDemo.DataAccess.Repository
{
    public class MongoRepositoryBase<T> : IRepository<T> where T : class, new()
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<T> _collection;

        public MongoRepositoryBase(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<T>();
        }


        public Result<ICollection<T>> GetAll()
        {
            var result = new Result<ICollection<T>>();
            try
            {
                var data = _collection.AsQueryable().ToList();
                if (data != null)
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message = $"AsQueryable {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }
        public async Task<Result<ICollection<T>>> GetAllAsync()
        {
            var result = new Result<ICollection<T>>();
            try
            {
                var data = await _collection.AsQueryable().ToListAsync();
                if (data != null)
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message = $"AsQueryable {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public Result<T> DeleteById(string id)
        {
            var result = new Result<T>();
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var data = _collection.FindOneAndDelete(filter);
                if (data != null)
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message = $"DeleteById {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<Result<T>> DeleteByIdAsync(string id)
        {
            var result = new Result<T>();
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var data = await _collection.FindOneAndDeleteAsync(filter);
                if (data != null)
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message = $"DeleteById {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }
        public void DeleteMany(Expression<Func<T, bool>> filter)
        {
            _collection.DeleteMany(filter);
        }
        public async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            await _collection.DeleteManyAsync(filter);
        }
        public Result<T> DeleteOne(Expression<Func<T, bool>> filter)
        {
            var result = new Result<T>();
            try
            {
                var deleteDocument = _collection.FindOneAndDelete(filter);
                result.Data = deleteDocument;
            }
            catch (Exception ex)
            {
                result.Message = $"DeleteOne {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }
        public async Task<Result<T>> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            var result = new Result<T>();
            try
            {
                var deleteDocument = await _collection.FindOneAndDeleteAsync(filter);
                result.Data = deleteDocument;
            }
            catch (Exception ex)
            {
                result.Message = $"DeleteOneAsync {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public Result<ICollection<T>> FilterBy(Expression<Func<T, bool>> filter)
        {
            var result = new Result<ICollection<T>>();
            try
            {
                var data = _collection.Find(filter).ToList();
                if (data != null)
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<Result<ICollection<T>>> FilterByAsync(Expression<Func<T, bool>> filter)
        {
            var result = new Result<ICollection<T>>();
            try
            {
                var data = await _collection.Find(filter).ToListAsync();
                if (data != null)
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public Result<T> GetById(string id, string type = "object")
        {
            var result = new Result<T>();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var data = _collection.Find(filter).FirstOrDefault();
                if (data != null)
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message = $"GetById {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<Result<T>> GetByIdAsync(string id, string type = "object")
        {
            var result = new Result<T>();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var data = await _collection.Find(filter).FirstOrDefaultAsync();
                if (data != null)
                    result.Data = data;
            }
            catch (Exception ex)
            {
                result.Message = $"GetById {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public Result<ICollection<T>> InsertMany(ICollection<T> entities)
        {
            var result = new Result<ICollection<T>>();
            try
            {
                _collection.InsertMany(entities);
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Message = $"InsertMany {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<Result<ICollection<T>>> InsertManyAsync(ICollection<T> entities)
        {
            var result = new Result<ICollection<T>>();
            try
            {
                await _collection.InsertManyAsync(entities);
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Message = $"InsertManyAsync {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public Result<T> InsertOne(T entity)
        {
            var result = new Result<T>();
            try
            {
                _collection.InsertOne(entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"InsertOne {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<Result<T>> InsertOneAsync(T entity)
        {
            var result = new Result<T>();
            try
            {
                await _collection.InsertOneAsync(entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"InsertOneAsync {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public Result<T> ReplaceOne(T entity, string id, string type = "object")
        {
            var result = new Result<T>();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var updatedDocument = _collection.ReplaceOne(filter, entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"GetById {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }

        public async Task<Result<T>> ReplaceOneAsync(T entity, string id, string type = "object")
        {
            var result = new Result<T>();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var updatedDocument = await _collection.ReplaceOneAsync(filter, entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"GetById {ex.Message}";
                //result.Success = false;
                result.Data = null;
            }
            return result;
        }
    }
}
