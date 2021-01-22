using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShipWrecks.GRPC.Domain.Model
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Entity(string id)
        {
            Id = id;
        }
        
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; private set; }
    }
}