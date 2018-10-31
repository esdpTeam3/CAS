using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CAStest.Migrations
{
    public partial class ChangeTypeUserIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnionUserGroups_AspNetUsers_UserId1",
                table: "UnionUserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UnionUserGroups_UserId1",
                table: "UnionUserGroups");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UnionUserGroups");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UnionUserGroups",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UnionUserGroups_UserId",
                table: "UnionUserGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnionUserGroups_AspNetUsers_UserId",
                table: "UnionUserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnionUserGroups_AspNetUsers_UserId",
                table: "UnionUserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UnionUserGroups_UserId",
                table: "UnionUserGroups");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UnionUserGroups",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UnionUserGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnionUserGroups_UserId1",
                table: "UnionUserGroups",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UnionUserGroups_AspNetUsers_UserId1",
                table: "UnionUserGroups",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
