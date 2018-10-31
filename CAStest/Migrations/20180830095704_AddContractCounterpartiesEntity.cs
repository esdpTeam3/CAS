using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddContractCounterpartiesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractCounterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
                    CounterpartyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCounterparties", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractPropertieses_PropertyId",
                table: "ContractPropertieses",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractPropertieses_Properties_PropertyId",
                table: "ContractPropertieses",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractPropertieses_Properties_PropertyId",
                table: "ContractPropertieses");

            migrationBuilder.DropTable(
                name: "ContractCounterparties");

            migrationBuilder.DropIndex(
                name: "IX_ContractPropertieses_PropertyId",
                table: "ContractPropertieses");
        }
    }
}
