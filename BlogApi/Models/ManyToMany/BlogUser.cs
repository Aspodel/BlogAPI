using BlogApi.Models.BlogModels;
using BlogApi.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models.ManyToMany
{
    public class BlogUser
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
