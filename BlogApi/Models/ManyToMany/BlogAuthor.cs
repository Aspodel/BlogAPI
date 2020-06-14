using BlogApi.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models.ManyToMany
{
    public class BlogAuthor
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
