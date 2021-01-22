using System.Collections.Generic;

namespace ShipWrecks.GRPC.Domain.Repositories
{
    public class QueryResult<TEntity>
    {
        public long Count { get; set; }

        public IEnumerable<TEntity> Items { get; set; } = new TEntity[0];
    }
}