using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagementSystem.Migrations
{
    public partial class AuditLogupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AuditLogs_AuditLogId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_AuditLogId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "AuditLogId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AuditLogs");

            migrationBuilder.AddColumn<string>(
                name: "BudgetId",
                table: "AuditLogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmail",
                table: "AuditLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByEmail",
                table: "AuditLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_BudgetId",
                table: "AuditLogs",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedByEmail",
                table: "AuditLogs",
                column: "CreatedByEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UpdatedByEmail",
                table: "AuditLogs",
                column: "UpdatedByEmail");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_BudgetId",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_CreatedByEmail",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_UpdatedByEmail",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "CreatedByEmail",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedByEmail",
                table: "AuditLogs");

            migrationBuilder.AddColumn<string>(
                name: "AuditLogId",
                table: "Budgets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_AuditLogId",
                table: "Budgets",
                column: "AuditLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AuditLogs_AuditLogId",
                table: "Budgets",
                column: "AuditLogId",
                principalTable: "AuditLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
