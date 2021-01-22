using System.Threading;
using System.Threading.Tasks;
using ShipWrecks.GRPC.Domain.Model;
using ShipWrecks.GRPC.Domain.Repositories;

namespace ShipWrecks.Infrastructure.Persistence.Repository
{
    public interface IShipWreckRepository
    {
        Task Add(ShipWreckEntity paymentHistoryEntry, CancellationToken cancellationToken = default);

        IShipWreckQueryBuilder Query();
    }
}