using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Web.Migrations
{
    /// <inheritdoc />
    public partial class addtagsnavigationprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tags_BlogPostId",
                table: "Tags",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_BlogPosts_BlogPostId",
                table: "Tags",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_BlogPosts_BlogPostId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_BlogPostId",
                table: "Tags");
        }
    }
}
