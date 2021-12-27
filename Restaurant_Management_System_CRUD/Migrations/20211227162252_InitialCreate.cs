using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant_Management_System_CRUD.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "RestuarantCustomer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "RestuarantCustomer",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RestuarantCustomer",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RestuarantCustomer",
                newName: "CustomerId");
        }
    }
}
