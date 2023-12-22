using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRest.Context.Migrations
{
    /// <inheritdoc />
    public partial class UniquePersonEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_PersonId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PersonId",
                table: "Employees",
                column: "PersonId",
                unique: true,
                filter: "DeletedAt is null");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employee_PersonId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonId",
                table: "Employees",
                column: "PersonId");
        }
    }
}
