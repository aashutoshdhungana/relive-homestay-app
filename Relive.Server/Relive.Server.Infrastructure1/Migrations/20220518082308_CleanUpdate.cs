using Microsoft.EntityFrameworkCore.Migrations;

namespace Relive.Server.Infrastructure.Migrations
{
    public partial class CleanUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "Users",
                newName: "IsAdmin");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.AddColumn<bool>(
                name: "HasHostProfile",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTravellerProfile",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasHostProfile",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HasTravellerProfile",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "Users",
                newName: "IsVerified");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "FullName");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
