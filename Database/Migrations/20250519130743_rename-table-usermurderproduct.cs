using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class renametableusermurderproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMurderProduct_OrderContent_OrderContentId",
                table: "UserMurderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMurderProduct_Product_ProductId",
                table: "UserMurderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMurderProduct_User_UserId",
                table: "UserMurderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMurderProduct",
                table: "UserMurderProduct");

            migrationBuilder.RenameTable(
                name: "UserMurderProduct",
                newName: "UserProduct");

            migrationBuilder.RenameIndex(
                name: "IX_UserMurderProduct_UserId",
                table: "UserProduct",
                newName: "IX_UserProduct_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMurderProduct_ProductId",
                table: "UserProduct",
                newName: "IX_UserProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMurderProduct_OrderContentId",
                table: "UserProduct",
                newName: "IX_UserProduct_OrderContentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProduct",
                table: "UserProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProduct_OrderContent_OrderContentId",
                table: "UserProduct",
                column: "OrderContentId",
                principalTable: "OrderContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProduct_Product_ProductId",
                table: "UserProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProduct_User_UserId",
                table: "UserProduct",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProduct_OrderContent_OrderContentId",
                table: "UserProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProduct_Product_ProductId",
                table: "UserProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProduct_User_UserId",
                table: "UserProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProduct",
                table: "UserProduct");

            migrationBuilder.RenameTable(
                name: "UserProduct",
                newName: "UserMurderProduct");

            migrationBuilder.RenameIndex(
                name: "IX_UserProduct_UserId",
                table: "UserMurderProduct",
                newName: "IX_UserMurderProduct_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProduct_ProductId",
                table: "UserMurderProduct",
                newName: "IX_UserMurderProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProduct_OrderContentId",
                table: "UserMurderProduct",
                newName: "IX_UserMurderProduct_OrderContentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMurderProduct",
                table: "UserMurderProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMurderProduct_OrderContent_OrderContentId",
                table: "UserMurderProduct",
                column: "OrderContentId",
                principalTable: "OrderContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMurderProduct_Product_ProductId",
                table: "UserMurderProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMurderProduct_User_UserId",
                table: "UserMurderProduct",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
