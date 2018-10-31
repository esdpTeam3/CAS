using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddFileModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CASFiles",
                columns: table => new
                {
                    ActId = table.Column<int>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ContractId = table.Column<int>(nullable: true),
                    SupplementaryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CASFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CASFiles_Acts_ActId",
                        column: x => x.ActId,
                        principalTable: "Acts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CASFiles_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CASFiles_Supplementaries_SupplementaryId",
                        column: x => x.SupplementaryId,
                        principalTable: "Supplementaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CASFiles_ActId",
                table: "CASFiles",
                column: "ActId");

            migrationBuilder.CreateIndex(
                name: "IX_CASFiles_ContractId",
                table: "CASFiles",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_CASFiles_SupplementaryId",
                table: "CASFiles",
                column: "SupplementaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CASFiles");
        }
    }
}
