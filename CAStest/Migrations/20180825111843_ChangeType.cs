using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class ChangeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Inn",
                table: "Counterparties",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyINN",
                table: "Counterparties",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractPropertieses_ContractId",
                table: "ContractPropertieses",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractPropertieses_Contracts_ContractId",
                table: "ContractPropertieses",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractPropertieses_Contracts_ContractId",
                table: "ContractPropertieses");

            migrationBuilder.DropIndex(
                name: "IX_ContractPropertieses_ContractId",
                table: "ContractPropertieses");

            migrationBuilder.AlterColumn<string>(
                name: "Inn",
                table: "Counterparties",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyINN",
                table: "Counterparties",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
