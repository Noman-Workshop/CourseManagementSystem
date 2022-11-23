using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagementSystem.Migrations
{
    public partial class BudgetAuditLogUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetAuditLogs_Budgets_BudgetId",
                table: "BudgetAuditLogs");

            migrationBuilder.AlterColumn<string>(
                name: "BudgetId",
                table: "BudgetAuditLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetAuditLogs_Budgets_BudgetId",
                table: "BudgetAuditLogs",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetAuditLogs_Budgets_BudgetId",
                table: "BudgetAuditLogs");

            migrationBuilder.AlterColumn<string>(
                name: "BudgetId",
                table: "BudgetAuditLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetAuditLogs_Budgets_BudgetId",
                table: "BudgetAuditLogs",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id");
        }
    }
}
