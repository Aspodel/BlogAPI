using BlogApi.Models.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models.BlogModels
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public int Like { get; set; }
        public bool Status { get; set; }
        public string IntroImage { get; set; }



        // One to One relationship
        //public int ContentId { get; set; }
        //public Content Content { get; set; }

        // One to Many relationship: blog is One
        public ICollection<Paragraph> Paragraphs { get; set; }

        // Many to Many relationship
        public ICollection<BlogAuthor> BlogAuthors { get; set; }


        public ICollection<BlogUser> BlogUsers { get; set; }
    }
}
