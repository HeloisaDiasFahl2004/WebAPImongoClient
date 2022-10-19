using MongoDB.Bson.Serialization.Attributes;

namespace WebAPImongoClient.Models
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation((MongoDB.Bson.BsonType.ObjectId))]
        public string Id { get; set; }
        public string Description { get; set; }//street/number...
        public string City { get; set; }
        public string Region { get; set; }
    }
}

