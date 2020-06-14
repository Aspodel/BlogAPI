using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models.Middleware
{
    public class AddNewBlog
    {
        public List<Author> ListAuthor = new List<Author>();

        public string Title { get; set; }
        //public string AuthorName { get; set; }
    }
}
