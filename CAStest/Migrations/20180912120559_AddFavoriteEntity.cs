using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddFavoriteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractCounterparties_ContractId",
                table: "ContractCounterparties",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCounterparties_CounterpartyId",
                table: "ContractCounterparties",
                column: "CounterpartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ContractId",
                table: "Favorites",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCounterparties_Contracts_ContractId",
                table: "ContractCounterparties",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCounterparties_Counterparties_CounterpartyId",
                table: "ContractCounterparties",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractCounterparties_Contracts_ContractId",
                table: "ContractCounterparties");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractCounterparties_Counterparties_CounterpartyId",
                table: "ContractCounterparties");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_ContractCounterparties_ContractId",
                table: "ContractCounterparties");

            migrationBuilder.DropIndex(
                name: "IX_ContractCounterparties_CounterpartyId",
                table: "ContractCounterparties");
        }
    }
}
