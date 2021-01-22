using ShipWrecks.GRPC.Domain.Model;

namespace ShipWrecks.GRPC.Domain.Repositories
{
    public interface IShipWreckQueryBuilder : IQueryBuilder<ShipWreckEntity, IShipWreckQueryBuilder>
    {
    }
}