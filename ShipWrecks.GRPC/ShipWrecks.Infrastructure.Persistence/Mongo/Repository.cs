using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using ShipWrecks.GRPC.Domain.Model;
using Subscriptions.Infrastructure.Persistence;

namespace ShipWrecks.Infrastructure.Persistence.Mongo
{
    public abstract class Repository<T> where T : Entity
    {
        private readonly IDatabaseProvider _databaseProvider;

        private const int RetryCount = 3;

        protected Repository(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        protected Task<T> GetEntity(string id, CancellationToken cancellationToken = default)
        {
            return GetCollection().Find(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
        }

        protected Task AddEntity(T entity, CancellationToken cancellationToken)
        {
            return GetCollection().InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
        }

        protected async Task<T> UpdateEntity(string id, Action<T> entityUpdateFunc, CancellationToken cancellationToken)
        {
            return await RetryHelper.Retry(RetryCount, async () =>
            {
                var entity = await GetEntity(id, cancellationToken);
               
                entityUpdateFunc(entity);
               
                var updateResult = await GetCollection().ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions
                {
                    IsUpsert = false
                }, cancellationToken);

               return new RetryResult<T>()
               {
                   Result = entity,
                   Success = updateResult.ModifiedCount > 0
               };
            });
        }

        protected Task DeleteEntity(string id, CancellationToken cancellationToken)
        {
            return GetCollection().DeleteOneAsync(x => x.Id == id, cancellationToken);
        }

        protected Task<bool> EntityExists(string id, CancellationToken cancellationToken)
        {
            return GetCollection().Find(x => x.Id == id).AnyAsync(cancellationToken);
        }

        protected IMongoCollection<T> GetCollection()
        {
            return _databaseProvider.GetDatabase().GetCollection<T>(GetMongoCollectionName());
        }
        
        protected IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName)
        {
            return _databaseProvider.GetDatabase().GetCollection<TEntity>(collectionName);
        }

        protected abstract string GetMongoCollectionName();
    }
}