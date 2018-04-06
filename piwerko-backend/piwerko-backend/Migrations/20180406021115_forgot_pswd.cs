using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Piwerko.Api.Migrations
{
    public partial class forgot_pswd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivationKey",
                table: "Users",
                newName: "ConfirmationCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConfirmationCode",
                table: "Users",
                newName: "ActivationKey");
        }
    }
}
