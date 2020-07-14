using BlogApi.Models.BlogModels;
using BlogApi.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models.Middleware
{
    public class AddNewBlog
    {
        public List<UserModel> ListAuthor = new List<UserModel>();
        public List<Paragraph> paragraphs = new List<Paragraph>();
        public string Title { get; set; }
        public string IntroImage { get; set; }
        public string Description { get; set; }
    }
}
