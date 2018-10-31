using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class FixFileModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId1",
                table: "CASFiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CASFiles_ContractId1",
                table: "CASFiles",
                column: "ContractId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CASFiles_Contracts_ContractId1",
                table: "CASFiles",
                column: "ContractId1",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CASFiles_Contracts_ContractId1",
                table: "CASFiles");

            migrationBuilder.DropIndex(
                name: "IX_CASFiles_ContractId1",
                table: "CASFiles");

            migrationBuilder.DropColumn(
                name: "ContractId1",
                table: "CASFiles");
        }
    }
}
