﻿using BlogApi.Models.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
        public string ProfileImage { get; set; }

        // Many to Many relationship
        public ICollection<BlogAuthor> BlogAuthors { get; set; }

    }
}
