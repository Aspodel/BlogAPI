using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApi.Database;
using BlogApi.Models.BlogModels;
using BlogApi.Models;
using BlogApi.Models.Middleware;
using BlogApi.Models.ManyToMany;

namespace BlogApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Blogs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog([FromBody]AddNewBlog model)
        {
            Author author = new Author
            {
                //AuthorName = model.AuthorName
            };
            Blog blog = new Blog()
            {
                Title = model.Title,
                Like = 0,
                Status = false,
                Content = new Content(),
                PublicationTime = DateTime.UtcNow,
                //BlogAuthors = new List<BlogAuthor> { new BlogAuthor { Blog = blog, Author = author } }
            };
            BlogAuthor blogAuthor = new BlogAuthor()
            {
                Author = author,
                Blog = blog
            };
            _context.Authors.Add(author);
            _context.Blogs.Add(blog);
            _context.BlogAuthors.Add(blogAuthor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> NewBlog([FromBody] AddNewBlog model)
        {
            var CheckBlogName = _context.Blogs.FirstOrDefault(x => x.Title == model.Title);
            if(CheckBlogName != null)
            {
                return BadRequest("Title has exist");
            }
            Blog blog = new Blog()
            {
                Title = model.Title,
                Like = 0,
                Status = false,
                Content = new Content(),
                PublicationTime = DateTime.UtcNow,
                //BlogAuthors = new List<BlogAuthor> { new BlogAuthor { Blog = blog, Author = author } }
            };
            foreach (Author author in model.ListAuthor)
            {
                var _author = _context.Authors.FirstOrDefault(x => x.AuthorName == author.AuthorName);
                if (_author != null)
                {
                    BlogAuthor blogAuthor = new BlogAuthor()
                    {
                        Author = _author,
                        Blog = blog
                    };
                    _context.BlogAuthors.Add(blogAuthor);
                }
                else
                {
                    Author author1 = new Author()
                    {
                        AuthorName = author.AuthorName
                    };
                    BlogAuthor blogAuthor = new BlogAuthor()
                    {
                        Author = author1,
                        Blog = blog
                    };
                    _context.BlogAuthors.Add(blogAuthor);
                }
            };
            //BlogAuthor blogAuthor = new BlogAuthor()
            //{
            //    //Author = author,
            //    Blog = blog
            //};
            //_context.Authors.Add(author);
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
        }


        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Blog>> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return blog;
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
