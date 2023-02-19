using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class addnormalizedusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c77bfef-ab90-4139-b35b-d3f9dc50695c",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6ee917f-55fc-4d03-9ade-bb5cc9335d38", "SUPERADMIN@BLOG.COM", "SUPERADMIN@BLOG.COM", "AQAAAAEAACcQAAAAED6UN8xChNAhctY5x7jU0J1A1ypXInUg45bGH2yuc4qZu/RDvjjend4fLbehxy9/tA==", "db2a73b2-97d9-46c3-87c8-e90079977827" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c77bfef-ab90-4139-b35b-d3f9dc50695c",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f556693d-a953-4373-807f-2f7640060f9e", null, null, "AQAAAAEAACcQAAAAEO4t4dNgb2jt05Ofxcgv1vHYus8IVInA5Ji5H/w5JZ08BKi5BM3TDHSNXVwdZN+H1A==", "6be4909f-b1c4-4e8f-b2a1-683672e3fa13" });
        }
    }
}
