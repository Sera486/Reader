using Microsoft.EntityFrameworkCore.Migrations;

namespace Reader.Migrations
{
    public partial class Addedhashtobookentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Books");
        }
    }
}
