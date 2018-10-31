using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddEntityContractType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CASFiles_Contracts_ContractId1",
                table: "CASFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UnionUserGroups_Groups_UserGroupId",
                table: "UnionUserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UnionUserGroups_AspNetUsers_UserId",
                table: "UnionUserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnionUserGroups",
                table: "UnionUserGroups");

            migrationBuilder.DropIndex(
                name: "IX_CASFiles_ContractId1",
                table: "CASFiles");

            migrationBuilder.DropColumn(
                name: "ContractId1",
                table: "CASFiles");

            migrationBuilder.RenameTable(
                name: "UnionUserGroups",
                newName: "UnionUserGroup");

            migrationBuilder.RenameIndex(
                name: "IX_UnionUserGroups_UserId",
                table: "UnionUserGroup",
                newName: "IX_UnionUserGroup_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UnionUserGroups_UserGroupId",
                table: "UnionUserGroup",
                newName: "IX_UnionUserGroup_UserGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnionUserGroup",
                table: "UnionUserGroup",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UnionUserGroup_Groups_UserGroupId",
                table: "UnionUserGroup",
                column: "UserGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnionUserGroup_AspNetUsers_UserId",
                table: "UnionUserGroup",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnionUserGroup_Groups_UserGroupId",
                table: "UnionUserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UnionUserGroup_AspNetUsers_UserId",
                table: "UnionUserGroup");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnionUserGroup",
                table: "UnionUserGroup");

            migrationBuilder.RenameTable(
                name: "UnionUserGroup",
                newName: "UnionUserGroups");

            migrationBuilder.RenameIndex(
                name: "IX_UnionUserGroup_UserId",
                table: "UnionUserGroups",
                newName: "IX_UnionUserGroups_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UnionUserGroup_UserGroupId",
                table: "UnionUserGroups",
                newName: "IX_UnionUserGroups_UserGroupId");

            migrationBuilder.AddColumn<int>(
                name: "ContractId1",
                table: "CASFiles",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnionUserGroups",
                table: "UnionUserGroups",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UnionUserGroups_Groups_UserGroupId",
                table: "UnionUserGroups",
                column: "UserGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnionUserGroups_AspNetUsers_UserId",
                table: "UnionUserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
