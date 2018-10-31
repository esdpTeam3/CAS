using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddFieldPasswordNatificationAndContractNatificationToContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractNotifications",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PasswordNotifications",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractNotifications",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordNotifications",
                table: "AspNetUsers");
        }
    }
}
