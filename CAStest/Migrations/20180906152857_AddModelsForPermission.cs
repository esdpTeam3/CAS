using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddModelsForPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissionses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissionses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PermissionId = table.Column<int>(nullable: false),
                    PermissionsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionsGroups_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionsGroups_Permissionses_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissionses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupsUserGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PermissionGroupId = table.Column<int>(nullable: false),
                    PermissionsGroupId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsUserGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsUserGroups_PermissionsGroups_PermissionsGroupId",
                        column: x => x.PermissionsGroupId,
                        principalTable: "PermissionsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupsUserGroups_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsUserGroups_PermissionsGroupId",
                table: "GroupsUserGroups",
                column: "PermissionsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsUserGroups_UserId1",
                table: "GroupsUserGroups",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsGroups_ContractId",
                table: "PermissionsGroups",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsGroups_PermissionsId",
                table: "PermissionsGroups",
                column: "PermissionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsUserGroups");

            migrationBuilder.DropTable(
                name: "PermissionsGroups");

            migrationBuilder.DropTable(
                name: "Permissionses");
        }
    }
}
