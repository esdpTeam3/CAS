using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddLegalAddressAndFieldIsBlocked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LegalAddress",
                table: "Counterparties",
                newName: "Individual_LegalAddress");

            migrationBuilder.AddColumn<string>(
                name: "LegalAddress",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Counterparties",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Contracts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegalAddress",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Individual_LegalAddress",
                table: "Counterparties",
                newName: "LegalAddress");
        }
    }
}
