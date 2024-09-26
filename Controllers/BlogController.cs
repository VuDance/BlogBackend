using BlogApi.Data;
using BlogBackend.DTOs;
using BlogBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BlogBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            var blogs = await _context.Blogs.Find(blog => true).ToListAsync();
            return Ok(blogs);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(string id)
        {
            var blog = await _context.Blogs.Find(b => b.Id == id).FirstOrDefaultAsync();
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(BlogCreate blog)
        {
            var b = new Blog
            {
                CreatedAt = DateTime.Now,
                Content = blog.Content,
                Title = blog.Title,
                Image=blog.Image,
            };
            await _context.Blogs.InsertOneAsync(b);
            return CreatedAtAction(nameof(GetBlog), new { id = b.Id }, b);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(string id, Blog blog)
        {
            var existingBlog = await _context.Blogs.Find(b => b.Id == id).FirstOrDefaultAsync();
            if (existingBlog == null)
            {
                return NotFound();
            }

            blog.Id = id;  // Ensure the ID is not changed
            await _context.Blogs.ReplaceOneAsync(b => b.Id == id, blog);
            return NoContent();
        }

        // DELETE: api/Blog/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            var result = await _context.Blogs.DeleteOneAsync(b => b.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost("{blogId}/comments")]
        public async Task<IActionResult> AddComment(string blogId, [FromBody] CommentRequest comment)
        {
            if (string.IsNullOrWhiteSpace(blogId) || comment == null)
            {
                return BadRequest("Invalid blog ID or comment.");
            }
            var c = new Comment
            {
                Content = comment.Content,
                UserName = comment.UserName,
                Rate = comment.Rate,
                CreatedAt = DateTime.Now
            };

            var updateDefinition = Builders<Blog>.Update.Push(p => p.Comments, c);
            await _context.Blogs.UpdateOneAsync(p => p.Id == blogId, updateDefinition);
            return Ok("Created");
        }
    }
}
