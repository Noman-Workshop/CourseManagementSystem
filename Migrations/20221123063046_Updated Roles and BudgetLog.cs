using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagementSystem.Migrations
{
    public partial class UpdatedRolesandBudgetLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetAuditLogs_Users_UpdatedByEmail",
                table: "BudgetAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserEmail",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserEmail",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_BudgetAuditLogs_UpdatedByEmail",
                table: "BudgetAuditLogs");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BudgetAuditLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedByEmail",
                table: "BudgetAuditLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersEmail });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersEmail",
                        column: x => x.UsersEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersEmail",
                table: "RoleUser",
                column: "UsersEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Roles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BudgetAuditLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByEmail",
                table: "BudgetAuditLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                columns: new[] { "Id", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserEmail",
                table: "Roles",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetAuditLogs_UpdatedByEmail",
                table: "BudgetAuditLogs",
                column: "UpdatedByEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetAuditLogs_Users_UpdatedByEmail",
                table: "BudgetAuditLogs",
                column: "UpdatedByEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserEmail",
                table: "Roles",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }
    }
}
