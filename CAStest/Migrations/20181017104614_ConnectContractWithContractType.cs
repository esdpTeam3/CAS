using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using ReferentialAction = Microsoft.EntityFrameworkCore.Migrations.ReferentialAction;

namespace CAStest.Migrations
{
    public partial class ConnectContractWithContractType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "ContractTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypes_ContractId",
                table: "ContractTypes",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTypes_Contracts_ContractId",
                table: "ContractTypes",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTypes_Contracts_ContractId",
                table: "ContractTypes");

            migrationBuilder.DropIndex(
                name: "IX_ContractTypes_ContractId",
                table: "ContractTypes");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "ContractTypes");
        }
    }
}
