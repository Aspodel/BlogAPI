using BlogApi.Models;
using BlogApi.Models.BlogModels;
using BlogApi.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<BlogAuthor> BlogAuthors { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogAuthor>()
                .HasKey(t => new { t.BlogId, t.AuthorId });

            modelBuilder.Entity<BlogAuthor>()
                .HasOne(pt => pt.Blog)
                .WithMany(p => p.BlogAuthors)
                .HasForeignKey(pt => pt.BlogId);

            modelBuilder.Entity<BlogAuthor>()
                .HasOne(pt => pt.Author)
                .WithMany(t => t.BlogAuthors)
                .HasForeignKey(pt => pt.AuthorId);
        }
    }
}
