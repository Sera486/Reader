using Microsoft.EntityFrameworkCore.Migrations;

namespace Reader.Migrations
{
    public partial class Addedcovertobookmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Books");
        }
    }
}
