using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagementSystem.Migrations
{
    public partial class Contextupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Budgets_BudgetId",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_CreatedByEmail",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_UpdatedByEmail",
                table: "AuditLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                newName: "BudgetAuditLogs");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_UpdatedByEmail",
                table: "BudgetAuditLogs",
                newName: "IX_BudgetAuditLogs_UpdatedByEmail");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_CreatedByEmail",
                table: "BudgetAuditLogs",
                newName: "IX_BudgetAuditLogs_CreatedByEmail");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_BudgetId",
                table: "BudgetAuditLogs",
                newName: "IX_BudgetAuditLogs_BudgetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetAuditLogs",
                table: "BudgetAuditLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetAuditLogs_Budgets_BudgetId",
                table: "BudgetAuditLogs",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetAuditLogs_Users_CreatedByEmail",
                table: "BudgetAuditLogs",
                column: "CreatedByEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetAuditLogs_Users_UpdatedByEmail",
                table: "BudgetAuditLogs",
                column: "UpdatedByEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetAuditLogs_Budgets_BudgetId",
                table: "BudgetAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetAuditLogs_Users_CreatedByEmail",
                table: "BudgetAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetAuditLogs_Users_UpdatedByEmail",
                table: "BudgetAuditLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetAuditLogs",
                table: "BudgetAuditLogs");

            migrationBuilder.RenameTable(
                name: "BudgetAuditLogs",
                newName: "AuditLogs");

            migrationBuilder.RenameIndex(
                name: "IX_BudgetAuditLogs_UpdatedByEmail",
                table: "AuditLogs",
                newName: "IX_AuditLogs_UpdatedByEmail");

            migrationBuilder.RenameIndex(
                name: "IX_BudgetAuditLogs_CreatedByEmail",
                table: "AuditLogs",
                newName: "IX_AuditLogs_CreatedByEmail");

            migrationBuilder.RenameIndex(
                name: "IX_BudgetAuditLogs_BudgetId",
                table: "AuditLogs",
                newName: "IX_AuditLogs_BudgetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Budgets_BudgetId",
                table: "AuditLogs",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_CreatedByEmail",
                table: "AuditLogs",
                column: "CreatedByEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UpdatedByEmail",
                table: "AuditLogs",
                column: "UpdatedByEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }
    }
}
