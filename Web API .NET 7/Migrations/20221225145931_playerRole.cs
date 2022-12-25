using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.NET7.Migrations
{
    /// <inheritdoc />
    public partial class playerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Demage",
                table: "Weapons",
                newName: "Damage");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Player");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Damage",
                table: "Weapons",
                newName: "Demage");
        }
    }
}
