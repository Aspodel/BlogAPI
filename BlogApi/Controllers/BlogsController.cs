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
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using BlogApi.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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
            return await _context.Blogs.Include("BlogAuthors").Include("Paragraphs").ToListAsync();
        }




        // GET: api/Blogs
        [HttpGet("{id}")]
        public async Task<object> BlogInfor(int id)
        {
            Blog blog = await _context.Blogs.AsNoTracking().Include(x => x.BlogUsers).Include(y => y.Paragraphs).FirstOrDefaultAsync(z => z.BlogId == id);


            List<AuthorList> authors = new List<AuthorList>();

            foreach(var data in blog.BlogUsers)
            {
                var author = await _context.Users.FindAsync(data.UserId);


                authors.Add(new AuthorList() { AuthorId = author.Id, AuthorName = author.FullName, AuthorDescription = author.UserDescription, ProfileImage = author.UserImage });

            }

            return new {
                blog.BlogId,
                blog.Title,
                blog.Description,
                blog.Time,
                blog.Like,
                blog.IntroImage,
                authors
            };
        }




        [HttpGet]
        public async Task<IActionResult> BlogList()
        {
            var blogs =  _context.Blogs.AsNoTracking().Include(x => x.BlogUsers).Include(y => y.Paragraphs).OrderByDescending(a => a.Time);
            List<BlogList> list = new List<BlogList>();

            foreach(var blog in blogs)
            {

                List<string> authors = new List<string>();

                foreach (var author in blog.BlogUsers)
                {
                    var result = await _context.Users.FindAsync(author.UserId);

                    authors.Add(result.FullName);

                }

                list.Add(new BlogList() { BlogId = blog.BlogId, Title = blog.Title, Description = blog.Description, Time = blog.Time, IntroImage = blog.IntroImage, Authors = authors });
            }

            return Ok(list);
        }




        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var ListBlog = await _context.Blogs.Include("BlogAuthors").Include("Paragraphs").ToListAsync();
            var blog = ListBlog.FirstOrDefault(x => x.BlogId == id);


            if (blog == null)
            {
                return BadRequest("Blog not found");
            }

            //var result = await _context.Blogs.Include("Paragraphs").Include("BlogAuthors").Where(x => x.BlogId == id);

            return blog;
        }




        // GET: http://localhost:65472/api/Blogs/GetAuthor/id
        // GET SPECIFIED AUTHOR
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);


            if (author == null)
            {
                return BadRequest("Author not found");
            }

            return author;
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




        // GET: http://localhost:65472/api/Blogs/UpdateLike/id
        // UPDATE LIKE FOR BLOG
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateLike(int id)
        {
            Blog blog = await _context.Blogs.FindAsync(id);
            blog.Like += 1;
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return Ok(blog);
        }




        // GET: http://localhost:65472/api/Blogs/BlogsOfAuthor/id
        // GET BLOGS FROM SPECIFICED AUTHOR
        [HttpGet("{id}")]
        public async Task<IActionResult> BlogsOfAuthor(int id)
        {
            var ListBlogId = _context.BlogAuthors.Where(x => x.AuthorId == id).ToList();
            List<Blog> ListBlog = new List<Blog>();
            foreach (var item in ListBlogId)
            {
                Blog blog = await _context.Blogs.FindAsync(item.BlogId);
                ListBlog.Add(blog);
            }

            return Ok(ListBlog);
        }




        // POST: http://localhost:65472/api/Blogs/NewBlog
        // POST NEW BLOG
        [HttpPost]
        public async Task<ActionResult<Blog>> NewBlog([FromBody] AddNewBlog model)
        {
            var CheckBlogName = _context.Blogs.FirstOrDefault(x => x.Title == model.Title);
            if (CheckBlogName != null)
            {
                return BadRequest("Title has exist");
            }
            Blog blog = new Blog()
            {
                Title = model.Title,
                IntroImage = model.IntroImage,
                Description = model.Description,
                Like = 0,
                Status = false,
                Paragraphs = model.paragraphs,
                Time = DateTime.UtcNow
            };
            foreach (UserModel user in model.ListAuthor)
            {
                var CheckAuthor = _context.Users.FirstOrDefault(x => x.Id == user.Id);

                if (CheckAuthor != null)
                {
                    BlogUser blogUser = new BlogUser()
                    {
                        User = CheckAuthor,
                        Blog = blog
                    };
                    _context.BlogUsers.Add(blogUser);
                }
                else
                {
                    return BadRequest("Author not found");
                }
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
        }




        // POST: http://localhost:65472/api/Blogs/NewAuthor
        // POST NEW BLOG
        [HttpPost]
        public async Task<IActionResult> NewAuthor([FromBody] Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Ok(author);
        }

        // GET: http://localhost:65472/api/Blogs/GetContent/id
        // GET CONTENT OF BLOG 
        [HttpGet("{id}")]
        public IActionResult GetContent(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            var BlogContent = from Paragraph in _context.Paragraphs.AsNoTracking().Where(x => x.BlogId == id)
                              select new
                              {
                                  Paragraph.ParagraphId,
                                  Paragraph.Type,
                                  Paragraph.Content
                              };
            return Ok(BlogContent.AsNoTracking());
        }


        // GET: http://localhost:65472/api/Blogs/SearchByName/keyword
        // SEARCH BLOG BY NAME
        [HttpGet("{keyword}")]
        public async Task<IActionResult> SearchByName (string keyword)
        {
            var blog = from b in _context.Blogs select b;
            if (!String.IsNullOrEmpty(keyword))
            {
                blog = blog.Where(s => s.Title.Contains(keyword));
            }

            return Ok(await blog.ToListAsync());
        }

        // GET: http://localhost:65472/api/Blogs/GetAuthors
        // GET LIST OF AUTHORS
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _context.Authors.Include("BlogAuthors").ToListAsync());
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
