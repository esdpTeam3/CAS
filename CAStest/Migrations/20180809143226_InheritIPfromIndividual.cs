using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class InheritIPfromIndividual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Counterparties_CounterpartyId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Counterparties_Counterparties_IndividualId",
                table: "Counterparties");

            migrationBuilder.DropIndex(
                name: "IX_Counterparties_IndividualId",
                table: "Counterparties");

            migrationBuilder.DropColumn(
                name: "IndividualId",
                table: "Counterparties");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Counterparties_CounterpartyId",
                table: "Contacts",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Counterparties_CounterpartyId",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "IndividualId",
                table: "Counterparties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Counterparties_IndividualId",
                table: "Counterparties",
                column: "IndividualId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Counterparties_CounterpartyId",
                table: "Contacts",
                column: "CounterpartyId",
                principalTable: "Counterparties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Counterparties_Counterparties_IndividualId",
                table: "Counterparties",
                column: "IndividualId",
                principalTable: "Counterparties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
