using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogBackend.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        [BsonElement("user")]
        public string UserName { get; set; }
        [BsonElement("rate")]
        public float Rate { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
