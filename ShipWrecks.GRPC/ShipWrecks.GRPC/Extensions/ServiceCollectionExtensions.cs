using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipWrecks.GRPC.Domain.Repositories;
using ShipWrecks.Infrastructure.Persistence.Configuration;
using ShipWrecks.Infrastructure.Persistence.Mongo;
using ShipWrecks.Infrastructure.Persistence.QueryBuilder;
using ShipWrecks.Infrastructure.Persistence.Repository;

namespace ShipWrecks.GRPC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConfiguration = configuration.GetSection("DbConfiguration").Get<MongoDbConfiguration>();
            
            DbMappings.RegisterAbstractTypes();
            
            services.AddSingleton(mongoConfiguration);
            services.AddSingleton<IDatabaseProvider, DatabaseProvider>();
            
            services.AddScoped<IShipWreckRepository, ShipWreckRepository>();
           
            services.AddScoped<IShipWreckQueryBuilder, ShipWreckQueryBuilder>();

            return services;
        }
    }
}