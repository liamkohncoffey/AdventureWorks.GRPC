using MongoDB.Driver;
using ShipWrecks.Infrastructure.Persistence.Configuration;

namespace ShipWrecks.Infrastructure.Persistence.Mongo
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly MongoDbConfiguration _configuration;
        
        private IMongoClient MongoClient => new MongoClient(_configuration.ConnectionString);
        
        public DatabaseProvider(MongoDbConfiguration configuration)
        {;
            _configuration = configuration;
        }

        public IMongoDatabase GetDatabase()
        {
            return MongoClient.GetDatabase(_configuration.DatabaseName);
        }
    }
}