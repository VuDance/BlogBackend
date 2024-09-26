// Data/BlogContext.cs
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using BlogBackend.Models;

namespace BlogApi.Data
{
    public class BlogContext
    {
        private readonly IMongoDatabase _database;

        public BlogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            _database = client.GetDatabase(configuration.GetSection("MongoDB:DatabaseName").Value);
        }

        public IMongoCollection<Blog> Blogs
        {
            get
            {
                return _database.GetCollection<Blog>("blogs");
            }
        }
    }
}
