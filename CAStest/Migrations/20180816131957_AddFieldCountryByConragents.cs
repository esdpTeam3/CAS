using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddFieldCountryByConragents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Individual_Country",
                table: "Counterparties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "Individual_Country",
                table: "Counterparties");
        }
    }
}
