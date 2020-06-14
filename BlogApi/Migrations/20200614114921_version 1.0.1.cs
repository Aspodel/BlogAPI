using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogApi.Migrations
{
    public partial class version101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogAuthor_Authors_AuthorId",
                table: "BlogAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogAuthor_Blogs_BlogId",
                table: "BlogAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogAuthor",
                table: "BlogAuthor");

            migrationBuilder.RenameTable(
                name: "BlogAuthor",
                newName: "BlogAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_BlogAuthor_AuthorId",
                table: "BlogAuthors",
                newName: "IX_BlogAuthors_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogAuthors",
                table: "BlogAuthors",
                columns: new[] { "BlogId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogAuthors_Authors_AuthorId",
                table: "BlogAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogAuthors_Blogs_BlogId",
                table: "BlogAuthors",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogAuthors_Authors_AuthorId",
                table: "BlogAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogAuthors_Blogs_BlogId",
                table: "BlogAuthors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogAuthors",
                table: "BlogAuthors");

            migrationBuilder.RenameTable(
                name: "BlogAuthors",
                newName: "BlogAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_BlogAuthors_AuthorId",
                table: "BlogAuthor",
                newName: "IX_BlogAuthor_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogAuthor",
                table: "BlogAuthor",
                columns: new[] { "BlogId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogAuthor_Authors_AuthorId",
                table: "BlogAuthor",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogAuthor_Blogs_BlogId",
                table: "BlogAuthor",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
