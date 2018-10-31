using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddNewFieldsToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BIC",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckingAccount",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyINN",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndividualAddress",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalAddress",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailAddress",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Counterparties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BIC",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "CheckingAccount",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "CompanyINN",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "IndividualAddress",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "LegalAddress",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "MailAddress",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Counterparties");
        }
    }
}
