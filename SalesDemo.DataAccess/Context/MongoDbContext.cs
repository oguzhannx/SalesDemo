using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SalesDemo.Core.DbSettingModels;


namespace SalesDemo.DataAccess.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name.Trim());
        }
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
