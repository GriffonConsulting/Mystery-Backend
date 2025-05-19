using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class UserProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NbPlayers",
                table: "UserMurderProduct",
                newName: "ProductType");

            migrationBuilder.AddColumn<string>(
                name: "ProductUserConfiguration",
                table: "UserMurderProduct",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductUserConfiguration",
                table: "UserMurderProduct");

            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "UserMurderProduct",
                newName: "NbPlayers");
        }
    }
}
