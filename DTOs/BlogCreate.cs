using MongoDB.Bson.Serialization.Attributes;

namespace BlogBackend.DTOs
{
    public class BlogCreate
    {
        public string Title { get; set; }
        public string Image {  get; set; }
        public string Content { get; set; }
    }
}
