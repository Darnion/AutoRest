using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRest.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_LoyaltyCards_LoyaltyCardId",
                table: "OrderItems");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Persons",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Persons",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Persons",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "LoyaltyCardId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeCashierId1",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_EmployeeCashierId1",
                table: "OrderItems",
                column: "EmployeeCashierId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Employees_EmployeeCashierId1",
                table: "OrderItems",
                column: "EmployeeCashierId1",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_LoyaltyCards_LoyaltyCardId",
                table: "OrderItems",
                column: "LoyaltyCardId",
                principalTable: "LoyaltyCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Employees_EmployeeCashierId1",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_LoyaltyCards_LoyaltyCardId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_EmployeeCashierId1",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "EmployeeCashierId1",
                table: "OrderItems");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<Guid>(
                name: "LoyaltyCardId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_LoyaltyCards_LoyaltyCardId",
                table: "OrderItems",
                column: "LoyaltyCardId",
                principalTable: "LoyaltyCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
