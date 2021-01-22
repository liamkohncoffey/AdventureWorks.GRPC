using MongoDB.Driver;

namespace ShipWrecks.Infrastructure.Persistence.Mongo
{
    public interface IDatabaseProvider
    {
        IMongoDatabase GetDatabase();
    }
}