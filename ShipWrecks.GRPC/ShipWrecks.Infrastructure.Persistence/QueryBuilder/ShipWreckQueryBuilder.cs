using ShipWrecks.GRPC.Domain.Model;
using ShipWrecks.GRPC.Domain.Repositories;
using ShipWrecks.Infrastructure.Persistence.Mongo;

namespace ShipWrecks.Infrastructure.Persistence.QueryBuilder
{
    public class ShipWreckQueryBuilder : QueryBuilder<ShipWreckEntity, IShipWreckQueryBuilder>, IShipWreckQueryBuilder
    {
        public ShipWreckQueryBuilder(IDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }

        protected override string GetMongoCollectionName()
        {
            return "shipwrecks";
        }
    }
}