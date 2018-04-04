using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Piwerko.Api.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    avatar_URL = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    firstname = table.Column<string>(nullable: true),
                    isAdmin = table.Column<bool>(nullable: false),
                    isConfirmed = table.Column<bool>(nullable: false),
                    lastname = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
