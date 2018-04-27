using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Reader.Migrations
{
    public partial class Addedusercatalogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Libraries_LibraryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Libraries_LibraryId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Books_LibraryId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LibraryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatalogBook",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    CatalogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBook", x => new { x.BookId, x.CatalogId });
                    table.ForeignKey(
                        name: "FK_CatalogBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogBook_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogBook_CatalogId",
                table: "CatalogBook",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_UserId",
                table: "Catalogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogBook");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibraryId",
                table: "Books",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LibraryId",
                table: "AspNetUsers",
                column: "LibraryId",
                unique: true,
                filter: "[LibraryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Libraries_LibraryId",
                table: "AspNetUsers",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Libraries_LibraryId",
                table: "Books",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
