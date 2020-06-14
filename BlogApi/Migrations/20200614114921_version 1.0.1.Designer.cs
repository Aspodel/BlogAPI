﻿// <auto-generated />
using System;
using BlogApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200614114921_version 1.0.1")]
    partial class version101
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogApi.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BlogApi.Models.BlogModels.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogId");

                    b.HasIndex("ContentId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BlogApi.Models.BlogModels.Content", b =>
                {
                    b.Property<int>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ContentId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("BlogApi.Models.BlogModels.Paragraph", b =>
                {
                    b.Property<int>("ParagraphId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<string>("ParagraphContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParagraphId");

                    b.HasIndex("ContentId");

                    b.ToTable("Paragraphs");
                });

            modelBuilder.Entity("BlogApi.Models.ManyToMany.BlogAuthor", b =>
                {
                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.HasKey("BlogId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BlogAuthors");
                });

            modelBuilder.Entity("BlogApi.Models.BlogModels.Blog", b =>
                {
                    b.HasOne("BlogApi.Models.BlogModels.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlogApi.Models.BlogModels.Paragraph", b =>
                {
                    b.HasOne("BlogApi.Models.BlogModels.Content", "Content")
                        .WithMany("Paragraphs")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlogApi.Models.ManyToMany.BlogAuthor", b =>
                {
                    b.HasOne("BlogApi.Models.Author", "Author")
                        .WithMany("BlogAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogApi.Models.BlogModels.Blog", "Blog")
                        .WithMany("BlogAuthors")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
