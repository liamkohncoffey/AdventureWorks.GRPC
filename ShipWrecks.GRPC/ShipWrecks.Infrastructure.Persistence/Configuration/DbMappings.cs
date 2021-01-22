using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using ShipWrecks.GRPC.Domain.Model;

namespace ShipWrecks.Infrastructure.Persistence.Configuration
{
    public static class DbMappings
    {
        public static void RegisterAbstractTypes()
        {
            BsonSerializer.RegisterSerializer(
                typeof(decimal),
                new DecimalSerializer(BsonType.Double,
                    new RepresentationConverter(
                        true, //allow truncation
                        true // allow overflow, return decimal.MinValue or decimal.MaxValue instead
                    ))
            );
            
            BsonClassMap.RegisterClassMap<ShipWreckEntity>(cm => {
                cm.AutoMap();
                cm.GetMemberMap(m => m.Record).SetElementName("recrd");
                cm.GetMemberMap(m => m.Chart).SetElementName("chart");
                cm.GetMemberMap(m => m.VesselTerms).SetElementName("vesslterms");
                cm.GetMemberMap(m => m.FeatureType).SetElementName("feature_type");
                cm.GetMemberMap(m => m.LatDec).SetElementName("latdec");
                cm.GetMemberMap(m => m.LonDec).SetElementName("londec");
                cm.GetMemberMap(m => m.GpQuality).SetElementName("gp_quality");
                cm.GetMemberMap(m => m.Depth).SetElementName("depth");
                cm.GetMemberMap(m => m.SoundingType).SetElementName("sounding_type");
                cm.GetMemberMap(m => m.History).SetElementName("history");
                cm.GetMemberMap(m => m.Quasou).SetElementName("quasou");
                cm.GetMemberMap(m => m.WatLev).SetElementName("watlev");
                cm.GetMemberMap(m => m.Coordinates).SetElementName("coordinates");
            });
        }
    }
}