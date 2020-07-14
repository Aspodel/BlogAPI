using BlogApi.Models.ManyToMany;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Models.User
{
    public class UserModel : IdentityUser<int>
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string UserImage { get; set; }
        public string UserDescription { get; set; }
        public ICollection<BlogUser> BlogUsers { get; set; }
    }
}
