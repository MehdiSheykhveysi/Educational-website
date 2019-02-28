using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Core.DataBase.Migrations
{
    public partial class EditTransactEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfitmPayTransaction",
                table: "Transact");

            migrationBuilder.AddColumn<string>(
                name: "TransactId",
                table: "Transact",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactId",
                table: "Transact");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfitmPayTransaction",
                table: "Transact",
                nullable: false,
                defaultValue: false);
        }
    }
}
