using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogBackend.Models
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        [BsonElement("comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
