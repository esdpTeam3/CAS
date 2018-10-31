using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class AddNotificationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfCreate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });
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
                name: "CASFiles");

            migrationBuilder.DropTable(
                name: "Notifications");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnionUserGroups",
                table: "UnionUserGroups",
                column: "Id");

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
