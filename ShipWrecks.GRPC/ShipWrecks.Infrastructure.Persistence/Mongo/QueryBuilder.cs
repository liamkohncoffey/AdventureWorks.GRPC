using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using ShipWrecks.GRPC.Domain.Repositories;

namespace ShipWrecks.Infrastructure.Persistence.Mongo
{
    public abstract class QueryBuilder<TEntity, TQueryBuilder>
        : IQueryBuilder<TEntity, TQueryBuilder>
        where TEntity : class
        where TQueryBuilder : class
    {
        private readonly IDatabaseProvider _databaseProvider;
        private int? _skip;
        private int? _limit;

        protected QueryBuilder(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
            
            Query = Builders<TEntity>.Filter.Empty;
            
            Sort = null;
        }

        protected FilterDefinition<TEntity> Query { get; set; }
        
        protected SortDefinition<TEntity> Sort { get; set; }

        public TQueryBuilder New()
        {
            Query = Builders<TEntity>.Filter.Empty;
            _skip = null;
            _limit = null;
            Sort = null;
            
            return this as TQueryBuilder;
        }

        public TQueryBuilder Where(Expression<Func<TEntity, bool>> expr)
        {
            if (expr == null)
            {
                return this as TQueryBuilder;
            }
            
            Query = Builders<TEntity>.Filter.And(Query, Builders<TEntity>.Filter.Where(expr));
            
            return this as TQueryBuilder;
        }

        public TQueryBuilder OrderBy(Expression<Func<TEntity, object>> prop)
        {
            if (prop == null)
            {
                return this as TQueryBuilder;
            }

            var sortDefinition = Builders<TEntity>.Sort.Ascending(prop);

            AppendSortDefinition(sortDefinition);
            
            return this as TQueryBuilder;
        }

        public TQueryBuilder OrderByDescending(Expression<Func<TEntity,object>> prop)
        {
            if (prop == null)
            {
                return this as TQueryBuilder;
            }

            var sortDefinition = Builders<TEntity>.Sort.Descending(prop);

            AppendSortDefinition(sortDefinition);

            return this as TQueryBuilder;
        }

        private void AppendSortDefinition(SortDefinition<TEntity> sortDefinition)
        {
            if (Sort != null)
            {
                Sort = Builders<TEntity>.Sort.Combine(Sort, sortDefinition);
            }
            else
            {
                Sort = sortDefinition;
            }
        }

        public TQueryBuilder Take(int count)
        {
            _limit = count;
            
            return this as TQueryBuilder;
        }

        public TQueryBuilder Skip(int count)
        {
            _skip = count;
            
            return this as TQueryBuilder;
        }

        public Task<TEntity> ToEntity(CancellationToken cancellationToken)
        {
            var finalQuery = GetCollection().Find(Query);

            finalQuery = ApplySort(finalQuery);
            
            return finalQuery.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<QueryResult<TEntity>> ToEntities(CancellationToken cancellationToken = default(CancellationToken))
        {
            var finalQuery = GetCollection().Find(Query);
            
            var getCountTask = finalQuery.CountDocumentsAsync(cancellationToken);

            finalQuery = ApplySort(finalQuery);
            finalQuery = ApplySkip(finalQuery);
            finalQuery = ApplyLimit(finalQuery);

            var getEntitiesTask = finalQuery.ToListAsync(cancellationToken);

            await Task.WhenAll(getCountTask, getEntitiesTask);

            return new QueryResult<TEntity>
            {
                Count = getCountTask.Result,
                Items = getEntitiesTask.Result
            };
        }

        public Task<bool> Any(CancellationToken cancellationToken = default(CancellationToken))
        {
            var finalQuery = GetCollection().Find(Query);

            return finalQuery.AnyAsync(cancellationToken);
        }

        private IFindFluent<TEntity, TEntity> ApplyLimit(IFindFluent<TEntity, TEntity> finalQuery)
        {
            if (_limit.HasValue)
            {
                finalQuery = finalQuery.Limit(_limit.Value);
            }

            return finalQuery;
        }

        private IFindFluent<TEntity, TEntity> ApplySkip(IFindFluent<TEntity, TEntity> finalQuery)
        {
            if (_skip.HasValue)
            {
                finalQuery = finalQuery.Skip(_skip.Value);
            }

            return finalQuery;
        }

        private IFindFluent<TEntity, TEntity> ApplySort(IFindFluent<TEntity, TEntity> finalQuery)
        {
            if (Sort != null)
            {
                finalQuery = finalQuery.Sort(Sort);
            }

            return finalQuery;
        }

        protected IMongoCollection<TEntity> GetCollection()
        {
            return _databaseProvider.GetDatabase().GetCollection<TEntity>(GetMongoCollectionName());
        }

        protected abstract string GetMongoCollectionName();
    }
}