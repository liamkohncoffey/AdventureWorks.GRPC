using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ShipWrecks.GRPC.Domain.Repositories
{
    public interface IQueryBuilder<TEntity, TQueryBuilder>
        where TEntity : class
    {
        TQueryBuilder OrderBy(Expression<Func<TEntity, object>> prop);

        TQueryBuilder OrderByDescending(Expression<Func<TEntity,object>> prop);

        TQueryBuilder Take(int count);

        TQueryBuilder Skip(int count);
        
        TQueryBuilder Where(Expression<Func<TEntity, bool>> expr);

        Task<TEntity> ToEntity(CancellationToken cancellationToken = default(CancellationToken));

        Task<QueryResult<TEntity>> ToEntities(CancellationToken cancellationToken = default(CancellationToken));
        
        Task<bool> Any(CancellationToken cancellationToken = default(CancellationToken));

        TQueryBuilder New();
    }
}