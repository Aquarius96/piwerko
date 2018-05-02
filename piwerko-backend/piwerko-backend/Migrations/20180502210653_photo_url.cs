using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Piwerko.Api.Migrations
{
    public partial class photo_url : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "webUrl",
                table: "Breweries",
                newName: "web_Url");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photo_URL",
                table: "Breweries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photo_URL",
                table: "Beers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<string>(nullable: true),
                    beerId = table.Column<int>(nullable: false),
                    breweryId = table.Column<int>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "photo_URL",
                table: "Breweries");

            migrationBuilder.DropColumn(
                name: "photo_URL",
                table: "Beers");

            migrationBuilder.RenameColumn(
                name: "web_Url",
                table: "Breweries",
                newName: "webUrl");
        }
    }
}
