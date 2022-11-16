using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagementSystem.Migrations
{
    public partial class UserNRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Identities_IdentityEmail",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Identities");

            migrationBuilder.RenameColumn(
                name: "IdentityEmail",
                table: "Roles",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_IdentityEmail",
                table: "Roles",
                newName: "IX_Roles_UserEmail");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserEmail",
                table: "Roles",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserEmail",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Roles",
                newName: "IdentityEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_UserEmail",
                table: "Roles",
                newName: "IX_Roles_IdentityEmail");

            migrationBuilder.CreateTable(
                name: "Identities",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Email);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Identities_IdentityEmail",
                table: "Roles",
                column: "IdentityEmail",
                principalTable: "Identities",
                principalColumn: "Email");
        }
    }
}
