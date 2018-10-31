using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class ChangeActAndSupplementaryNameToActAndSupplementaryNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Supplementaries",
                newName: "SupplementaryNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Acts",
                newName: "ActNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplementaryNumber",
                table: "Supplementaries",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ActNumber",
                table: "Acts",
                newName: "Name");
        }
    }
}
