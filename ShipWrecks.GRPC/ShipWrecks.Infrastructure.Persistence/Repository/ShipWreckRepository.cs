using System.Threading;
using System.Threading.Tasks;
using ShipWrecks.GRPC.Domain.Model;
using ShipWrecks.GRPC.Domain.Repositories;
using ShipWrecks.Infrastructure.Persistence.Mongo;

namespace ShipWrecks.Infrastructure.Persistence.Repository
{
    public class ShipWreckRepository : Repository<ShipWreckEntity>, IShipWreckRepository
    {
        private readonly IShipWreckQueryBuilder _queryBuilder;
        
        public ShipWreckRepository(IDatabaseProvider databaseProvider, IShipWreckQueryBuilder queryBuilder) : base(databaseProvider)
        {
            _queryBuilder = queryBuilder;
        }
        
        public Task Add(ShipWreckEntity paymentHistoryEntry, CancellationToken cancellationToken = default)
        {
            return AddEntity(paymentHistoryEntry, cancellationToken);
        }

        public IShipWreckQueryBuilder Query()
        {
            return _queryBuilder.New();
        }

        protected override string GetMongoCollectionName()
        {
            return "shipwrecks";
        }
    }
}